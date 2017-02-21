namespace ContAcerta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Atualizacao1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedido", "DcPedido", c => c.String());
            DropColumn("dbo.Pedido", "Cliente");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pedido", "Cliente", c => c.String(nullable: false));
            DropColumn("dbo.Pedido", "DcPedido");
        }
    }
}
