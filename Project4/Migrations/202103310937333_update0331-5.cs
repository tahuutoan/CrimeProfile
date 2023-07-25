namespace Project4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update03315 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PhongGiams", "TenPhong", c => c.String());
            AlterColumn("dbo.QuanNgucs", "NamCongTac", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.QuanNgucs", "NamCongTac", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PhongGiams", "TenPhong", c => c.String(nullable: false));
        }
    }
}
