namespace ShapeShift.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig17 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AppUsers", "TextMessage_Postion", "dbo.TextMessages");
            DropIndex("dbo.AppUsers", new[] { "TextMessage_Postion" });
            DropColumn("dbo.AppUsers", "TextMessage_Postion");
            DropTable("dbo.TextMessages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TextMessages",
                c => new
                    {
                        Postion = c.String(nullable: false, maxLength: 128),
                        NumberToSendTo = c.String(),
                        BodyOfMessage = c.String(),
                        id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Postion);
            
            AddColumn("dbo.AppUsers", "TextMessage_Postion", c => c.String(maxLength: 128));
            CreateIndex("dbo.AppUsers", "TextMessage_Postion");
            AddForeignKey("dbo.AppUsers", "TextMessage_Postion", "dbo.TextMessages", "Postion");
        }
    }
}
