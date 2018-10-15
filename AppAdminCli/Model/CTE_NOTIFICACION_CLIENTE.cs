using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("CTE_NOTIFICACION_CLIENTE")]
    public class CTE_NOTIFICACION_CLIENTE
    {
        [Key]
        public int Id { get; set; }

        [Column("ID_NOTIFICACION")]
        [ForeignKey("Notificacion")]
        public int NotificacionId { get; set; }
        public  Notificacion Notificacion { get; set; }

        [Column("ID_CLIENTE")]
        [ForeignKey("CuentaUsuario")]
        public decimal CuentaUsuarioId { get; set; }
        public  CuentaUsuario CuentaUsuario { get; set; }
        public bool LEIDA { get; set; }
        public DateTime FECHA_LECTURA { get; set; }


    }
}
