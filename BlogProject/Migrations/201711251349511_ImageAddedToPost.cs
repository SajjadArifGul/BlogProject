namespace BlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageAddedToPost : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Source = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Posts", "Image_ID", c => c.Int());
            CreateIndex("dbo.Posts", "Image_ID");
            AddForeignKey("dbo.Posts", "Image_ID", "dbo.Images", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "Image_ID", "dbo.Images");
            DropIndex("dbo.Posts", new[] { "Image_ID" });
            DropColumn("dbo.Posts", "Image_ID");
            DropTable("dbo.Images");
        }
    }
}
