using Model.Procedure;
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

        public List<Item> clienteAutocompletado(string busqueda)
        {

            var clientes = new List<USPCE_CTE_AUTOCOMPLETADO>();
            try
            {
                using (var ctx = new ContextoAplicacion())
                {
                    var data  = ctx.Database.SqlQuery<USPCE_CTE_AUTOCOMPLETADO>("USPCE_CTE_AUTOCOMPLETADO").ToList();

                    var consulta = from c in data
                                   where c.NOMBRE.Contains(busqueda.ToUpper())
                                   select new Item
                                   {
                                       id = c.ID_CLIENTE.ToString(),
                                       value = c.NOMBRE


                                   };

                    return consulta.ToList();

                }

            }
            catch (Exception)
            {
                throw;
            }


        }

       public List<USPCE_CTE_DASHBOARD> getDashboardUser()
        {      
            try
            {
                using (var ctx = new ContextoAplicacion())
                {
                    return ctx.Database.SqlQuery<USPCE_CTE_DASHBOARD>("USPCE_CTE_DASHBOARD").ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
