namespace Project4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class code03252021 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BanGiaoCongViecCuaQuanNgucNghis",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QuanNgucNhanID = c.Guid(nullable: false),
                        NgayNhan = c.DateTime(nullable: false),
                        PhongID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PhongGiams", t => t.PhongID)
                .Index(t => t.PhongID);
            
            CreateTable(
                "dbo.BanGiaoPhamNhans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QuanNgucNghiID = c.Guid(nullable: false),
                        QuanNgucNhanID = c.Guid(nullable: false),
                        NgayNhan = c.DateTime(nullable: false),
                        SoNgayBanGiao = c.Int(nullable: false),
                        PhongID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PhongGiams", t => t.PhongID)
                .Index(t => t.PhongID);
            
            AddColumn("dbo.Benhs", "NgayBatDauChuaTri", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BanGiaoPhamNhans", "PhongID", "dbo.PhongGiams");
            DropForeignKey("dbo.BanGiaoCongViecCuaQuanNgucNghis", "PhongID", "dbo.PhongGiams");
            DropIndex("dbo.BanGiaoPhamNhans", new[] { "PhongID" });
            DropIndex("dbo.BanGiaoCongViecCuaQuanNgucNghis", new[] { "PhongID" });
            DropColumn("dbo.Benhs", "NgayBatDauChuaTri");
            DropTable("dbo.BanGiaoPhamNhans");
            DropTable("dbo.BanGiaoCongViecCuaQuanNgucNghis");
        }
    }
}
