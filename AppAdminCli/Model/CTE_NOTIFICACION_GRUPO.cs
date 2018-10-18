using Model.Procedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public  class CTE_NOTIFICACION_GRUPO
    {
        public int ID { get; set; }
        public string NOMBRE { get; set; }
        public DateTime FECHA { get; set; }
        public bool ESTADO { get; set; }
        public string OBSERVACION { get; set; }


        public List<CTE_NOTIFICACION_GRUPO> Listar()
        {
            var grupo = new List<CTE_NOTIFICACION_GRUPO>();

            try
            {
                using (var ctx = new ContextoAplicacion())
                {
                    grupo = ctx.CTE_NOTIFICACION_GRUPO.ToList();
                }

            }
            catch (Exception)
            {
                throw;

            }
            return grupo;

        }

        public List<USPCE_CTE_GRUPOS> ListarEstadisticas()
        {
            try
            {
                using (var ctx = new ContextoAplicacion())
                {
                    var ord =  ctx.Database.SqlQuery<USPCE_CTE_GRUPOS>("USPCE_CTE_GRUPOS @p0", "TODOS").ToList();

                    return ord;

                }

            }
            catch (Exception)
            {
                throw;
            }
       

        } 

    }

   





}
