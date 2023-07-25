namespace Project4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateHihi : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.QuanNgucs", "NamCongTac", c => c.DateTime());
            AlterColumn("dbo.QuanNgucs", "ThoiHanCongTac", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.QuanNgucs", "ThoiHanCongTac", c => c.Int(nullable: false));
            AlterColumn("dbo.QuanNgucs", "NamCongTac", c => c.Int(nullable: false));
        }
    }
}
