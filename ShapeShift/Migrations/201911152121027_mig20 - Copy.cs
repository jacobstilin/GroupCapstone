namespace ShapeShift.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig20 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shifts", "LocationId", c => c.Int());
            CreateIndex("dbo.Shifts", "LocationId");
            AddForeignKey("dbo.Shifts", "LocationId", "dbo.Locations", "LocationId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shifts", "LocationId", "dbo.Locations");
            DropIndex("dbo.Shifts", new[] { "LocationId" });
            DropColumn("dbo.Shifts", "LocationId");
        }
    }
}
