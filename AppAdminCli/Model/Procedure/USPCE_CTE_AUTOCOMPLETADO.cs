using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Procedure
{
    public class USPCE_CTE_AUTOCOMPLETADO
    {   
        public decimal ID_CLIENTE { get; set; }
        public string NOMBRE { get; set; }
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
