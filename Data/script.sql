USE [master]
GO
/****** Object:  Database [BillTrackerDB]    Script Date: 12/28/2024 3:10:02 PM ******/
CREATE DATABASE [BillTrackerDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BillTrackerDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\BillTrackerDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BillTrackerDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\BillTrackerDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [BillTrackerDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BillTrackerDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BillTrackerDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BillTrackerDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BillTrackerDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BillTrackerDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BillTrackerDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [BillTrackerDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BillTrackerDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BillTrackerDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BillTrackerDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BillTrackerDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BillTrackerDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BillTrackerDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BillTrackerDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BillTrackerDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BillTrackerDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BillTrackerDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BillTrackerDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BillTrackerDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BillTrackerDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BillTrackerDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BillTrackerDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BillTrackerDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BillTrackerDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BillTrackerDB] SET  MULTI_USER 
GO
ALTER DATABASE [BillTrackerDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BillTrackerDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BillTrackerDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BillTrackerDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BillTrackerDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BillTrackerDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BillTrackerDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [BillTrackerDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [BillTrackerDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 12/28/2024 3:10:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 12/28/2024 3:10:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UniqueBillId] [nvarchar](max) NULL,
	[PoNo] [nvarchar](max) NULL,
	[BillNo] [nvarchar](max) NULL,
	[BillDate] [nvarchar](max) NULL,
	[BillAmount] [decimal](18, 2) NOT NULL,
	[Challan] [nvarchar](max) NULL,
	[ChallanDate] [nvarchar](max) NULL,
	[Status] [bit] NOT NULL,
	[QuantityReceived] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ItemKey] [int] NOT NULL,
	[SubmitDate] [nvarchar](max) NULL,
	[UserId] [int] NOT NULL,
	[RequestForDeletion] [bit] NOT NULL,
	[QrCode] [nvarchar](max) NULL,
	[ApprovedAmount] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/28/2024 3:10:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[IsAdmin] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241224013005_InitialMigration', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241224013656_RemoveUserName', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241226123219_AddIsActiveToUser', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241227023908_ProductTable', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241227043141_ConfigureBillAmountPrecision', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241227122345_QRCodeColumnAdded', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241228065454_PartialAmountColumn', N'9.0.0')
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [UniqueBillId], [PoNo], [BillNo], [BillDate], [BillAmount], [Challan], [ChallanDate], [Status], [QuantityReceived], [Description], [ItemKey], [SubmitDate], [UserId], [RequestForDeletion], [QrCode], [ApprovedAmount]) VALUES (3, N'TEST123', N'TestPONO123', N'TestB123', N'2024-12-24', CAST(652.00 AS Decimal(18, 2)), N'1', N'2024-12-10', 1, 1, N'This is test product', 125, N'2024-12-25', 2, 0, N'TyeCsL73OSfXTdSMepyv2sjN3l/vGyIEjVXAxqHGQE7jyf9CVdF/1iFejZxXXUD8rD+yNU0xAskb00/aHaLKy43WQmhRGssgUtVF2SQ9KyhQJZv57mc9rspOcH8LZJfG', CAST(650.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([Id], [UniqueBillId], [PoNo], [BillNo], [BillDate], [BillAmount], [Challan], [ChallanDate], [Status], [QuantityReceived], [Description], [ItemKey], [SubmitDate], [UserId], [RequestForDeletion], [QrCode], [ApprovedAmount]) VALUES (4, N'UBI123', N'PO123', N'B1236', N'2024-12-04', CAST(324.00 AS Decimal(18, 2)), N'1', N'2024-12-12', 0, 1, N'This is First product', 12, N'2024-12-26', 4, 0, N'JRCYlGDiD0Blze0j7GMrHtEdPsHYBpbVChDMfu9G6LVAD6vxPACCByW6yQ4HEG+lgCzlr0rRqYybDuP+xQu9Bp87mE/bW6egQwwkAQWeJz4MCVThVFPe7/0ilqoSuI2p', CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([Id], [UniqueBillId], [PoNo], [BillNo], [BillDate], [BillAmount], [Challan], [ChallanDate], [Status], [QuantityReceived], [Description], [ItemKey], [SubmitDate], [UserId], [RequestForDeletion], [QrCode], [ApprovedAmount]) VALUES (6, N'UBI123', N'TestPONO123', N'TestB123', N'2024-12-15', CAST(687.00 AS Decimal(18, 2)), N'12', N'2024-12-26', 1, 2, N'Testing Delete', 3, N'2024-12-29', 5, 1, N'JRCYlGDiD0Blze0j7GMrHi98xiGhYiUoUxC0X9374mrWaBr6/9gpwKUDwFv+x5lbjja5gvmsi0m62yRCwnlH4r3B0ty4T5xsTYjZMGZx78XckJJVWtbj5MCTfW4oHdoK', CAST(630.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [FullName], [Email], [Password], [IsAdmin], [IsActive]) VALUES (1, N'Shuvo', N'shuvo@gmail.com', N'j3VVgKmgy2g0pSIJPTxDPw==', 1, 1)
INSERT [dbo].[Users] ([Id], [FullName], [Email], [Password], [IsAdmin], [IsActive]) VALUES (2, N'Test User', N'testuser@test.com', N'VeAQzw/rFI+U38IDRN9Hcg==', 0, 1)
INSERT [dbo].[Users] ([Id], [FullName], [Email], [Password], [IsAdmin], [IsActive]) VALUES (3, N'Test User 2', N'testuser2@test.com', N'Z2JdtzEepHiA+/LokHl87g==', 0, 1)
INSERT [dbo].[Users] ([Id], [FullName], [Email], [Password], [IsAdmin], [IsActive]) VALUES (4, N'Rezaul Karim', N'rezaul@gmail.com', N'zjK6Pf9c9S5rLigNB/oRwg==', 0, 1)
INSERT [dbo].[Users] ([Id], [FullName], [Email], [Password], [IsAdmin], [IsActive]) VALUES (5, N'Test User 3', N'testuser3@test.com', N'Z2JdtzEepHiA+/LokHl87g==', 0, 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_Products_UserId]    Script Date: 12/28/2024 3:10:02 PM ******/
CREATE NONCLUSTERED INDEX [IX_Products_UserId] ON [dbo].[Products]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT (CONVERT([bit],(0))) FOR [RequestForDeletion]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT ((0.0)) FOR [ApprovedAmount]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [BillTrackerDB] SET  READ_WRITE 
GO
