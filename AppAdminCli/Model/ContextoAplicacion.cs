using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ContextoAplicacion : DbContext
    {

        public ContextoAplicacion():base("name=DefaultConnection")
        {
            //Habilitar las migraciones
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ContextoAplicacion, Migrations.Configuration>("DefaultConnection"));

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CuentaUsuario> CTE_CUENTA_USUARIO { get; set; }
        public DbSet<Notificacion> CTE_NOTIFICACION { get; set; }
        public DbSet<CTE_NOTIFICACION_CLIENTE> CTE_NOTIFICACION_CLIENTE { get; set; }

    }
}
