namespace ContAcerta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Atualização : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Produto", "TpProduto", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Produto", "TpProduto", c => c.String(nullable: false));
        }
    }
}
