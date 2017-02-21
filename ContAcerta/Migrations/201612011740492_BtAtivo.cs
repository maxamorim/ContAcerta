namespace ContAcerta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BtAtivo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedido", "BtAtivo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pedido", "BtAtivo");
        }
    }
}
