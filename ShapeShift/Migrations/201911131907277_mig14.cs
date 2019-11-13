namespace ShapeShift.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppUsers", "phoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppUsers", "phoneNumber");
        }
    }
}
