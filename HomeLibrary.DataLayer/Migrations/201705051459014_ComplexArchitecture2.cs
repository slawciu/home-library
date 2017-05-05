namespace HomeLibrary.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComplexArchitecture2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookCopies",
                c => new
                    {
                        BookCopyId = c.Int(nullable: false, identity: true),
                        Book_Id = c.Int(),
                        Holder_UserId = c.Int(),
                        Localisation_LocalisationId = c.Int(),
                    })
                .PrimaryKey(t => t.BookCopyId)
                .ForeignKey("dbo.Books", t => t.Book_Id)
                .ForeignKey("dbo.Users", t => t.Holder_UserId)
                .ForeignKey("dbo.Localisations", t => t.Localisation_LocalisationId)
                .Index(t => t.Book_Id)
                .Index(t => t.Holder_UserId)
                .Index(t => t.Localisation_LocalisationId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        FacebookId = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Localisations",
                c => new
                    {
                        LocalisationId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.LocalisationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookCopies", "Localisation_LocalisationId", "dbo.Localisations");
            DropForeignKey("dbo.BookCopies", "Holder_UserId", "dbo.Users");
            DropForeignKey("dbo.BookCopies", "Book_Id", "dbo.Books");
            DropIndex("dbo.BookCopies", new[] { "Localisation_LocalisationId" });
            DropIndex("dbo.BookCopies", new[] { "Holder_UserId" });
            DropIndex("dbo.BookCopies", new[] { "Book_Id" });
            DropTable("dbo.Localisations");
            DropTable("dbo.Users");
            DropTable("dbo.BookCopies");
        }
    }
}
