USE [SoLienLacDT]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 3/29/2021 8:10:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TaiKhoan] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[TenAdmin] [nvarchar](50) NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietHocPhi]    Script Date: 3/29/2021 8:10:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHocPhi](
	[MaMon] [varchar](10) NOT NULL,
	[TCHP] [float] NULL,
	[PhiTL] [int] NULL,
	[HocPhi] [bigint] NULL,
	[SoTienPhaiDong] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaMon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Diem]    Script Date: 3/29/2021 8:10:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Diem](
	[IDDiem] [int] IDENTITY(1,1) NOT NULL,
	[MaSV] [varchar](50) NULL,
	[DiemTBTichLuy] [float] NULL,
	[DiemTBHK] [float] NULL,
	[SoTinChiDat] [int] NULL,
	[SoTinChiTL] [int] NULL,
	[NamHoc] [varchar](10) NOT NULL,
	[HocKy] [int] NOT NULL,
 CONSTRAINT [PK_Diem] PRIMARY KEY CLUSTERED 
(
	[IDDiem] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DiemCT]    Script Date: 3/29/2021 8:10:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiemCT](
	[IDDiemCT] [int] IDENTITY(1,1) NOT NULL,
	[IDDiem] [int] NULL,
	[MaMon] [varchar](10) NOT NULL,
	[NamHoc] [varchar](10) NOT NULL,
	[DiemGK] [float] NULL,
	[DiemCK] [float] NULL,
	[DiemTBM] [float] NULL,
	[HocKy] [int] NOT NULL,
	[KQ] [bit] NULL,
 CONSTRAINT [PK_DiemCT] PRIMARY KEY CLUSTERED 
(
	[IDDiemCT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GiangVien]    Script Date: 3/29/2021 8:10:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiangVien](
	[MaGV] [varchar](50) NOT NULL,
	[TenGV] [nchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaGV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GiangVien_Nganh]    Script Date: 3/29/2021 8:10:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiangVien_Nganh](
	[Stt] [int] IDENTITY(1,1) NOT NULL,
	[MaGV] [varchar](50) NULL,
	[MaNganh] [varchar](50) NULL,
 CONSTRAINT [PK_GiangVien_Nganh] PRIMARY KEY CLUSTERED 
(
	[Stt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GV_LM]    Script Date: 3/29/2021 8:10:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GV_LM](
	[MaLM] [varchar](10) NULL,
	[MaGV] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GV_Mon]    Script Date: 3/29/2021 8:10:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GV_Mon](
	[MaGV] [varchar](50) NOT NULL,
	[MaMon] [varchar](10) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LichThi]    Script Date: 3/29/2021 8:10:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LichThi](
	[IDLichThi] [int] IDENTITY(1,1) NOT NULL,
	[MaMon] [varchar](10) NOT NULL,
	[MaSV] [varchar](50) NOT NULL,
	[TenMon] [nvarchar](50) NOT NULL,
	[NhomThi] [varchar](5) NOT NULL,
	[NgayThi] [date] NOT NULL,
	[GioBD] [time](5) NOT NULL,
	[SoPhut] [int] NOT NULL,
	[Phong] [varchar](10) NOT NULL,
	[GhiChu] [nvarchar](20) NOT NULL,
	[NamHoc] [varchar](10) NOT NULL,
	[HocKy] [int] NOT NULL,
 CONSTRAINT [PK_LichThi] PRIMARY KEY CLUSTERED 
(
	[IDLichThi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lop]    Script Date: 3/29/2021 8:10:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lop](
	[Malop] [varchar](10) NOT NULL,
	[MaGV] [varchar](50) NULL,
 CONSTRAINT [PK_Lop] PRIMARY KEY CLUSTERED 
(
	[Malop] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LopMon]    Script Date: 3/29/2021 8:10:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LopMon](
	[MaLM] [varchar](10) NOT NULL,
	[Lop] [varchar](10) NULL,
	[MaMon] [varchar](10) NOT NULL,
 CONSTRAINT [PK_LopMon] PRIMARY KEY CLUSTERED 
(
	[MaLM] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MonHoc]    Script Date: 3/29/2021 8:10:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MonHoc](
	[MaMon] [varchar](10) NOT NULL,
	[TenMon] [nvarchar](50) NULL,
	[Sotinchi] [int] NULL,
	[MaMonTienQuyet] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaMon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nganh]    Script Date: 3/29/2021 8:10:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nganh](
	[MaNganh] [varchar](50) NOT NULL,
	[TenNganh] [nchar](30) NOT NULL,
	[TongTC] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNganh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nganh_Mon]    Script Date: 3/29/2021 8:10:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nganh_Mon](
	[MaNganh] [varchar](50) NOT NULL,
	[MaMon] [varchar](10) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PH]    Script Date: 3/29/2021 8:10:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PH](
	[MaPH] [varchar](50) NOT NULL,
	[TenPH] [nvarchar](30) NULL,
	[SDTPH] [varchar](15) NULL,
 CONSTRAINT [PK_PH] PRIMARY KEY CLUSTERED 
(
	[MaPH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SinhVien]    Script Date: 3/29/2021 8:10:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SinhVien](
	[MaSV] [varchar](50) NOT NULL,
	[TenSV] [nchar](50) NOT NULL,
	[GioiTinh] [nchar](10) NOT NULL,
	[Diachi] [ntext] NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[SDT] [bigint] NULL,
	[NgaySinh] [datetime] NULL,
	[Password] [varchar](50) NOT NULL,
	[MaPH] [varchar](50) NULL,
 CONSTRAINT [PK__SinhVien__2725081A994C852E] PRIMARY KEY CLUSTERED 
(
	[MaSV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SinhVien_LopMon]    Script Date: 3/29/2021 8:10:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SinhVien_LopMon](
	[ST] [int] IDENTITY(1,1) NOT NULL,
	[MaSV] [varchar](50) NOT NULL,
	[MaLM] [varchar](10) NOT NULL,
 CONSTRAINT [PK_SinhVien_LopMon] PRIMARY KEY CLUSTERED 
(
	[ST] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SinhVien_Nganh]    Script Date: 3/29/2021 8:10:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SinhVien_Nganh](
	[MaSV] [varchar](50) NOT NULL,
	[MaNganh] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SV_LOP]    Script Date: 3/29/2021 8:10:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SV_LOP](
	[Malop] [varchar](10) NULL,
	[MaSV] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SV_MON]    Script Date: 3/29/2021 8:10:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SV_MON](
	[STT] [int] IDENTITY(1,1) NOT NULL,
	[MaSV] [varchar](50) NULL,
	[MaMon] [varchar](10) NULL,
	[DaHoc] [bit] NULL,
	[MaMonTienquyet] [varchar](50) NULL,
 CONSTRAINT [PK_SV_MON] PRIMARY KEY CLUSTERED 
(
	[STT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThoiKhoaBieu]    Script Date: 3/29/2021 8:10:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThoiKhoaBieu](
	[IDTKB] [int] IDENTITY(1,1) NOT NULL,
	[MaMon] [varchar](10) NOT NULL,
	[TenMon] [nvarchar](50) NOT NULL,
	[MaGV] [varchar](50) NOT NULL,
	[Phong] [varchar](10) NOT NULL,
	[TietBD] [int] NOT NULL,
	[Thu] [int] NOT NULL,
	[TH] [int] NULL,
	[ST] [int] NOT NULL,
	[ThoiGianBD] [date] NULL,
	[HocKy] [int] NOT NULL,
	[MaLM] [varchar](10) NULL,
	[ThoiGianKT] [date] NULL,
	[TongNgayHoc] [int] NULL,
 CONSTRAINT [PK_ThoiKhoaBieu] PRIMARY KEY CLUSTERED 
(
	[IDTKB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThongBao]    Script Date: 3/29/2021 8:10:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongBao](
	[IDTB] [int] IDENTITY(1,1) NOT NULL,
	[MaGV] [varchar](50) NOT NULL,
	[NoiDung] [text] NOT NULL,
	[NgayDang] [datetime] NULL,
	[MaLop] [varchar](10) NULL,
	[MaLM] [varchar](10) NULL,
	[MaSV] [varchar](50) NULL,
 CONSTRAINT [PK_ThongBao] PRIMARY KEY CLUSTERED 
(
	[IDTB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThongBaoChung]    Script Date: 3/29/2021 8:10:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongBaoChung](
	[MaTB] [int] IDENTITY(1,1) NOT NULL,
	[TieuDe] [nvarchar](50) NOT NULL,
	[NoiDung] [ntext] NOT NULL,
	[NgayDang] [datetime] NOT NULL,
 CONSTRAINT [PK_ThongBaoChung] PRIMARY KEY CLUSTERED 
(
	[MaTB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ThongBao] ADD  CONSTRAINT [DF_ThongBao_NgayDang]  DEFAULT (getdate()) FOR [NgayDang]
GO
ALTER TABLE [dbo].[ChiTietHocPhi]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHocPhi_MonHoc] FOREIGN KEY([MaMon])
REFERENCES [dbo].[MonHoc] ([MaMon])
GO
ALTER TABLE [dbo].[ChiTietHocPhi] CHECK CONSTRAINT [FK_ChiTietHocPhi_MonHoc]
GO
ALTER TABLE [dbo].[Diem]  WITH CHECK ADD  CONSTRAINT [FK_Diem_SinhVien] FOREIGN KEY([MaSV])
REFERENCES [dbo].[SinhVien] ([MaSV])
GO
ALTER TABLE [dbo].[Diem] CHECK CONSTRAINT [FK_Diem_SinhVien]
GO
ALTER TABLE [dbo].[DiemCT]  WITH CHECK ADD  CONSTRAINT [FK_DiemCT_Diem] FOREIGN KEY([IDDiem])
REFERENCES [dbo].[Diem] ([IDDiem])
GO
ALTER TABLE [dbo].[DiemCT] CHECK CONSTRAINT [FK_DiemCT_Diem]
GO
ALTER TABLE [dbo].[DiemCT]  WITH CHECK ADD  CONSTRAINT [FK_DiemCT_MonHoc] FOREIGN KEY([MaMon])
REFERENCES [dbo].[MonHoc] ([MaMon])
GO
ALTER TABLE [dbo].[DiemCT] CHECK CONSTRAINT [FK_DiemCT_MonHoc]
GO
ALTER TABLE [dbo].[GiangVien_Nganh]  WITH CHECK ADD  CONSTRAINT [FK_GiangVien_Nganh_GiangVien] FOREIGN KEY([MaGV])
REFERENCES [dbo].[GiangVien] ([MaGV])
GO
ALTER TABLE [dbo].[GiangVien_Nganh] CHECK CONSTRAINT [FK_GiangVien_Nganh_GiangVien]
GO
ALTER TABLE [dbo].[GiangVien_Nganh]  WITH CHECK ADD  CONSTRAINT [FK_GiangVien_Nganh_Nganh] FOREIGN KEY([MaNganh])
REFERENCES [dbo].[Nganh] ([MaNganh])
GO
ALTER TABLE [dbo].[GiangVien_Nganh] CHECK CONSTRAINT [FK_GiangVien_Nganh_Nganh]
GO
ALTER TABLE [dbo].[GV_LM]  WITH CHECK ADD  CONSTRAINT [FK_GV_LM_GiangVien] FOREIGN KEY([MaGV])
REFERENCES [dbo].[GiangVien] ([MaGV])
GO
ALTER TABLE [dbo].[GV_LM] CHECK CONSTRAINT [FK_GV_LM_GiangVien]
GO
ALTER TABLE [dbo].[GV_LM]  WITH CHECK ADD  CONSTRAINT [FK_GV_LM_LopMon] FOREIGN KEY([MaLM])
REFERENCES [dbo].[LopMon] ([MaLM])
GO
ALTER TABLE [dbo].[GV_LM] CHECK CONSTRAINT [FK_GV_LM_LopMon]
GO
ALTER TABLE [dbo].[GV_Mon]  WITH CHECK ADD  CONSTRAINT [FK_GV_Mon_GiangVien] FOREIGN KEY([MaGV])
REFERENCES [dbo].[GiangVien] ([MaGV])
GO
ALTER TABLE [dbo].[GV_Mon] CHECK CONSTRAINT [FK_GV_Mon_GiangVien]
GO
ALTER TABLE [dbo].[GV_Mon]  WITH CHECK ADD  CONSTRAINT [FK_GV_Mon_MonHoc] FOREIGN KEY([MaMon])
REFERENCES [dbo].[MonHoc] ([MaMon])
GO
ALTER TABLE [dbo].[GV_Mon] CHECK CONSTRAINT [FK_GV_Mon_MonHoc]
GO
ALTER TABLE [dbo].[LichThi]  WITH CHECK ADD  CONSTRAINT [FK_LichThi_MonHoc] FOREIGN KEY([MaMon])
REFERENCES [dbo].[MonHoc] ([MaMon])
GO
ALTER TABLE [dbo].[LichThi] CHECK CONSTRAINT [FK_LichThi_MonHoc]
GO
ALTER TABLE [dbo].[LichThi]  WITH CHECK ADD  CONSTRAINT [FK_LichThi_SinhVien] FOREIGN KEY([MaSV])
REFERENCES [dbo].[SinhVien] ([MaSV])
GO
ALTER TABLE [dbo].[LichThi] CHECK CONSTRAINT [FK_LichThi_SinhVien]
GO
ALTER TABLE [dbo].[Lop]  WITH CHECK ADD  CONSTRAINT [FK_Lop_GiangVien] FOREIGN KEY([MaGV])
REFERENCES [dbo].[GiangVien] ([MaGV])
GO
ALTER TABLE [dbo].[Lop] CHECK CONSTRAINT [FK_Lop_GiangVien]
GO
ALTER TABLE [dbo].[LopMon]  WITH CHECK ADD  CONSTRAINT [FK_LopMon_MonHoc] FOREIGN KEY([MaMon])
REFERENCES [dbo].[MonHoc] ([MaMon])
GO
ALTER TABLE [dbo].[LopMon] CHECK CONSTRAINT [FK_LopMon_MonHoc]
GO
ALTER TABLE [dbo].[Nganh_Mon]  WITH CHECK ADD  CONSTRAINT [FK_Nganh_Mon_MonHoc] FOREIGN KEY([MaMon])
REFERENCES [dbo].[MonHoc] ([MaMon])
GO
ALTER TABLE [dbo].[Nganh_Mon] CHECK CONSTRAINT [FK_Nganh_Mon_MonHoc]
GO
ALTER TABLE [dbo].[Nganh_Mon]  WITH CHECK ADD  CONSTRAINT [FK_Nganh_Mon_Nganh] FOREIGN KEY([MaNganh])
REFERENCES [dbo].[Nganh] ([MaNganh])
GO
ALTER TABLE [dbo].[Nganh_Mon] CHECK CONSTRAINT [FK_Nganh_Mon_Nganh]
GO
ALTER TABLE [dbo].[SinhVien]  WITH CHECK ADD  CONSTRAINT [FK_SinhVien_PH] FOREIGN KEY([MaPH])
REFERENCES [dbo].[PH] ([MaPH])
GO
ALTER TABLE [dbo].[SinhVien] CHECK CONSTRAINT [FK_SinhVien_PH]
GO
ALTER TABLE [dbo].[SinhVien_LopMon]  WITH CHECK ADD  CONSTRAINT [FK_SinhVien_LopMon_LopMon] FOREIGN KEY([MaLM])
REFERENCES [dbo].[LopMon] ([MaLM])
GO
ALTER TABLE [dbo].[SinhVien_LopMon] CHECK CONSTRAINT [FK_SinhVien_LopMon_LopMon]
GO
ALTER TABLE [dbo].[SinhVien_LopMon]  WITH CHECK ADD  CONSTRAINT [FK_SinhVien_LopMon_SinhVien] FOREIGN KEY([MaSV])
REFERENCES [dbo].[SinhVien] ([MaSV])
GO
ALTER TABLE [dbo].[SinhVien_LopMon] CHECK CONSTRAINT [FK_SinhVien_LopMon_SinhVien]
GO
ALTER TABLE [dbo].[SinhVien_Nganh]  WITH CHECK ADD  CONSTRAINT [FK_SinhVien_Nganh_Nganh] FOREIGN KEY([MaNganh])
REFERENCES [dbo].[Nganh] ([MaNganh])
GO
ALTER TABLE [dbo].[SinhVien_Nganh] CHECK CONSTRAINT [FK_SinhVien_Nganh_Nganh]
GO
ALTER TABLE [dbo].[SinhVien_Nganh]  WITH CHECK ADD  CONSTRAINT [FK_SinhVien_Nganh_SinhVien] FOREIGN KEY([MaSV])
REFERENCES [dbo].[SinhVien] ([MaSV])
GO
ALTER TABLE [dbo].[SinhVien_Nganh] CHECK CONSTRAINT [FK_SinhVien_Nganh_SinhVien]
GO
ALTER TABLE [dbo].[SV_LOP]  WITH CHECK ADD  CONSTRAINT [FK_SV_LOP_Lop] FOREIGN KEY([Malop])
REFERENCES [dbo].[Lop] ([Malop])
GO
ALTER TABLE [dbo].[SV_LOP] CHECK CONSTRAINT [FK_SV_LOP_Lop]
GO
ALTER TABLE [dbo].[SV_LOP]  WITH CHECK ADD  CONSTRAINT [FK_SV_LOP_SinhVien] FOREIGN KEY([MaSV])
REFERENCES [dbo].[SinhVien] ([MaSV])
GO
ALTER TABLE [dbo].[SV_LOP] CHECK CONSTRAINT [FK_SV_LOP_SinhVien]
GO
ALTER TABLE [dbo].[SV_MON]  WITH CHECK ADD  CONSTRAINT [FK_SV_MON_MonHoc] FOREIGN KEY([MaMon])
REFERENCES [dbo].[MonHoc] ([MaMon])
GO
ALTER TABLE [dbo].[SV_MON] CHECK CONSTRAINT [FK_SV_MON_MonHoc]
GO
ALTER TABLE [dbo].[SV_MON]  WITH CHECK ADD  CONSTRAINT [FK_SV_MON_SinhVien] FOREIGN KEY([MaSV])
REFERENCES [dbo].[SinhVien] ([MaSV])
GO
ALTER TABLE [dbo].[SV_MON] CHECK CONSTRAINT [FK_SV_MON_SinhVien]
GO
ALTER TABLE [dbo].[ThoiKhoaBieu]  WITH CHECK ADD  CONSTRAINT [FK_ThoiKhoaBieu_MonHoc] FOREIGN KEY([MaMon])
REFERENCES [dbo].[MonHoc] ([MaMon])
GO
ALTER TABLE [dbo].[ThoiKhoaBieu] CHECK CONSTRAINT [FK_ThoiKhoaBieu_MonHoc]
GO
ALTER TABLE [dbo].[ThongBao]  WITH CHECK ADD  CONSTRAINT [FK_ThongBao_GiangVien] FOREIGN KEY([MaGV])
REFERENCES [dbo].[GiangVien] ([MaGV])
GO
ALTER TABLE [dbo].[ThongBao] CHECK CONSTRAINT [FK_ThongBao_GiangVien]
GO
ALTER TABLE [dbo].[ThongBao]  WITH CHECK ADD  CONSTRAINT [FK_ThongBao_LopMon] FOREIGN KEY([MaLM])
REFERENCES [dbo].[LopMon] ([MaLM])
GO
ALTER TABLE [dbo].[ThongBao] CHECK CONSTRAINT [FK_ThongBao_LopMon]
GO
