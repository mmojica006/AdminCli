using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class NotificacionViewModel
    {
        [Key]
        [Column("ID_NOTIFICACION")]
     
        public int NotificacionId { get; set; }
        [DisplayName("Título")]
        public string TITULO { get; set; }
        [DisplayName("Mensaje")]
        public string CUERPO_NOTIFICACION { get; set; }       
        public DateTime FECHA { get; set; }

        [Column("ID_CLIENTE")]
        public decimal CuentaUsuarioId { get; set; }
   
        public CuentaUsuario CuentaUsuario { get; set; }
      
        public string NOMBRE_USUARIO { get; set; }
        public int GRUPO { get; set; }
        public ResponseModel Guardar(NotificacionViewModel model)
        {
            var rm = new ResponseModel();
            DateTime fecha = DateTime.Now;

            try
            {

                using (var ctx = new ContextoAplicacion())
                {


                    var notificacion = new Notificacion()
                    {
                        //notificacion.NotificacionId = model.NotificacionId;
                        TITULO = model.TITULO,
                        CUERPO_NOTIFICACION = model.CUERPO_NOTIFICACION,
                        PRIORIDAD = 3,
                        FECHA = fecha
                    };

                    var newNotification = ctx.CTE_NOTIFICACION.Add(notificacion);
                    ctx.Entry(notificacion).State = EntityState.Added;

                    var clienteNotificacion = new CTE_NOTIFICACION_CLIENTE()
                    {
                        CuentaUsuarioId = model.CuentaUsuarioId,
                        NotificacionId = newNotification.NotificacionId,
                        FECHA_LECTURA = fecha,
                        LEIDA = false
                    };

                    var newClienteNotifica = ctx.CTE_NOTIFICACION_CLIENTE.Add(clienteNotificacion);
                    ctx.Entry(clienteNotificacion).State = EntityState.Added;
                    ctx.SaveChanges();
                    //return Json(new { success = true });
                    rm.SetResponse(true);

                    //var clientes = new SelectList(db.CTE_CUENTA_USUARIO.ToList(), "CuentaUsuarioId", "NOMBRE_USUARIO");
                    //ViewData["Clientes"] = clientes;


                }

            }
            catch (Exception ex)
            {
                throw;
            }

            return rm;

        }

        public ResponseModel GuardarMasivo(NotificacionViewModel model)
        {
            var rm = new ResponseModel();
            DateTime fecha = DateTime.Now;

            try
            {

            }catch (Exception)
            {

            }

            return rm;



        }
    }

   

}
