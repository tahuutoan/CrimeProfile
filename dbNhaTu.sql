USE [master]
GO
/****** Object:  Database [NhaTu]    Script Date: 4/1/2021 10:09:57 AM ******/
CREATE DATABASE [NhaTu]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NhaTu', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\NhaTu.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NhaTu_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\NhaTu_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [NhaTu] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NhaTu].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NhaTu] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NhaTu] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NhaTu] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NhaTu] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NhaTu] SET ARITHABORT OFF 
GO
ALTER DATABASE [NhaTu] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [NhaTu] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NhaTu] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NhaTu] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NhaTu] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NhaTu] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NhaTu] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NhaTu] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NhaTu] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NhaTu] SET  ENABLE_BROKER 
GO
ALTER DATABASE [NhaTu] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NhaTu] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NhaTu] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NhaTu] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NhaTu] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NhaTu] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [NhaTu] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NhaTu] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [NhaTu] SET  MULTI_USER 
GO
ALTER DATABASE [NhaTu] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NhaTu] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NhaTu] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NhaTu] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NhaTu] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NhaTu] SET QUERY_STORE = OFF
GO
USE [NhaTu]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 4/1/2021 10:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AnXas]    Script Date: 4/1/2021 10:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnXas](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PhamNhanID] [uniqueidentifier] NOT NULL,
	[MucDoAnXa] [int] NOT NULL,
	[MucDoCaiTao] [int] NOT NULL,
 CONSTRAINT [PK_dbo.AnXas] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 4/1/2021 10:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 4/1/2021 10:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 4/1/2021 10:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 4/1/2021 10:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 4/1/2021 10:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[QuanNgucID] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BanGiaoCongViecCuaQuanNgucNghis]    Script Date: 4/1/2021 10:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BanGiaoCongViecCuaQuanNgucNghis](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[QuanNgucNhanID] [uniqueidentifier] NOT NULL,
	[NgayNhan] [datetime] NOT NULL,
	[PhongID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.BanGiaoCongViecCuaQuanNgucNghis] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BanGiaoPhamNhans]    Script Date: 4/1/2021 10:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BanGiaoPhamNhans](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[QuanNgucNghiID] [uniqueidentifier] NOT NULL,
	[QuanNgucNhanID] [uniqueidentifier] NOT NULL,
	[NgayNhan] [datetime] NOT NULL,
	[SoNgayBanGiao] [int] NOT NULL,
	[PhongID] [int] NULL,
 CONSTRAINT [PK_dbo.BanGiaoPhamNhans] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Benhs]    Script Date: 4/1/2021 10:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Benhs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PhamNhanID] [uniqueidentifier] NOT NULL,
	[NgayChuaTri] [int] NOT NULL,
	[NgayBatDauChuaTri] [datetime] NULL,
 CONSTRAINT [PK_dbo.Benhs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CheDoNghiPhepCuaQuanNgucs]    Script Date: 4/1/2021 10:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CheDoNghiPhepCuaQuanNgucs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[QuanNgucID] [uniqueidentifier] NOT NULL,
	[SoNgayNghi] [int] NOT NULL,
	[LyDoNghi] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.CheDoNghiPhepCuaQuanNgucs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Khus]    Script Date: 4/1/2021 10:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Khus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenKhu] [nvarchar](max) NULL,
	[QuanNgucID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.Khus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LaoDongCongIches]    Script Date: 4/1/2021 10:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LaoDongCongIches](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PhamNhanID] [uniqueidentifier] NOT NULL,
	[QuanNgucID] [uniqueidentifier] NOT NULL,
	[KhuVucLamViec] [int] NOT NULL,
	[BieuHien] [int] NOT NULL,
	[DangBiBenh] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.LaoDongCongIches] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhamNhans]    Script Date: 4/1/2021 10:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhamNhans](
	[ID] [uniqueidentifier] NOT NULL,
	[TenPhamNhan] [nvarchar](max) NOT NULL,
	[BiDanh] [nvarchar](max) NOT NULL,
	[AnhNhanDien] [nvarchar](max) NULL,
	[QueQuan] [nvarchar](max) NOT NULL,
	[NgaySinh] [datetime] NULL,
	[GioiTinh] [int] NOT NULL,
	[IDKhu] [int] NOT NULL,
	[ToiDanh] [int] NOT NULL,
	[MucDoNguyHiem] [int] NOT NULL,
	[SoNgayGiamGiu] [int] NOT NULL,
	[CMND] [nvarchar](max) NOT NULL,
	[QuaTrinhGayAn] [nvarchar](max) NOT NULL,
	[DiaDiemGayAn] [nvarchar](max) NOT NULL,
	[PhongGiamID] [int] NOT NULL,
	[NgayVaoTrai] [datetime] NULL,
	[Khu_ID] [int] NULL,
 CONSTRAINT [PK_dbo.PhamNhans] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhongGiams]    Script Date: 4/1/2021 10:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhongGiams](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenPhong] [nvarchar](max) NULL,
	[KhuID] [int] NOT NULL,
	[SoLuongPhamNhanMax] [int] NOT NULL,
 CONSTRAINT [PK_dbo.PhongGiams] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuanLyBenhTats]    Script Date: 4/1/2021 10:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuanLyBenhTats](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PhamNhanID] [uniqueidentifier] NOT NULL,
	[BenhID] [int] NOT NULL,
	[ThoiGianDieuTri] [int] NOT NULL,
	[NoiDieuTri] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.QuanLyBenhTats] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuanNgucs]    Script Date: 4/1/2021 10:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuanNgucs](
	[ID] [uniqueidentifier] NOT NULL,
	[TenQuanNguc] [nvarchar](max) NOT NULL,
	[AnhNhanDien] [nvarchar](max) NULL,
	[NgaySinh] [datetime] NULL,
	[QueQuan] [nvarchar](max) NOT NULL,
	[GioiTinh] [int] NOT NULL,
	[KhuID] [int] NOT NULL,
	[NamCongTac] [datetime] NULL,
	[ThoiHanCongTac] [datetime] NULL,
	[CMND] [nvarchar](max) NOT NULL,
	[ChucVu] [int] NOT NULL,
	[QuanHam] [int] NOT NULL,
 CONSTRAINT [PK_dbo.QuanNgucs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThamGuis]    Script Date: 4/1/2021 10:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThamGuis](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PhamNhanID] [uniqueidentifier] NOT NULL,
	[QuanNgucID] [uniqueidentifier] NOT NULL,
	[KeHoachThamGui] [int] NOT NULL,
	[NgayThamGui] [datetime] NULL,
	[ThongTinNguoiThamHoi] [nvarchar](max) NOT NULL,
	[SoLanThamHoi] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ThamGuis] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_PhamNhanID]    Script Date: 4/1/2021 10:09:58 AM ******/
CREATE NONCLUSTERED INDEX [IX_PhamNhanID] ON [dbo].[AnXas]
(
	[PhamNhanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 4/1/2021 10:09:58 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 4/1/2021 10:09:58 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 4/1/2021 10:09:58 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 4/1/2021 10:09:58 AM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 4/1/2021 10:09:58 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 4/1/2021 10:09:58 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PhongID]    Script Date: 4/1/2021 10:09:58 AM ******/
CREATE NONCLUSTERED INDEX [IX_PhongID] ON [dbo].[BanGiaoCongViecCuaQuanNgucNghis]
(
	[PhongID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PhongID]    Script Date: 4/1/2021 10:09:58 AM ******/
CREATE NONCLUSTERED INDEX [IX_PhongID] ON [dbo].[BanGiaoPhamNhans]
(
	[PhongID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PhamNhanID]    Script Date: 4/1/2021 10:09:58 AM ******/
CREATE NONCLUSTERED INDEX [IX_PhamNhanID] ON [dbo].[Benhs]
(
	[PhamNhanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_QuanNgucID]    Script Date: 4/1/2021 10:09:58 AM ******/
CREATE NONCLUSTERED INDEX [IX_QuanNgucID] ON [dbo].[CheDoNghiPhepCuaQuanNgucs]
(
	[QuanNgucID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PhamNhanID]    Script Date: 4/1/2021 10:09:58 AM ******/
CREATE NONCLUSTERED INDEX [IX_PhamNhanID] ON [dbo].[LaoDongCongIches]
(
	[PhamNhanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_QuanNgucID]    Script Date: 4/1/2021 10:09:58 AM ******/
CREATE NONCLUSTERED INDEX [IX_QuanNgucID] ON [dbo].[LaoDongCongIches]
(
	[QuanNgucID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Khu_ID]    Script Date: 4/1/2021 10:09:58 AM ******/
CREATE NONCLUSTERED INDEX [IX_Khu_ID] ON [dbo].[PhamNhans]
(
	[Khu_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PhongGiamID]    Script Date: 4/1/2021 10:09:58 AM ******/
CREATE NONCLUSTERED INDEX [IX_PhongGiamID] ON [dbo].[PhamNhans]
(
	[PhongGiamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_KhuID]    Script Date: 4/1/2021 10:09:58 AM ******/
CREATE NONCLUSTERED INDEX [IX_KhuID] ON [dbo].[QuanNgucs]
(
	[KhuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PhamNhanID]    Script Date: 4/1/2021 10:09:58 AM ******/
CREATE NONCLUSTERED INDEX [IX_PhamNhanID] ON [dbo].[ThamGuis]
(
	[PhamNhanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_QuanNgucID]    Script Date: 4/1/2021 10:09:58 AM ******/
CREATE NONCLUSTERED INDEX [IX_QuanNgucID] ON [dbo].[ThamGuis]
(
	[QuanNgucID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LaoDongCongIches] ADD  DEFAULT ((0)) FOR [DangBiBenh]
GO
ALTER TABLE [dbo].[AnXas]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AnXas_dbo.PhamNhans_PhamNhanID] FOREIGN KEY([PhamNhanID])
REFERENCES [dbo].[PhamNhans] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AnXas] CHECK CONSTRAINT [FK_dbo.AnXas_dbo.PhamNhans_PhamNhanID]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[BanGiaoCongViecCuaQuanNgucNghis]  WITH CHECK ADD  CONSTRAINT [FK_dbo.BanGiaoCongViecCuaQuanNgucNghis_dbo.PhongGiams_PhongID] FOREIGN KEY([PhongID])
REFERENCES [dbo].[PhongGiams] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BanGiaoCongViecCuaQuanNgucNghis] CHECK CONSTRAINT [FK_dbo.BanGiaoCongViecCuaQuanNgucNghis_dbo.PhongGiams_PhongID]
GO
ALTER TABLE [dbo].[BanGiaoPhamNhans]  WITH CHECK ADD  CONSTRAINT [FK_dbo.BanGiaoPhamNhans_dbo.PhongGiams_PhongID] FOREIGN KEY([PhongID])
REFERENCES [dbo].[PhongGiams] ([ID])
GO
ALTER TABLE [dbo].[BanGiaoPhamNhans] CHECK CONSTRAINT [FK_dbo.BanGiaoPhamNhans_dbo.PhongGiams_PhongID]
GO
ALTER TABLE [dbo].[Benhs]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Benhs_dbo.PhamNhans_PhamNhanID] FOREIGN KEY([PhamNhanID])
REFERENCES [dbo].[PhamNhans] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Benhs] CHECK CONSTRAINT [FK_dbo.Benhs_dbo.PhamNhans_PhamNhanID]
GO
ALTER TABLE [dbo].[CheDoNghiPhepCuaQuanNgucs]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CheDoNghiPhepCuaQuanNgucs_dbo.QuanNgucs_QuanNgucID] FOREIGN KEY([QuanNgucID])
REFERENCES [dbo].[QuanNgucs] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CheDoNghiPhepCuaQuanNgucs] CHECK CONSTRAINT [FK_dbo.CheDoNghiPhepCuaQuanNgucs_dbo.QuanNgucs_QuanNgucID]
GO
ALTER TABLE [dbo].[LaoDongCongIches]  WITH CHECK ADD  CONSTRAINT [FK_dbo.LaoDongCongIches_dbo.PhamNhans_PhamNhanID] FOREIGN KEY([PhamNhanID])
REFERENCES [dbo].[PhamNhans] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LaoDongCongIches] CHECK CONSTRAINT [FK_dbo.LaoDongCongIches_dbo.PhamNhans_PhamNhanID]
GO
ALTER TABLE [dbo].[LaoDongCongIches]  WITH CHECK ADD  CONSTRAINT [FK_dbo.LaoDongCongIches_dbo.QuanNgucs_QuanNgucID] FOREIGN KEY([QuanNgucID])
REFERENCES [dbo].[QuanNgucs] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LaoDongCongIches] CHECK CONSTRAINT [FK_dbo.LaoDongCongIches_dbo.QuanNgucs_QuanNgucID]
GO
ALTER TABLE [dbo].[PhamNhans]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PhamNhans_dbo.Khus_Khu_ID] FOREIGN KEY([Khu_ID])
REFERENCES [dbo].[Khus] ([ID])
GO
ALTER TABLE [dbo].[PhamNhans] CHECK CONSTRAINT [FK_dbo.PhamNhans_dbo.Khus_Khu_ID]
GO
ALTER TABLE [dbo].[PhamNhans]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PhamNhans_dbo.PhongGiams_PhongGiamID] FOREIGN KEY([PhongGiamID])
REFERENCES [dbo].[PhongGiams] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PhamNhans] CHECK CONSTRAINT [FK_dbo.PhamNhans_dbo.PhongGiams_PhongGiamID]
GO
ALTER TABLE [dbo].[QuanNgucs]  WITH CHECK ADD  CONSTRAINT [FK_dbo.QuanNgucs_dbo.Khus_KhuID] FOREIGN KEY([KhuID])
REFERENCES [dbo].[Khus] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuanNgucs] CHECK CONSTRAINT [FK_dbo.QuanNgucs_dbo.Khus_KhuID]
GO
ALTER TABLE [dbo].[ThamGuis]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ThamGuis_dbo.PhamNhans_PhamNhanID] FOREIGN KEY([PhamNhanID])
REFERENCES [dbo].[PhamNhans] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ThamGuis] CHECK CONSTRAINT [FK_dbo.ThamGuis_dbo.PhamNhans_PhamNhanID]
GO
ALTER TABLE [dbo].[ThamGuis]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ThamGuis_dbo.QuanNgucs_QuanNgucID] FOREIGN KEY([QuanNgucID])
REFERENCES [dbo].[QuanNgucs] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ThamGuis] CHECK CONSTRAINT [FK_dbo.ThamGuis_dbo.QuanNgucs_QuanNgucID]
GO
USE [master]
GO
ALTER DATABASE [NhaTu] SET  READ_WRITE 
GO
