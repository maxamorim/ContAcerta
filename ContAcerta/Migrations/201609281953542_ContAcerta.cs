namespace ContAcerta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContAcerta : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pedido", "Cliente", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pedido", "Cliente", c => c.String());
        }
    }
}
