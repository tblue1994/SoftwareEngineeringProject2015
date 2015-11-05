namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Goals", "Timeline");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Goals", "Timeline", c => c.Int(nullable: false));
        }
    }
}
