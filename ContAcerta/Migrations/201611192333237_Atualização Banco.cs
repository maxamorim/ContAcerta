namespace ContAcerta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtualizaçãoBanco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TipoProduto",
                c => new
                    {
                        TipoProdutoId = c.Int(nullable: false, identity: true),
                        TipoProdutoDc = c.String(),
                        DtInclusao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TipoProdutoId);
            
            CreateTable(
                "dbo.TipoUsuario",
                c => new
                    {
                        TipoUsuarioId = c.Int(nullable: false, identity: true),
                        TipoUsuarioDc = c.String(),
                        DtInclusao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TipoUsuarioId);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        UsuarioDc = c.String(),
                        DtInclusao = c.DateTime(nullable: false),
                        TipoUsuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Usuario");
            DropTable("dbo.TipoUsuario");
            DropTable("dbo.TipoProduto");
        }
    }
}
