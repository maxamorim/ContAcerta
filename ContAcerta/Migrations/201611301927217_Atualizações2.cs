namespace ContAcerta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Atualizações2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pedido", "Valor", c => c.String(nullable: false));
            AlterColumn("dbo.Pedido", "ValorCredito", c => c.String());
            AlterColumn("dbo.Pedido", "ValorDebito", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pedido", "ValorDebito", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Pedido", "ValorCredito", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Pedido", "Valor", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
