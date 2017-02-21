namespace ContAcerta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Atualizações1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pedido", "ValorCredito", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Pedido", "ValorDebito", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pedido", "ValorDebito", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Pedido", "ValorCredito", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
