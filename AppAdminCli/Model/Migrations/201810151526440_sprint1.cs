namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sprint1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CTE_NOTIFICACION_CLIENTE", "ID_CLIENTE", "dbo.CTE_CUENTA_USUARIO");
            DropIndex("dbo.CTE_NOTIFICACION_CLIENTE", new[] { "ID_CLIENTE" });
            DropPrimaryKey("dbo.CTE_CUENTA_USUARIO");
            AlterColumn("dbo.CTE_CUENTA_USUARIO", "ID_CLIENTE", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CTE_NOTIFICACION_CLIENTE", "ID_CLIENTE", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddPrimaryKey("dbo.CTE_CUENTA_USUARIO", "ID_CLIENTE");
            CreateIndex("dbo.CTE_NOTIFICACION_CLIENTE", "ID_CLIENTE");
            AddForeignKey("dbo.CTE_NOTIFICACION_CLIENTE", "ID_CLIENTE", "dbo.CTE_CUENTA_USUARIO", "ID_CLIENTE", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CTE_NOTIFICACION_CLIENTE", "ID_CLIENTE", "dbo.CTE_CUENTA_USUARIO");
            DropIndex("dbo.CTE_NOTIFICACION_CLIENTE", new[] { "ID_CLIENTE" });
            DropPrimaryKey("dbo.CTE_CUENTA_USUARIO");
            AlterColumn("dbo.CTE_NOTIFICACION_CLIENTE", "ID_CLIENTE", c => c.Int(nullable: false));
            AlterColumn("dbo.CTE_CUENTA_USUARIO", "ID_CLIENTE", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CTE_CUENTA_USUARIO", "ID_CLIENTE");
            CreateIndex("dbo.CTE_NOTIFICACION_CLIENTE", "ID_CLIENTE");
            AddForeignKey("dbo.CTE_NOTIFICACION_CLIENTE", "ID_CLIENTE", "dbo.CTE_CUENTA_USUARIO", "ID_CLIENTE", cascadeDelete: true);
        }
    }
}
