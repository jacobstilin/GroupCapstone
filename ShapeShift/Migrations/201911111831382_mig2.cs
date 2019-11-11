namespace ShapeShift.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppUsers", "Email", c => c.String(nullable: false));
            AddColumn("dbo.AppUsers", "Password", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AppUsers", "ConfirmPassword", c => c.String());
            AddColumn("dbo.Organizations", "organizationName", c => c.String());
            AddColumn("dbo.Positions", "title", c => c.String());
            AddColumn("dbo.Shifts", "position", c => c.Int(nullable: false));
            AddColumn("dbo.Shifts", "start", c => c.DateTime());
            AddColumn("dbo.Shifts", "end", c => c.DateTime());
            AddColumn("dbo.Shifts", "duration", c => c.Double());
            AddColumn("dbo.Shifts", "additionalInfo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shifts", "additionalInfo");
            DropColumn("dbo.Shifts", "duration");
            DropColumn("dbo.Shifts", "end");
            DropColumn("dbo.Shifts", "start");
            DropColumn("dbo.Shifts", "position");
            DropColumn("dbo.Positions", "title");
            DropColumn("dbo.Organizations", "organizationName");
            DropColumn("dbo.AppUsers", "ConfirmPassword");
            DropColumn("dbo.AppUsers", "Password");
            DropColumn("dbo.AppUsers", "Email");
        }
    }
}
