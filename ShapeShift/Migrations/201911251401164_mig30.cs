namespace ShapeShift.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig30 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Locations", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Locations", new[] { "AddressId" });
            AlterColumn("dbo.Locations", "AddressId", c => c.Int());
            CreateIndex("dbo.Locations", "AddressId");
            AddForeignKey("dbo.Locations", "AddressId", "dbo.Addresses", "AddressId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Locations", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Locations", new[] { "AddressId" });
            AlterColumn("dbo.Locations", "AddressId", c => c.Int(nullable: false));
            CreateIndex("dbo.Locations", "AddressId");
            AddForeignKey("dbo.Locations", "AddressId", "dbo.Addresses", "AddressId", cascadeDelete: true);
        }
    }
}
