namespace ContAcerta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Atualizacao : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Produto", "DcProduto", c => c.String(nullable: false));
            AlterColumn("dbo.Produto", "TpProduto", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Produto", "TpProduto", c => c.String());
            AlterColumn("dbo.Produto", "DcProduto", c => c.String());
        }
    }
}
