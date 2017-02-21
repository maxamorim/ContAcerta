namespace ContAcerta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Atualizações : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedido", "ValorCredito", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Pedido", "ValorDebito", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Pedido", "TpPagamento", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pedido", "TpPagamento");
            DropColumn("dbo.Pedido", "ValorDebito");
            DropColumn("dbo.Pedido", "ValorCredito");
        }
    }
}
