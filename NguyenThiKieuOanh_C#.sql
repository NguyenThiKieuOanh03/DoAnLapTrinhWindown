USE [QLBanHang]
GO
/****** Object:  Table [dbo].[ThongTinHoaDon]    Script Date: 11/16/2023 8:58:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongTinHoaDon](
	[MaHD] [int] NOT NULL,
	[NgayLapHD] [date] NOT NULL,
	[MaKH] [int] NOT NULL,
	[TenKH] [nvarchar](50) NOT NULL,
	[TenNhanVien] [nvarchar](50) NOT NULL,
	[DiaChi] [nvarchar](50) NOT NULL,
	[SDT] [nvarchar](50) NOT NULL,
	[TenSP] [nvarchar](50) NOT NULL,
	[DonGiaBan] [int] NOT NULL,
	[SoLuongSP] [int] NOT NULL,
	[TongTien] [int] NOT NULL,
	[MaNhanVien] [int] NOT NULL,
 CONSTRAINT [PK_ThongTinHoaDon] PRIMARY KEY CLUSTERED 
(
	[MaHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ThongTinKhachHang]    Script Date: 11/16/2023 8:58:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongTinKhachHang](
	[MaKH] [int] NOT NULL,
	[TenKH] [nvarchar](50) NOT NULL,
	[DienThoai] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ThongTinKhachHang] PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ThongTinLoai]    Script Date: 11/16/2023 8:58:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongTinLoai](
	[MaLoai] [int] NOT NULL,
	[TenLoai] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ThongTinLoai] PRIMARY KEY CLUSTERED 
(
	[MaLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ThongTinNhanVien]    Script Date: 11/16/2023 8:58:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongTinNhanVien](
	[MaNhanVien] [int] NOT NULL,
	[TenNhanVien] [nvarchar](50) NOT NULL,
	[NgaySinh] [date] NOT NULL,
	[DienThoai] [nvarchar](50) NOT NULL,
	[GioiTinh] [nvarchar](50) NOT NULL,
	[DiaChi] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ThongTinNhanVien] PRIMARY KEY CLUSTERED 
(
	[MaNhanVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ThongTinSanPham]    Script Date: 11/16/2023 8:58:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongTinSanPham](
	[MaSP] [int] NOT NULL,
	[TenSP] [nvarchar](50) NOT NULL,
	[Loai] [nvarchar](50) NOT NULL,
	[DonGia] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[ChiTiet] [nvarchar](50) NOT NULL,
	[Anh] [image] NULL,
 CONSTRAINT [PK_ThongTinSanPham] PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ThongTinThanhVien]    Script Date: 11/16/2023 8:58:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongTinThanhVien](
	[MaTV] [int] NOT NULL,
	[HoTen] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[SDT] [nvarchar](50) NOT NULL,
	[MatKhau] [nvarchar](50) NOT NULL,
	[TenDangNhap] [nvarchar](50) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ThongTinThanhVien] PRIMARY KEY CLUSTERED 
(
	[MaTV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
