using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Data.Entity;
using Helpers;

namespace FrontEnd.Areas.Admin.Controllers
{
    public class NotificacionesController : Controller
    {
        ContextoAplicacion db = new ContextoAplicacion();
        FCMPushNotification fcmPush = new FCMPushNotification();

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
        [HttpPost]
    
        public ActionResult Create(NotificacionViewModel model)
        {
           
            DateTime fecha = DateTime.Now;
            var deviceKey = db.CTE_CUENTA_USUARIO.Where(x => x.CuentaUsuarioId == model.CuentaUsuarioId).Select(u => u.FCM_TOKEN).FirstOrDefault();

                if (ModelState.IsValid)
            {
                fcmPush.SendNotification(model.TITULO, model.CUERPO_NOTIFICACION, deviceKey); 
                            
                if (fcmPush.Successful)
                {

                
                        var notificacion = new Notificacion()
                        {
                         //notificacion.NotificacionId = model.NotificacionId;
                        TITULO =model.TITULO,
                        CUERPO_NOTIFICACION = model.CUERPO_NOTIFICACION,
                        PRIORIDAD = 3,
                        FECHA = fecha
                        };

                        var newNotification = db.CTE_NOTIFICACION.Add(notificacion);
                             db.Entry(notificacion).State = EntityState.Added;

                        var clienteNotificacion = new CTE_NOTIFICACION_CLIENTE()
                        {
                            CuentaUsuarioId = model.CuentaUsuarioId,
                            NotificacionId = newNotification.NotificacionId,
                            FECHA_LECTURA = fecha,
                            LEIDA = false
                        };

                        var newClienteNotifica = db.CTE_NOTIFICACION_CLIENTE.Add(clienteNotificacion);
                        db.Entry(clienteNotificacion).State = EntityState.Added;
                        db.SaveChanges();
                        return Json(new { success = true });

                }
                else
                {
                    var message = fcmPush.Error;


                }


            }
                var clientes = new SelectList(db.CTE_CUENTA_USUARIO.ToList(), "CuentaUsuarioId", "NOMBRE_USUARIO");
                ViewData["Clientes"] = clientes;

                return PartialView(model);
        



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