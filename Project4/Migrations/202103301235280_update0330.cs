namespace Project4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update0330 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LaoDongCongIches", "DangBiBenh", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Benhs", "PhamNhanID");
            AddForeignKey("dbo.Benhs", "PhamNhanID", "dbo.PhamNhans", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Benhs", "PhamNhanID", "dbo.PhamNhans");
            DropIndex("dbo.Benhs", new[] { "PhamNhanID" });
            DropColumn("dbo.LaoDongCongIches", "DangBiBenh");
        }
    }
}
