namespace ShapeShift.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig19 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Locations", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Availabilities", "UserId", "dbo.AppUsers");
            DropIndex("dbo.Locations", new[] { "OrganizationId" });
            AddColumn("dbo.Availabilities", "AppUser_UserId", c => c.Int());
            AddColumn("dbo.Availabilities", "AppUser_UserId1", c => c.Int());
            AddColumn("dbo.Locations", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Availabilities", "AppUser_UserId");
            CreateIndex("dbo.Availabilities", "AppUser_UserId1");
            CreateIndex("dbo.Locations", "UserId");
            AddForeignKey("dbo.Availabilities", "AppUser_UserId1", "dbo.AppUsers", "UserId");
            AddForeignKey("dbo.Locations", "UserId", "dbo.AppUsers", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Availabilities", "AppUser_UserId", "dbo.AppUsers", "UserId");
            DropColumn("dbo.Locations", "OrganizationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Locations", "OrganizationId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Availabilities", "AppUser_UserId", "dbo.AppUsers");
            DropForeignKey("dbo.Locations", "UserId", "dbo.AppUsers");
            DropForeignKey("dbo.Availabilities", "AppUser_UserId1", "dbo.AppUsers");
            DropIndex("dbo.Locations", new[] { "UserId" });
            DropIndex("dbo.Availabilities", new[] { "AppUser_UserId1" });
            DropIndex("dbo.Availabilities", new[] { "AppUser_UserId" });
            DropColumn("dbo.Locations", "UserId");
            DropColumn("dbo.Availabilities", "AppUser_UserId1");
            DropColumn("dbo.Availabilities", "AppUser_UserId");
            CreateIndex("dbo.Locations", "OrganizationId");
            AddForeignKey("dbo.Availabilities", "UserId", "dbo.AppUsers", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Locations", "OrganizationId", "dbo.Organizations", "OrganizationId", cascadeDelete: true);
        }
    }
}
