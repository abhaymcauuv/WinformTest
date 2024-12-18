USE [master]
GO
/****** Object:  Database [CompanyDB]    Script Date: 11/27/2024 17:17:59 ******/
CREATE DATABASE [CompanyDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CompanyDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\CompanyDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CompanyDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\CompanyDB_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CompanyDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CompanyDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CompanyDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CompanyDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CompanyDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CompanyDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CompanyDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [CompanyDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CompanyDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CompanyDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CompanyDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CompanyDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CompanyDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CompanyDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CompanyDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CompanyDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CompanyDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CompanyDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CompanyDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CompanyDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CompanyDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CompanyDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CompanyDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CompanyDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CompanyDB] SET RECOVERY FULL 
GO
ALTER DATABASE [CompanyDB] SET  MULTI_USER 
GO
ALTER DATABASE [CompanyDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CompanyDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CompanyDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CompanyDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CompanyDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CompanyDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [CompanyDB] SET QUERY_STORE = OFF
GO
USE [CompanyDB]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 11/27/2024 17:17:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](100) NOT NULL,
	[AddressLine1] [nvarchar](200) NOT NULL,
	[AddressLine2] [nvarchar](200) NULL,
	[ZipCode] [nvarchar](20) NULL,
	[Telephone] [nvarchar](15) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[CompanyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 11/27/2024 17:17:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[ContactID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyID] [int] NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Telephone] [nvarchar](15) NULL,
	[Email] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Company] ON 

INSERT [dbo].[Company] ([CompanyID], [CompanyName], [AddressLine1], [AddressLine2], [ZipCode], [Telephone], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1003, N'Test', N'test', N'test', N'751010', N'91123456789', 1, CAST(N'2024-11-27T17:14:13.883' AS DateTime), N'Admin', NULL, NULL)
INSERT [dbo].[Company] ([CompanyID], [CompanyName], [AddressLine1], [AddressLine2], [ZipCode], [Telephone], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1004, N'test1', N'test1', N'tets2', N'751001', N'8888888888', 1, CAST(N'2024-11-27T17:14:42.437' AS DateTime), N'Admin', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Company] OFF
GO
SET IDENTITY_INSERT [dbo].[Contact] ON 

INSERT [dbo].[Contact] ([ContactID], [CompanyID], [FirstName], [LastName], [Telephone], [Email], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1, 1003, N'abhi', N'r', N'12332444', N'aa@gmail.com', 1, CAST(N'2024-11-27T17:15:15.093' AS DateTime), N'Admin', NULL, NULL)
INSERT [dbo].[Contact] ([ContactID], [CompanyID], [FirstName], [LastName], [Telephone], [Email], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (2, 1003, N'bbb', N'bbbb', N'888277777', N'aaa@gmai.com', 1, CAST(N'2024-11-27T17:15:35.643' AS DateTime), N'Admin', NULL, NULL)
INSERT [dbo].[Contact] ([ContactID], [CompanyID], [FirstName], [LastName], [Telephone], [Email], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (3, 1004, N'abc', N'abc', N'333333333', N'ssss@gmail.com', 1, CAST(N'2024-11-27T17:15:58.153' AS DateTime), N'Admin', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Contact] OFF
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Contact] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Contact] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD FOREIGN KEY([CompanyID])
REFERENCES [dbo].[Company] ([CompanyID])
GO
USE [master]
GO
ALTER DATABASE [CompanyDB] SET  READ_WRITE 
GO
