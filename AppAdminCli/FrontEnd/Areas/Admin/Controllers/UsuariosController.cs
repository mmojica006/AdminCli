using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Areas.Admin.Controllers
{
    public class UsuariosController : Controller
    {
        ContextoAplicacion db = new ContextoAplicacion();
        CuentaUsuario cliente = new CuentaUsuario();

        // GET: Admin/Usuarios
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult Listar(string term)
        {
            return Json(cliente.clienteAutocompletado(term),JsonRequestBehavior.AllowGet);
        }
    }
}