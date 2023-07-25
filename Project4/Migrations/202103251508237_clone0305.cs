namespace Project4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clone0305 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BanGiaoCongViecCuaQuanNgucNghis", "PhongID", "dbo.PhongGiams");
            DropIndex("dbo.BanGiaoCongViecCuaQuanNgucNghis", new[] { "PhongID" });
            AlterColumn("dbo.BanGiaoCongViecCuaQuanNgucNghis", "PhongID", c => c.Int(nullable: false));
            CreateIndex("dbo.BanGiaoCongViecCuaQuanNgucNghis", "PhongID");
            AddForeignKey("dbo.BanGiaoCongViecCuaQuanNgucNghis", "PhongID", "dbo.PhongGiams", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BanGiaoCongViecCuaQuanNgucNghis", "PhongID", "dbo.PhongGiams");
            DropIndex("dbo.BanGiaoCongViecCuaQuanNgucNghis", new[] { "PhongID" });
            AlterColumn("dbo.BanGiaoCongViecCuaQuanNgucNghis", "PhongID", c => c.Int());
            CreateIndex("dbo.BanGiaoCongViecCuaQuanNgucNghis", "PhongID");
            AddForeignKey("dbo.BanGiaoCongViecCuaQuanNgucNghis", "PhongID", "dbo.PhongGiams", "ID");
        }
    }
}
