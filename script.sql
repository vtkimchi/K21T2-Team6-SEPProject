USE [master]
GO
/****** Object:  Database [SEP]    Script Date: 6/13/2018 12:47:27 PM ******/
CREATE DATABASE [SEP]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SEP_DB', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\SEP.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SEP_DB_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\SEP_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SEP] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SEP].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SEP] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SEP] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SEP] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SEP] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SEP] SET ARITHABORT OFF 
GO
ALTER DATABASE [SEP] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SEP] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [SEP] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SEP] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SEP] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SEP] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SEP] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SEP] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SEP] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SEP] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SEP] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SEP] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SEP] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SEP] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SEP] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SEP] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SEP] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SEP] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SEP] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SEP] SET  MULTI_USER 
GO
ALTER DATABASE [SEP] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SEP] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SEP] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SEP] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [SEP]
GO
/****** Object:  Table [dbo].[Attendance]    Script Date: 6/13/2018 12:47:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attendance](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_Lesson] [int] NULL,
	[ID_Student] [int] NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Attendance] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Lesson]    Script Date: 6/13/2018 12:47:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lesson](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MaKH] [nvarchar](100) NULL,
	[Day] [date] NULL,
	[Count] [int] NULL,
 CONSTRAINT [PK_Lesson] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Member]    Script Date: 6/13/2018 12:47:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Member](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MaSV] [varchar](7) NOT NULL,
	[MaKH] [nvarchar](100) NULL,
	[Firstname] [nvarchar](50) NULL,
	[Lastname] [nvarchar](50) NULL,
 CONSTRAINT [PK_Students_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Attendance] ON 

INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (97, 22, 19, 0)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (98, 22, 20, 0)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (99, 22, 21, 0)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (100, 22, 22, 1)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (101, 22, 23, 1)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (102, 22, 24, 0)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (103, 23, 19, 0)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (104, 23, 20, 0)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (105, 23, 21, 0)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (106, 23, 22, 1)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (107, 23, 23, 0)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (108, 23, 24, 0)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (109, 24, 19, 0)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (110, 24, 20, 0)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (111, 24, 21, 0)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (112, 24, 22, 1)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (113, 24, 23, 0)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (114, 24, 24, 0)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (1103, 1023, 19, 1)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (1104, 1023, 20, 0)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (1105, 1023, 21, 0)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (1106, 1023, 22, 1)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (1107, 1023, 23, 1)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (1108, 1023, 24, 0)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (1109, 1024, 19, 0)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (1110, 1024, 20, 0)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (1111, 1024, 21, 0)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (1112, 1024, 22, 1)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (1113, 1024, 23, 1)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (1114, 1024, 24, 1)
INSERT [dbo].[Attendance] ([ID], [ID_Lesson], [ID_Student], [Status]) VALUES (1115, 1024, 25, 1)
SET IDENTITY_INSERT [dbo].[Attendance] OFF
SET IDENTITY_INSERT [dbo].[Lesson] ON 

INSERT [dbo].[Lesson] ([ID], [MaKH], [Day], [Count]) VALUES (22, N'TH2', CAST(0x3D3E0B00 AS Date), 1)
INSERT [dbo].[Lesson] ([ID], [MaKH], [Day], [Count]) VALUES (23, N'TH2', CAST(0x443E0B00 AS Date), 2)
INSERT [dbo].[Lesson] ([ID], [MaKH], [Day], [Count]) VALUES (24, N'TH2', CAST(0x463E0B00 AS Date), 3)
INSERT [dbo].[Lesson] ([ID], [MaKH], [Day], [Count]) VALUES (1023, N'TH2', CAST(0x583E0B00 AS Date), 4)
INSERT [dbo].[Lesson] ([ID], [MaKH], [Day], [Count]) VALUES (1024, N'TH2', CAST(0x593E0B00 AS Date), 5)
SET IDENTITY_INSERT [dbo].[Lesson] OFF
SET IDENTITY_INSERT [dbo].[Member] ON 

INSERT [dbo].[Member] ([ID], [MaSV], [MaKH], [Firstname], [Lastname]) VALUES (13, N'T151451', N'TH1', N'DUY', N'KHẤU THÀNH')
INSERT [dbo].[Member] ([ID], [MaSV], [MaKH], [Firstname], [Lastname]) VALUES (14, N'T152750', N'TH1', N'NGÂN', N'NGUYỄN THỊ TÀI')
INSERT [dbo].[Member] ([ID], [MaSV], [MaKH], [Firstname], [Lastname]) VALUES (15, N'T150776', N'TH1', N'PHƯỚC', N'LÊ VĂN')
INSERT [dbo].[Member] ([ID], [MaSV], [MaKH], [Firstname], [Lastname]) VALUES (16, N'T154135', N'TH1', N'QUÂN', N'NGÔ NGUYỄN MINH')
INSERT [dbo].[Member] ([ID], [MaSV], [MaKH], [Firstname], [Lastname]) VALUES (17, N'T151391', N'TH1', N'TÂM', N'LA THÀNH')
INSERT [dbo].[Member] ([ID], [MaSV], [MaKH], [Firstname], [Lastname]) VALUES (18, N'T150445', N'TH1', N'TUẤN', N'HỒ VŨ MINH')
INSERT [dbo].[Member] ([ID], [MaSV], [MaKH], [Firstname], [Lastname]) VALUES (19, N'T153556', N'TH2', N'CHI', N'VÕ THỊ KIM')
INSERT [dbo].[Member] ([ID], [MaSV], [MaKH], [Firstname], [Lastname]) VALUES (20, N'T153220', N'TH2', N'TÙNG', N'NGUYỄN THANH')
INSERT [dbo].[Member] ([ID], [MaSV], [MaKH], [Firstname], [Lastname]) VALUES (21, N'T154966', N'TH2', N'TÍNH', N'NGÔ HỮU')
INSERT [dbo].[Member] ([ID], [MaSV], [MaKH], [Firstname], [Lastname]) VALUES (22, N'T154810', N'TH2', N'ĐỨC', N'PHẠM CÔNG')
INSERT [dbo].[Member] ([ID], [MaSV], [MaKH], [Firstname], [Lastname]) VALUES (23, N'T153930', N'TH2', N'TRANG', N'NGUYỄN THỊ PHƯƠNG')
INSERT [dbo].[Member] ([ID], [MaSV], [MaKH], [Firstname], [Lastname]) VALUES (24, N'T154432', N'TH2', N'TRUNG', N'NGUYỄN ĐỨC')
INSERT [dbo].[Member] ([ID], [MaSV], [MaKH], [Firstname], [Lastname]) VALUES (25, N'T153130', N'TH2', N'LÂM', N'TÔ NGỌC')
SET IDENTITY_INSERT [dbo].[Member] OFF
ALTER TABLE [dbo].[Attendance] ADD  CONSTRAINT [DF_Attendance_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD  CONSTRAINT [FK_Attendance_Lesson] FOREIGN KEY([ID_Lesson])
REFERENCES [dbo].[Lesson] ([ID])
GO
ALTER TABLE [dbo].[Attendance] CHECK CONSTRAINT [FK_Attendance_Lesson]
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD  CONSTRAINT [FK_Attendance_Member] FOREIGN KEY([ID_Student])
REFERENCES [dbo].[Member] ([ID])
GO
ALTER TABLE [dbo].[Attendance] CHECK CONSTRAINT [FK_Attendance_Member]
GO
USE [master]
GO
ALTER DATABASE [SEP] SET  READ_WRITE 
GO
