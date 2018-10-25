using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        CTE_NOTIFICACION_GRUPO cTE_NOTIFICACION_GRUPO = new CTE_NOTIFICACION_GRUPO();
        ContextoAplicacion db = new ContextoAplicacion();
        // GET: Admin/Home
        public ActionResult Index()
        {
            CuentaUsuario cuentaUsuario = new CuentaUsuario();
            ViewData["estadisticas"] = cuentaUsuario.getDashboardUser();

            return View();
        }
        public ActionResult GetData()
        {
            //var data = new[] { new Entry() { value = 20, year = 2008 }, new Entry() { value = 10, year = 2009 } };

            var data = cTE_NOTIFICACION_GRUPO.ListarEstadisticas("TODOS");

            var result = from c in data
                         orderby c.ESTADO
                         group c by c.ESTADO into g
                         select new
                         {
                             key = g.Key,
                             cnt = g.Count()
                         };



            var dataGroup = data.GroupBy(x => x.ESTADO).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public class Entry
        {
            public int year { get; set; }
            public int value { get; set; }
        }
    }
}