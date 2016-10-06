namespace LaheyHealth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changecompletedatetonull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Participants", "CompleteDt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Participants", "CompleteDt", c => c.DateTime(nullable: false));
        }
    }
}
