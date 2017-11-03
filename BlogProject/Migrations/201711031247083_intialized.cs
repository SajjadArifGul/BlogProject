namespace BlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intialized : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Country_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Countries", t => t.Country_ID, cascadeDelete: true)
                .Index(t => t.Country_ID);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false),
                        PublishedTime = c.DateTime(nullable: false),
                        Author_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.Author_ID, cascadeDelete: true)
                .Index(t => t.Author_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 13),
                        Gender = c.String(),
                        isStudent = c.Boolean(nullable: false),
                        isFullTimeJob = c.Boolean(nullable: false),
                        isPartTimeJob = c.Boolean(nullable: false),
                        AddressDetails = c.String(maxLength: 250),
                        City_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cities", t => t.City_ID, cascadeDelete: true)
                .Index(t => t.City_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "Author_ID", "dbo.Users");
            DropForeignKey("dbo.Users", "City_ID", "dbo.Cities");
            DropForeignKey("dbo.Cities", "Country_ID", "dbo.Countries");
            DropIndex("dbo.Users", new[] { "City_ID" });
            DropIndex("dbo.Posts", new[] { "Author_ID" });
            DropIndex("dbo.Cities", new[] { "Country_ID" });
            DropTable("dbo.Users");
            DropTable("dbo.Posts");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
        }
    }
}
