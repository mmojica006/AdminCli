using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("CTE_CUENTA_USUARIO")]
    [DisplayName("Usuario")]
    public class CuentaUsuario
    {
        [Key]
        [Column("ID_CLIENTE")]
        public decimal CuentaUsuarioId { get; set; }
        public string NOMBRE_USUARIO { get; set; }
        public string TEMP_PASS_PART1 { get; set; }
        public string TEMP_PASS_PART2 { get; set; }
        public string CLAVE { get; set; }
        public bool BLOQUEADO { get; set; }
        public string MOTIVO_BLOQUEO { get; set; }
        public DateTime FECHA_CREACION { get; set; }
        public string FCM_TOKEN { get; set; }




    }
}
