using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Data.Entity;
using Helpers;
using System.Text;

namespace FrontEnd.Areas.Admin.Controllers
{
    public class NotificacionesController : Controller
    {

        ContextoAplicacion db = new ContextoAplicacion();
        FCMPushNotification fcmPush = new FCMPushNotification();

        private Notificacion notificacion = new Notificacion();
        private CTE_NOTIFICACION_GRUPO Grupo = new CTE_NOTIFICACION_GRUPO();
        // GET: Admin/Notificaciones
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Listar(AnexGRID grid)
        {
            return Json(notificacion.Listar(grid));

        }
        public ActionResult Crud(int id = 0)
        {
            return View();

        }
       
        public ActionResult Create()
        {
            try
            {         
            using (var ctx = new ContextoAplicacion())
            {
                var clientes = new SelectList(ctx.CTE_CUENTA_USUARIO.ToList(), "CuentaUsuarioId", "NOMBRE_USUARIO");
    

                    ViewData["Clientes"] = clientes;
            }
            }
            catch(Exception ex)
            {
                throw;
            }
            return PartialView();

        }  
        public ActionResult Masivo()
        {
            var listGoup = new SelectList(Grupo.Listar(), "ID", "NOMBRE");
            ViewData["Grupos"] = listGoup;

            return PartialView();
        }
    
        public JsonResult Guardar(NotificacionViewModel model)
        {

            var rm = new ResponseModel();
            if (ModelState.IsValid)
            {
                rm = model.Guardar(model);

                if (rm.response)
                {
                    rm.function= "closeModalCreate()";
                  //  rm.href = Url.Content("~/Admin/Notificaciones/Index");
                }


            }  

            return Json(rm);     



        }
        [ChildActionOnly]
        public String GruposRegistrados()
        {
            StringBuilder Resultado = new StringBuilder();
            var estadistica = Grupo.ListarEstadisticas();

            //var data = from c in estadistica
            //           group c.IDCLIENTE by c.ESTADO into g
            //           select new
            //           {
            //               Name_Grupo = g.Key,
            //               cuenta_Grupo = g.ToList().Count()
            //           };

            var grupos = estadistica.GroupBy(x => x.ESTADO).ToList();

            Resultado.Append(string.Format("['{0}', '{1}'],", "Estado", "Cantidad"));
            foreach (var grupo in grupos)
                Resultado.Append(string.Format("['{0}', {1}],",
                    grupo.FirstOrDefault().ESTADO.Trim(), grupo.Count()));

             return "[" + Resultado.ToString() + "]";



        }
        public ActionResult LoadData()
        {
            try
            {
                using (var ctx = new ContextoAplicacion())
                {
                    var draw = Request.Form.GetValues("draw").FirstOrDefault();
                    var start = Request.Form.GetValues("start").FirstOrDefault();
                    var length = Request.Form.GetValues("length").FirstOrDefault();
                    var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

                    //Paging Size (10,20,50,100)  
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    //Get Notification
                    var notificationData = (from tempNotification in ctx.CTE_NOTIFICACION
                                            select tempNotification);

                    //Sorting  
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        notificationData = notificationData.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    //Search  
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        notificationData = notificationData.Where(m => m.TITULO == searchValue
                        || m.CUERPO_NOTIFICACION == searchValue );
                    }

                    //total number of rows count   
                    recordsTotal = notificationData.Count();
                    //Paging   
                    var data = notificationData.Skip(skip).Take(pageSize).ToList();

                    //Returning Json Data  
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

                }


            }
            catch(Exception ex)
            {
                throw;

            }



        }

    }
}