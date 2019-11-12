namespace ShapeShift.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Shifts", "position", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Shifts", "position", c => c.Int(nullable: false));
        }
    }
}
