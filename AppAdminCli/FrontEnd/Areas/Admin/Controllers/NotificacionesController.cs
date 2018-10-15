using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Data.Entity;

namespace FrontEnd.Areas.Admin.Controllers
{
    public class NotificacionesController : Controller
    {
        private Notificacion notificacion = new Notificacion();
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
            //using (var ctx = new ContextoAplicacion())
            //{
            //    var usuarios = new SelectList(ctx.CTE_CUENTA_USUARIO.ToList());
            //}
            using (var ctx = new ContextoAplicacion())
            {
                var clientes = new SelectList(ctx.CTE_CUENTA_USUARIO.ToList(),"ID_CLIENTE","NOMBRE_USUARIO");
                ViewData["Clientes"] = clientes;

            }

         
                

            return View();

        }
        [HttpPost]
        public ActionResult Create(NotificacionViewModel model)
        {
            Notificacion notificacion = new Notificacion();
            CTE_NOTIFICACION_CLIENTE notificacionCliente = new CTE_NOTIFICACION_CLIENTE();

            if (ModelState.IsValid)
            {
                notificacion.NotificacionId = model.NotificacionId;
                notificacion.TITULO = model.TITULO;
                notificacion.CUERPO_NOTIFICACION = model.CUERPO_NOTIFICACION;
                notificacion.FECHA = model.FECHA;
                
                notificacionCliente.FECHA_LECTURA = model.FECHA;

                   











            }

            return null;

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