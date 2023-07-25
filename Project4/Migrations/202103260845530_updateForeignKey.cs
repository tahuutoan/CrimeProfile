namespace Project4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateForeignKey : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnXas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PhamNhanID = c.Guid(nullable: false),
                        MucDoAnXa = c.Int(nullable: false),
                        MucDoCaiTao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PhamNhans", t => t.PhamNhanID, cascadeDelete: true)
                .Index(t => t.PhamNhanID);
            
            CreateTable(
                "dbo.PhamNhans",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        TenPhamNhan = c.String(),
                        BiDanh = c.String(),
                        AnhNhanDien = c.String(),
                        QueQuan = c.String(),
                        NgaySinh = c.DateTime(),
                        GioiTinh = c.Int(nullable: false),
                        IDKhu = c.Int(nullable: false),
                        ToiDanh = c.Int(nullable: false),
                        MucDoNguyHiem = c.Int(nullable: false),
                        SoNgayGiamGiu = c.Int(nullable: false),
                        CMND = c.String(),
                        QuaTrinhGayAn = c.String(),
                        DiaDiemGayAn = c.String(),
                        PhongGiamID = c.Int(nullable: false),
                        NgayVaoTrai = c.DateTime(),
                        Khu_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Khus", t => t.Khu_ID)
                .ForeignKey("dbo.PhongGiams", t => t.PhongGiamID, cascadeDelete: true)
                .Index(t => t.PhongGiamID)
                .Index(t => t.Khu_ID);
            
            CreateTable(
                "dbo.Khus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenKhu = c.String(),
                        QuanNgucID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PhongGiams",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenPhong = c.String(),
                        KhuID = c.Int(nullable: false),
                        SoLuongPhamNhanMax = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BanGiaoCongViecCuaQuanNgucNghis",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QuanNgucNhanID = c.Guid(nullable: false),
                        NgayNhan = c.DateTime(nullable: false),
                        PhongID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PhongGiams", t => t.PhongID, cascadeDelete: true)
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
            
            CreateTable(
                "dbo.Benhs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PhamNhanID = c.Guid(nullable: false),
                        NgayChuaTri = c.Int(nullable: false),
                        NgayBatDauChuaTri = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CheDoNghiPhepCuaQuanNgucs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QuanNgucID = c.Guid(nullable: false),
                        SoNgayNghi = c.Int(nullable: false),
                        LyDoNghi = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.QuanNgucs", t => t.QuanNgucID, cascadeDelete: true)
                .Index(t => t.QuanNgucID);
            
            CreateTable(
                "dbo.QuanNgucs",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        TenQuanNguc = c.String(),
                        AnhNhanDien = c.String(),
                        NgaySinh = c.DateTime(),
                        QueQuan = c.String(),
                        GioiTinh = c.Int(nullable: false),
                        KhuID = c.Int(nullable: false),
                        NamCongTac = c.Int(nullable: false),
                        ThoiHanCongTac = c.Int(nullable: false),
                        CMND = c.String(),
                        ChucVu = c.Int(nullable: false),
                        QuanHam = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Khus", t => t.KhuID, cascadeDelete: true)
                .Index(t => t.KhuID);
            
            CreateTable(
                "dbo.LaoDongCongIches",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PhamNhanID = c.Guid(nullable: false),
                        QuanNgucID = c.Guid(nullable: false),
                        KhuVucLamViec = c.Int(nullable: false),
                        BieuHien = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PhamNhans", t => t.PhamNhanID, cascadeDelete: true)
                .ForeignKey("dbo.QuanNgucs", t => t.QuanNgucID, cascadeDelete: true)
                .Index(t => t.PhamNhanID)
                .Index(t => t.QuanNgucID);
            
            CreateTable(
                "dbo.QuanLyBenhTats",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PhamNhanID = c.Guid(nullable: false),
                        BenhID = c.Int(nullable: false),
                        ThoiGianDieuTri = c.Int(nullable: false),
                        NoiDieuTri = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ThamGuis",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PhamNhanID = c.Guid(nullable: false),
                        QuanNgucID = c.Guid(nullable: false),
                        KeHoachThamGui = c.Int(nullable: false),
                        NgayThamGui = c.DateTime(nullable: false),
                        ThongTinNguoiThamHoi = c.String(),
                        SoLanThamHoi = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PhamNhans", t => t.PhamNhanID, cascadeDelete: true)
                .ForeignKey("dbo.QuanNgucs", t => t.QuanNgucID, cascadeDelete: true)
                .Index(t => t.PhamNhanID)
                .Index(t => t.QuanNgucID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        QuanNgucID = c.Guid(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ThamGuis", "QuanNgucID", "dbo.QuanNgucs");
            DropForeignKey("dbo.ThamGuis", "PhamNhanID", "dbo.PhamNhans");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.LaoDongCongIches", "QuanNgucID", "dbo.QuanNgucs");
            DropForeignKey("dbo.LaoDongCongIches", "PhamNhanID", "dbo.PhamNhans");
            DropForeignKey("dbo.CheDoNghiPhepCuaQuanNgucs", "QuanNgucID", "dbo.QuanNgucs");
            DropForeignKey("dbo.QuanNgucs", "KhuID", "dbo.Khus");
            DropForeignKey("dbo.BanGiaoPhamNhans", "PhongID", "dbo.PhongGiams");
            DropForeignKey("dbo.BanGiaoCongViecCuaQuanNgucNghis", "PhongID", "dbo.PhongGiams");
            DropForeignKey("dbo.AnXas", "PhamNhanID", "dbo.PhamNhans");
            DropForeignKey("dbo.PhamNhans", "PhongGiamID", "dbo.PhongGiams");
            DropForeignKey("dbo.PhamNhans", "Khu_ID", "dbo.Khus");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ThamGuis", new[] { "QuanNgucID" });
            DropIndex("dbo.ThamGuis", new[] { "PhamNhanID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.LaoDongCongIches", new[] { "QuanNgucID" });
            DropIndex("dbo.LaoDongCongIches", new[] { "PhamNhanID" });
            DropIndex("dbo.QuanNgucs", new[] { "KhuID" });
            DropIndex("dbo.CheDoNghiPhepCuaQuanNgucs", new[] { "QuanNgucID" });
            DropIndex("dbo.BanGiaoPhamNhans", new[] { "PhongID" });
            DropIndex("dbo.BanGiaoCongViecCuaQuanNgucNghis", new[] { "PhongID" });
            DropIndex("dbo.PhamNhans", new[] { "Khu_ID" });
            DropIndex("dbo.PhamNhans", new[] { "PhongGiamID" });
            DropIndex("dbo.AnXas", new[] { "PhamNhanID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ThamGuis");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.QuanLyBenhTats");
            DropTable("dbo.LaoDongCongIches");
            DropTable("dbo.QuanNgucs");
            DropTable("dbo.CheDoNghiPhepCuaQuanNgucs");
            DropTable("dbo.Benhs");
            DropTable("dbo.BanGiaoPhamNhans");
            DropTable("dbo.BanGiaoCongViecCuaQuanNgucNghis");
            DropTable("dbo.PhongGiams");
            DropTable("dbo.Khus");
            DropTable("dbo.PhamNhans");
            DropTable("dbo.AnXas");
        }
    }
}
