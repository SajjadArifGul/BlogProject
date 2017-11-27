namespace BlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class summarytitlelength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Title", c => c.String(nullable: false, maxLength: 300));
            AlterColumn("dbo.Posts", "Summary", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Summary", c => c.String(maxLength: 200));
            AlterColumn("dbo.Posts", "Title", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
