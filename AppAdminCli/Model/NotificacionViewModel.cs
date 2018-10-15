using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string TITULO { get; set; }
        public string CUERPO_NOTIFICACION { get; set; }       
        public DateTime FECHA { get; set; }

        [Column("ID_CLIENTE")]
        public int CuentaUsuarioId { get; set; }
        public CuentaUsuario CuentaUsuario { get; set; }

        public string NOMBRE_USUARIO { get; set; }


    }
}
