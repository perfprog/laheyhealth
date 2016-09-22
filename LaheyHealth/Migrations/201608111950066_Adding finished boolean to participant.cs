namespace LaheyHealth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addingfinishedbooleantoparticipant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Participants", "Finished", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Participants", "Finished");
        }
    }
}
