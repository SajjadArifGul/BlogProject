namespace BlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedRequiredOnPostAuthorTemporary : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "Author_ID", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "Author_ID" });
            AlterColumn("dbo.Posts", "Author_ID", c => c.Int());
            CreateIndex("dbo.Posts", "Author_ID");
            AddForeignKey("dbo.Posts", "Author_ID", "dbo.Users", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "Author_ID", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "Author_ID" });
            AlterColumn("dbo.Posts", "Author_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "Author_ID");
            AddForeignKey("dbo.Posts", "Author_ID", "dbo.Users", "ID", cascadeDelete: true);
        }
    }
}
