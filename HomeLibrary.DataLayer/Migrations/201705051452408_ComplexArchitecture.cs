namespace HomeLibrary.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComplexArchitecture : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                    })
                .PrimaryKey(t => t.AuthorId);
            
            CreateTable(
                "dbo.Conditions",
                c => new
                    {
                        ConditionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ConditionId);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        PublisherId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PublisherId);
            
            AddColumn("dbo.Books", "PublishedYear", c => c.Int(nullable: false));
            AddColumn("dbo.Books", "Author_AuthorId", c => c.Int());
            AddColumn("dbo.Books", "Condition_ConditionId", c => c.Int());
            AddColumn("dbo.Books", "Publisher_PublisherId", c => c.Int());
            CreateIndex("dbo.Books", "Author_AuthorId");
            CreateIndex("dbo.Books", "Condition_ConditionId");
            CreateIndex("dbo.Books", "Publisher_PublisherId");
            AddForeignKey("dbo.Books", "Author_AuthorId", "dbo.Authors", "AuthorId");
            AddForeignKey("dbo.Books", "Condition_ConditionId", "dbo.Conditions", "ConditionId");
            AddForeignKey("dbo.Books", "Publisher_PublisherId", "dbo.Publishers", "PublisherId");
            DropColumn("dbo.Books", "Author");
            DropColumn("dbo.Books", "Localisation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Localisation", c => c.String());
            AddColumn("dbo.Books", "Author", c => c.String());
            DropForeignKey("dbo.Books", "Publisher_PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.Books", "Condition_ConditionId", "dbo.Conditions");
            DropForeignKey("dbo.Books", "Author_AuthorId", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "Publisher_PublisherId" });
            DropIndex("dbo.Books", new[] { "Condition_ConditionId" });
            DropIndex("dbo.Books", new[] { "Author_AuthorId" });
            DropColumn("dbo.Books", "Publisher_PublisherId");
            DropColumn("dbo.Books", "Condition_ConditionId");
            DropColumn("dbo.Books", "Author_AuthorId");
            DropColumn("dbo.Books", "PublishedYear");
            DropTable("dbo.Publishers");
            DropTable("dbo.Conditions");
            DropTable("dbo.Authors");
        }
    }
}
