namespace Project4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update0331part4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PhamNhans", "TenPhamNhan", c => c.String(nullable: false));
            AlterColumn("dbo.PhamNhans", "BiDanh", c => c.String(nullable: false));
            AlterColumn("dbo.PhamNhans", "QueQuan", c => c.String(nullable: false));
            AlterColumn("dbo.PhamNhans", "CMND", c => c.String(nullable: false));
            AlterColumn("dbo.PhamNhans", "QuaTrinhGayAn", c => c.String(nullable: false));
            AlterColumn("dbo.PhamNhans", "DiaDiemGayAn", c => c.String(nullable: false));
            AlterColumn("dbo.PhongGiams", "TenPhong", c => c.String(nullable: false));
            AlterColumn("dbo.CheDoNghiPhepCuaQuanNgucs", "LyDoNghi", c => c.String(nullable: false));
            AlterColumn("dbo.QuanNgucs", "TenQuanNguc", c => c.String(nullable: false));
            AlterColumn("dbo.QuanNgucs", "QueQuan", c => c.String(nullable: false));
            AlterColumn("dbo.QuanNgucs", "NamCongTac", c => c.DateTime(nullable: false));
            AlterColumn("dbo.QuanNgucs", "CMND", c => c.String(nullable: false));
            AlterColumn("dbo.ThamGuis", "ThongTinNguoiThamHoi", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ThamGuis", "ThongTinNguoiThamHoi", c => c.String());
            AlterColumn("dbo.QuanNgucs", "CMND", c => c.String());
            AlterColumn("dbo.QuanNgucs", "NamCongTac", c => c.DateTime());
            AlterColumn("dbo.QuanNgucs", "QueQuan", c => c.String());
            AlterColumn("dbo.QuanNgucs", "TenQuanNguc", c => c.String());
            AlterColumn("dbo.CheDoNghiPhepCuaQuanNgucs", "LyDoNghi", c => c.String());
            AlterColumn("dbo.PhongGiams", "TenPhong", c => c.String());
            AlterColumn("dbo.PhamNhans", "DiaDiemGayAn", c => c.String());
            AlterColumn("dbo.PhamNhans", "QuaTrinhGayAn", c => c.String());
            AlterColumn("dbo.PhamNhans", "CMND", c => c.String());
            AlterColumn("dbo.PhamNhans", "QueQuan", c => c.String());
            AlterColumn("dbo.PhamNhans", "BiDanh", c => c.String());
            AlterColumn("dbo.PhamNhans", "TenPhamNhan", c => c.String());
        }
    }
}
