namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_midle_name : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "MidleName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "MidleName");
        }
    }
}
