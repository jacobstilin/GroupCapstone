namespace ShapeShift.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig111 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Availabilities", "AppUser_UserId", "dbo.AppUsers");
            DropForeignKey("dbo.Positions", "AppUser_UserId", "dbo.AppUsers");
            DropIndex("dbo.Availabilities", new[] { "AppUser_UserId" });
            DropIndex("dbo.Positions", new[] { "AppUser_UserId" });
            RenameColumn(table: "dbo.Availabilities", name: "AppUser_UserId", newName: "UserId");
            RenameColumn(table: "dbo.Positions", name: "AppUser_UserId", newName: "UserId");
            AlterColumn("dbo.Availabilities", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Positions", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Availabilities", "UserId");
            CreateIndex("dbo.Positions", "UserId");
            AddForeignKey("dbo.Availabilities", "UserId", "dbo.AppUsers", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Positions", "UserId", "dbo.AppUsers", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Positions", "UserId", "dbo.AppUsers");
            DropForeignKey("dbo.Availabilities", "UserId", "dbo.AppUsers");
            DropIndex("dbo.Positions", new[] { "UserId" });
            DropIndex("dbo.Availabilities", new[] { "UserId" });
            AlterColumn("dbo.Positions", "UserId", c => c.Int());
            AlterColumn("dbo.Availabilities", "UserId", c => c.Int());
            RenameColumn(table: "dbo.Positions", name: "UserId", newName: "AppUser_UserId");
            RenameColumn(table: "dbo.Availabilities", name: "UserId", newName: "AppUser_UserId");
            CreateIndex("dbo.Positions", "AppUser_UserId");
            CreateIndex("dbo.Availabilities", "AppUser_UserId");
            AddForeignKey("dbo.Positions", "AppUser_UserId", "dbo.AppUsers", "UserId");
            AddForeignKey("dbo.Availabilities", "AppUser_UserId", "dbo.AppUsers", "UserId");
        }
    }
}
