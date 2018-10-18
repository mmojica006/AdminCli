using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FrontEnd
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error()
        {
            //Capturamos la excepción
            var ultimoError = Server.GetLastError();
            //Mandamos a mostrar el mensaje de error
            Response.Write("<script>alert('" + ultimoError.Message.Replace("'", "") + "')</script>");
            if (ultimoError.InnerException != null)
            {
                Response.Write("<script>alert('" + ultimoError.InnerException.Message.Replace("'", "") + "')</script>");
                if (ultimoError.InnerException.InnerException != null)
                    Response.Write("<script>alert('" + ultimoError.InnerException.InnerException.Message.Replace("'", "") + "')</script>");
            }
            //Limpiar los errores
            Server.ClearError();
        }
    }
}
