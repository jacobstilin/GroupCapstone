namespace ShapeShift.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AppUsers", "Email");
            DropColumn("dbo.AppUsers", "Password");
            DropColumn("dbo.AppUsers", "ConfirmPassword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppUsers", "ConfirmPassword", c => c.String());
            AddColumn("dbo.AppUsers", "Password", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AppUsers", "Email", c => c.String(nullable: false));
        }
    }
}
