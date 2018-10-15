using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("CTE_NOTIFICACION")]
        public class Notificacion
    {
        [Key]
        [Column("ID_NOTIFICACION")]
        public int NotificacionId { get; set; }
        public string TITULO { get; set; }
        public string CUERPO_NOTIFICACION { get; set; }
        public int PRIORIDAD { get; set; }
        public DateTime FECHA { get; set; }


     public AnexGRIDResponde Listar(AnexGRID grid)
        {
            try
            {

                using (var ctx = new ContextoAplicacion())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    grid.Inicializar();

                    var query = ctx.CTE_NOTIFICACION.Where(x => x.NotificacionId == 5);
                    //var query = ctx.CTE_NOTIFICACION.ToList();
                    // Ordenamiento
                    if (grid.columna == "id")
                    {
                        query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.NotificacionId)
                                                             : query.OrderBy(x => x.NotificacionId);
                    }

                    if (grid.columna == "TITULO")
                    {
                        query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.TITULO)
                                                             : query.OrderBy(x => x.TITULO);
                    }

                    if (grid.columna == "FECHA")
                    {
                        query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.FECHA)
                                                             : query.OrderBy(x => x.FECHA);
                    }


                    var Notificacion = query.Skip(grid.pagina)
                                          .Take(grid.limite)
                                          .ToList();

                    var total = query.Count();

                    grid.SetData(Notificacion, total);
                

            }

            }catch(Exception ex)
            {
                throw;
            }

            return grid.responde();
        }


    }
}
