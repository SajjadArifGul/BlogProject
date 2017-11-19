namespace BlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSummaryToPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Summary", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Summary");
        }
    }
}
