USE [db_book]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DETAILED_BILL]') AND type in (N'U'))
ALTER TABLE [dbo].[DETAILED_BILL] DROP CONSTRAINT IF EXISTS [FK_DETAILED_BILL_BOOK]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DETAILED_BILL]') AND type in (N'U'))
ALTER TABLE [dbo].[DETAILED_BILL] DROP CONSTRAINT IF EXISTS [FK_DETAILED_BILL_BILL]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BOOK_PROMOTION]') AND type in (N'U'))
ALTER TABLE [dbo].[BOOK_PROMOTION] DROP CONSTRAINT IF EXISTS [FK_BOOK_PROMOTION_PROMOTION]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BOOK_PROMOTION]') AND type in (N'U'))
ALTER TABLE [dbo].[BOOK_PROMOTION] DROP CONSTRAINT IF EXISTS [FK_BOOK_PROMOTION_BOOK]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BOOK]') AND type in (N'U'))
ALTER TABLE [dbo].[BOOK] DROP CONSTRAINT IF EXISTS [FK_BOOK_GENRE]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BILL]') AND type in (N'U'))
ALTER TABLE [dbo].[BILL] DROP CONSTRAINT IF EXISTS [FK_BILL_ACCOUNT]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACCOUNT]') AND type in (N'U'))
ALTER TABLE [dbo].[ACCOUNT] DROP CONSTRAINT IF EXISTS [FK_ACCOUNT_ROLE]
GO
/****** Object:  Table [dbo].[ROLE]    Script Date: 4/8/2023 3:23:40 PM ******/
DROP TABLE IF EXISTS [dbo].[ROLE]
GO
/****** Object:  Table [dbo].[PROMOTION]    Script Date: 4/8/2023 3:23:40 PM ******/
DROP TABLE IF EXISTS [dbo].[PROMOTION]
GO
/****** Object:  Table [dbo].[GENRE]    Script Date: 4/8/2023 3:23:40 PM ******/
DROP TABLE IF EXISTS [dbo].[GENRE]
GO
/****** Object:  Table [dbo].[DETAILED_BILL]    Script Date: 4/8/2023 3:23:40 PM ******/
DROP TABLE IF EXISTS [dbo].[DETAILED_BILL]
GO
/****** Object:  Table [dbo].[BOOK_PROMOTION]    Script Date: 4/8/2023 3:23:40 PM ******/
DROP TABLE IF EXISTS [dbo].[BOOK_PROMOTION]
GO
/****** Object:  Table [dbo].[BOOK]    Script Date: 4/8/2023 3:23:40 PM ******/
DROP TABLE IF EXISTS [dbo].[BOOK]
GO
/****** Object:  Table [dbo].[BILL]    Script Date: 4/8/2023 3:23:40 PM ******/
DROP TABLE IF EXISTS [dbo].[BILL]
GO
/****** Object:  Table [dbo].[ACCOUNT]    Script Date: 4/8/2023 3:23:40 PM ******/
DROP TABLE IF EXISTS [dbo].[ACCOUNT]
GO
USE [master]
GO
/****** Object:  Database [db_book]    Script Date: 4/8/2023 3:23:40 PM ******/
DROP DATABASE IF EXISTS [db_book]
GO
/****** Object:  Database [db_book]    Script Date: 4/8/2023 3:23:40 PM ******/
CREATE DATABASE [db_book]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'db_book', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\db_book.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'db_book_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\db_book_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [db_book] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [db_book].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [db_book] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [db_book] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [db_book] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [db_book] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [db_book] SET ARITHABORT OFF 
GO
ALTER DATABASE [db_book] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [db_book] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [db_book] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [db_book] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [db_book] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [db_book] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [db_book] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [db_book] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [db_book] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [db_book] SET  ENABLE_BROKER 
GO
ALTER DATABASE [db_book] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [db_book] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [db_book] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [db_book] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [db_book] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [db_book] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [db_book] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [db_book] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [db_book] SET  MULTI_USER 
GO
ALTER DATABASE [db_book] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [db_book] SET DB_CHAINING OFF 
GO
ALTER DATABASE [db_book] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [db_book] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [db_book] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [db_book] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [db_book] SET QUERY_STORE = ON
GO
ALTER DATABASE [db_book] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [db_book]
GO
/****** Object:  Table [dbo].[ACCOUNT]    Script Date: 4/8/2023 3:23:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACCOUNT](
	[id] [int] NOT NULL,
	[fullname] [nvarchar](50) NULL,
	[phone] [varchar](16) NULL,
	[address] [nvarchar](100) NULL,
	[username] [varchar](50) NULL,
	[password] [varchar](50) NULL,
	[role_id] [int] NULL,
 CONSTRAINT [PK_ACCOUNT] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BILL]    Script Date: 4/8/2023 3:23:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BILL](
	[id] [int] NOT NULL,
	[customer_id] [int] NULL,
	[total_price] [decimal](10, 2) NULL,
	[transaction_date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BOOK]    Script Date: 4/8/2023 3:23:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOOK](
	[id] [int] NOT NULL,
	[title] [nvarchar](100) NULL,
	[author] [nvarchar](50) NULL,
	[image] [varchar](255) NULL,
	[genre_id] [int] NULL,
	[description] [nvarchar](255) NULL,
	[publication_date] [date] NULL,
	[price] [decimal](10, 2) NULL,
	[quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BOOK_PROMOTION]    Script Date: 4/8/2023 3:23:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOOK_PROMOTION](
	[book_id] [int] NOT NULL,
	[promotion_id] [int] NOT NULL,
 CONSTRAINT [PK_BOOK_PROMOTION] PRIMARY KEY CLUSTERED 
(
	[book_id] ASC,
	[promotion_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DETAILED_BILL]    Script Date: 4/8/2023 3:23:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DETAILED_BILL](
	[bill_id] [int] NOT NULL,
	[book_id] [int] NOT NULL,
	[number] [int] NULL,
 CONSTRAINT [PK_DETAILED_BILL] PRIMARY KEY CLUSTERED 
(
	[bill_id] ASC,
	[book_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GENRE]    Script Date: 4/8/2023 3:23:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GENRE](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PROMOTION]    Script Date: 4/8/2023 3:23:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PROMOTION](
	[id] [int] NOT NULL,
	[start_date] [date] NULL,
	[expired_date] [date] NULL,
	[value] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ROLE]    Script Date: 4/8/2023 3:23:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ROLE](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ACCOUNT] ([id], [fullname], [phone], [address], [username], [password], [role_id]) VALUES (1, N'Nguyễn Minh Quang', N'+84839994855', N'135b Trần Hưng Đạo, phường Cầu Ông Lãnh, Quận 1, Tp.HCM', N'minhquang', NULL, 1)
INSERT [dbo].[ACCOUNT] ([id], [fullname], [phone], [address], [username], [password], [role_id]) VALUES (2, N'Lê Hoàng Khanh Nguyên', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ACCOUNT] ([id], [fullname], [phone], [address], [username], [password], [role_id]) VALUES (3, N'Lăng Thảo Thảo', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ACCOUNT] ([id], [fullname], [phone], [address], [username], [password], [role_id]) VALUES (4, N'Lê Hoài Phương', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ACCOUNT] ([id], [fullname], [phone], [address], [username], [password], [role_id]) VALUES (5, N'Bành Hảo Toàn', NULL, NULL, NULL, NULL, 2)
GO
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (1, N'The Great Gatsby', N'F. Scott Fitzgerald', N'The_Great_Gatsby.jpg', 10, NULL, CAST(N'1925-04-10' AS Date), CAST(363884.43 AS Decimal(10, 2)), 50)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (2, N'To Kill a Mockingbird', N'Harper Lee', N'To_Kill_a_Mockingbird.jpg', 10, NULL, CAST(N'1960-07-11' AS Date), CAST(295613.43 AS Decimal(10, 2)), 75)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (3, N'1984', N'George Orwell', N'1984.jpg', 3, NULL, CAST(N'1949-06-08' AS Date), CAST(250099.43 AS Decimal(10, 2)), 100)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (4, N'Pride and Prejudice', N'Jane Austen', N'Pride_and_Prejudice.jpg', 10, NULL, CAST(N'1813-01-28' AS Date), CAST(227342.43 AS Decimal(10, 2)), 120)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (5, N'The Catcher in the Rye', N'J.D. Salinger', N'The_Catcher_in_the_Rye.jpg', 31, NULL, CAST(N'1951-07-16' AS Date), CAST(272856.43 AS Decimal(10, 2)), 90)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (6, N'To the Lighthouse', N'Virginia Woolf', N'To_the_Lighthouse.jpg', 10, NULL, CAST(N'1927-05-05' AS Date), CAST(318370.43 AS Decimal(10, 2)), 60)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (7, N'The Hobbit', N'J.R.R. Tolkien', N'The_Hobbit.jpg', 1, NULL, CAST(N'1937-09-21' AS Date), CAST(341127.43 AS Decimal(10, 2)), 80)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (8, N'The Picture of Dorian Gray', N'Oscar Wilde', N'The_Picture_of_Dorian_Gray.jpg', 32, NULL, CAST(N'1890-07-01' AS Date), CAST(386641.43 AS Decimal(10, 2)), 40)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (9, N'Jane Eyre', N'Charlotte Bronte', N'Jane_Eyre.jpg', 10, NULL, CAST(N'1847-10-16' AS Date), CAST(250099.43 AS Decimal(10, 2)), 95)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (10, N'Wuthering Heights', N'Emily Bronte', N'Wuthering_Heights.jpg', 32, NULL, CAST(N'1847-12-19' AS Date), CAST(295613.43 AS Decimal(10, 2)), 70)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (11, N'The Lord of the Rings', N'J.R.R. Tolkien', N'The_Lord_of_the_Rings.jpg', 1, NULL, CAST(N'1954-07-29' AS Date), CAST(432155.43 AS Decimal(10, 2)), 85)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (12, N'One Hundred Years of Solitude', N'Gabriel Garcia Marquez', N'One_Hundred_Years_of_Solitude.jpg', 10, NULL, CAST(N'1967-06-05' AS Date), CAST(272856.43 AS Decimal(10, 2)), 65)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (13, N'The Sun Also Rises', N'Ernest Hemingway', N'The_Sun_Also_Rises.jpg', 10, NULL, CAST(N'1926-10-22' AS Date), CAST(318370.43 AS Decimal(10, 2)), 55)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (14, N'The Sound and the Fury', N'William Faulkner', N'The_Sound_and_the_Fury.jpg', 10, NULL, CAST(N'1929-10-07' AS Date), CAST(341127.43 AS Decimal(10, 2)), 45)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (15, N'Brave New World', N'Aldous Huxley', N'Brave_New_World.jpg', 3, NULL, CAST(N'1932-06-17' AS Date), CAST(227342.43 AS Decimal(10, 2)), 110)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (16, N'The Grapes of Wrath', N'John Steinbeck', N'The_Grapes_of_Wrath.jpg', 10, NULL, CAST(N'1939-04-14' AS Date), CAST(295613.43 AS Decimal(10, 2)), 75)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (17, N'Crime and Punishment', N'Fyodor Dostoevsky', N'Crime_and_Punishment.jpg', 6, NULL, CAST(N'1866-12-01' AS Date), CAST(386641.43 AS Decimal(10, 2)), 43)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (18, N'Moby-Dick', N'Herman Melville', N'Moby_Dick.jpg', 4, NULL, CAST(N'1851-10-18' AS Date), CAST(409398.43 AS Decimal(10, 2)), 35)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (19, N'The Brothers Karamazov', N'Fyodor Dostoevsky', N'The_Brothers_Karamazov.jpg', 10, NULL, CAST(N'1880-11-26' AS Date), CAST(363884.43 AS Decimal(10, 2)), 50)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (20, N'Anna Karenina', N'Leo Tolstoy', N'Anna_Karenina.jpg', 10, NULL, CAST(N'1877-01-01' AS Date), CAST(272856.43 AS Decimal(10, 2)), 90)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (21, N'The Odyssey', N'Homer', N'The_Odyssey.jpg', 10, NULL, NULL, CAST(204585.43 AS Decimal(10, 2)), 150)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (22, N'The Iliad', N'Homer', N'The_Iliad.jpg', 10, NULL, NULL, CAST(204585.43 AS Decimal(10, 2)), 140)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (23, N'The Divine Comedy', N'Dante Alighieri', N'The_Divine_Comedy.jpg', 27, NULL, CAST(N'1320-01-01' AS Date), CAST(386641.43 AS Decimal(10, 2)), 45)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (24, N'The Canterbury Tales', N'Geoffrey Chaucer', N'The_Canterbury_Tales.jpg', 10, NULL, CAST(N'1400-01-01' AS Date), CAST(318370.43 AS Decimal(10, 2)), 60)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (25, N'The Adventures of Huckleberry Finn', N'Mark Twain', N'The_Adventures_of_Huckleberry_Finn.jpg', 4, NULL, CAST(N'1884-12-10' AS Date), CAST(227342.43 AS Decimal(10, 2)), 100)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (26, N'The Adventures of Tom Sawyer', N'Mark Twain', N'The_Adventures_of_Tom_Sawyer.jpg', 4, NULL, CAST(N'1876-12-01' AS Date), CAST(204585.43 AS Decimal(10, 2)), 120)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (27, N'The War of the Worlds', N'H.G. Wells', N'The_War_of_the_Worlds.jpg', 2, NULL, CAST(N'1898-04-01' AS Date), CAST(250099.43 AS Decimal(10, 2)), 95)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (28, N'Frankenstein', N'Mary Shelley', N'Frankenstein.jpg', 7, NULL, CAST(N'1818-01-01' AS Date), CAST(272856.43 AS Decimal(10, 2)), 80)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (29, N'Dracula', N'Bram Stoker', N'Dracula.jpg', 7, NULL, CAST(N'1897-05-26' AS Date), CAST(295613.43 AS Decimal(10, 2)), 70)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (30, N'The Time Machine', N'H.G. Wells', N'The_Time_Machine.jpg', 2, NULL, CAST(N'1895-01-01' AS Date), CAST(227342.43 AS Decimal(10, 2)), 105)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (31, N'Harry Potter and the Goblet of Fire', N'J.K. Rowling', N'harry_potter_goblet_of_fire.jpg', 1, NULL, CAST(N'2000-07-08' AS Date), CAST(250099.43 AS Decimal(10, 2)), 50)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (32, N'The Lovely Bones', N'Alice Sebold', N'the_lovely_bones.jpg', 8, NULL, CAST(N'2002-07-03' AS Date), CAST(295613.43 AS Decimal(10, 2)), 30)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (33, N'The Corrections', N'Jonathan Franzen', N'the_corrections.jpg', 22, NULL, CAST(N'2001-09-01' AS Date), CAST(227342.43 AS Decimal(10, 2)), 40)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (34, N'The Secret Life of Bees', N'Sue Monk Kidd', N'secret_life_of_bees.jpg', 10, NULL, CAST(N'2002-11-08' AS Date), CAST(204585.43 AS Decimal(10, 2)), 60)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (35, N'The Da Vinci Code', N'Dan Brown', N'da_vinci_code.jpg', 8, NULL, CAST(N'2003-03-18' AS Date), CAST(272856.43 AS Decimal(10, 2)), 20)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (36, N'The Hours', N'Michael Cunningham', N'the_hours.jpg', 33, NULL, CAST(N'2000-11-11' AS Date), CAST(181828.43 AS Decimal(10, 2)), 25)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (37, N'White Teeth', N'Zadie Smith', N'white_teeth.jpg', 33, NULL, CAST(N'2000-01-27' AS Date), CAST(250099.43 AS Decimal(10, 2)), 35)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (38, N'Life of Pi', N'Yann Martel', N'life_of_pi.jpg', 4, NULL, CAST(N'2001-09-11' AS Date), CAST(318370.43 AS Decimal(10, 2)), 45)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (39, N'The Road', N'Cormac McCarthy', N'the_road.jpg', 3, NULL, CAST(N'2006-09-26' AS Date), CAST(204585.43 AS Decimal(10, 2)), 30)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (40, N'The Kite Runner', N'Khaled Hosseini', N'the_kite_runner.jpg', 10, NULL, CAST(N'2003-05-29' AS Date), CAST(227342.43 AS Decimal(10, 2)), 40)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (41, N'Middlesex', N'Jeffrey Eugenides', N'middlesex.jpg', 9, NULL, CAST(N'2002-09-04' AS Date), CAST(272856.43 AS Decimal(10, 2)), 50)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (42, N'Atonement', N'Ian McEwan', N'atonement.jpg', 10, NULL, CAST(N'2001-09-10' AS Date), CAST(295613.43 AS Decimal(10, 2)), 20)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (43, N'The Curious Incident of the Dog in the Night-Time', N'Mark Haddon', N'curious_incident.jpg', 11, NULL, CAST(N'2003-05-18' AS Date), CAST(227342.43 AS Decimal(10, 2)), 30)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (44, N'The Catcher in the Rye', N'J.D. Salinger', N'catcher_in_the_rye.jpg', 31, NULL, CAST(N'2001-08-15' AS Date), CAST(181828.43 AS Decimal(10, 2)), 35)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (45, N'The Poisonwood Bible', N'Barbara Kingsolver', N'poisonwood_bible.jpg', 10, NULL, CAST(N'2004-03-01' AS Date), CAST(250099.43 AS Decimal(10, 2)), 40)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (46, N'Bel Canto', N'Ann Patchett', N'bel_canto.jpg', 33, NULL, CAST(N'2001-05-22' AS Date), CAST(204585.43 AS Decimal(10, 2)), 25)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (47, N'The Girl with the Dragon Tattoo', N'Stieg Larsson', N'girl_with_dragon_tattoo.jpg', 30, NULL, CAST(N'2005-08-01' AS Date), CAST(341127.43 AS Decimal(10, 2)), 30)
INSERT [dbo].[BOOK] ([id], [title], [author], [image], [genre_id], [description], [publication_date], [price], [quantity]) VALUES (48, N'The Memory Keeper''s Daughter', N'Kim Edwards', N'memory_keepers_daughter.jpg', 33, NULL, CAST(N'2005-06-14' AS Date), CAST(295613.43 AS Decimal(10, 2)), 20)
GO
INSERT [dbo].[GENRE] ([id], [name]) VALUES (1, N'Fantasy')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (2, N'Science Fiction')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (3, N'Dystopian')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (4, N'Adventure')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (5, N'Romance')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (6, N'Detective & Mystery')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (7, N'Horror')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (8, N'Thriller')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (9, N'LGBTQ+')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (10, N'Historical Fiction')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (11, N'Young Adult (YA)')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (12, N'Children’s Fiction')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (13, N'Memoir & Autobiography')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (14, N'Biography')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (15, N'Cooking')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (16, N'Art & Photography')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (17, N'Self-Help/Personal Development')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (18, N'Motivational/Inspirational')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (19, N'Health & Fitness')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (20, N'History')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (21, N'Crafts, Hobbies & Home')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (22, N'Families & Relationships')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (23, N'Humor & Entertainment')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (24, N'Business & Money')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (25, N'Law & Criminology')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (26, N'Politics & Social Sciences')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (27, N'Religion & Spirituality')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (28, N'Education & Teaching')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (29, N'Travel')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (30, N'True Crime')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (31, N'Bildungsroman (coming-of-age novel)')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (32, N'Gothic Fiction')
INSERT [dbo].[GENRE] ([id], [name]) VALUES (33, N'Literary Fiction')
GO
INSERT [dbo].[ROLE] ([id], [name]) VALUES (1, N'Admin')
INSERT [dbo].[ROLE] ([id], [name]) VALUES (2, N'Customer')
GO
ALTER TABLE [dbo].[ACCOUNT]  WITH CHECK ADD  CONSTRAINT [FK_ACCOUNT_ROLE] FOREIGN KEY([role_id])
REFERENCES [dbo].[ROLE] ([id])
GO
ALTER TABLE [dbo].[ACCOUNT] CHECK CONSTRAINT [FK_ACCOUNT_ROLE]
GO
ALTER TABLE [dbo].[BILL]  WITH CHECK ADD  CONSTRAINT [FK_BILL_ACCOUNT] FOREIGN KEY([customer_id])
REFERENCES [dbo].[ACCOUNT] ([id])
GO
ALTER TABLE [dbo].[BILL] CHECK CONSTRAINT [FK_BILL_ACCOUNT]
GO
ALTER TABLE [dbo].[BOOK]  WITH CHECK ADD  CONSTRAINT [FK_BOOK_GENRE] FOREIGN KEY([genre_id])
REFERENCES [dbo].[GENRE] ([id])
GO
ALTER TABLE [dbo].[BOOK] CHECK CONSTRAINT [FK_BOOK_GENRE]
GO
ALTER TABLE [dbo].[BOOK_PROMOTION]  WITH CHECK ADD  CONSTRAINT [FK_BOOK_PROMOTION_BOOK] FOREIGN KEY([book_id])
REFERENCES [dbo].[BOOK] ([id])
GO
ALTER TABLE [dbo].[BOOK_PROMOTION] CHECK CONSTRAINT [FK_BOOK_PROMOTION_BOOK]
GO
ALTER TABLE [dbo].[BOOK_PROMOTION]  WITH CHECK ADD  CONSTRAINT [FK_BOOK_PROMOTION_PROMOTION] FOREIGN KEY([promotion_id])
REFERENCES [dbo].[PROMOTION] ([id])
GO
ALTER TABLE [dbo].[BOOK_PROMOTION] CHECK CONSTRAINT [FK_BOOK_PROMOTION_PROMOTION]
GO
ALTER TABLE [dbo].[DETAILED_BILL]  WITH CHECK ADD  CONSTRAINT [FK_DETAILED_BILL_BILL] FOREIGN KEY([bill_id])
REFERENCES [dbo].[BILL] ([id])
GO
ALTER TABLE [dbo].[DETAILED_BILL] CHECK CONSTRAINT [FK_DETAILED_BILL_BILL]
GO
ALTER TABLE [dbo].[DETAILED_BILL]  WITH CHECK ADD  CONSTRAINT [FK_DETAILED_BILL_BOOK] FOREIGN KEY([book_id])
REFERENCES [dbo].[BOOK] ([id])
GO
ALTER TABLE [dbo].[DETAILED_BILL] CHECK CONSTRAINT [FK_DETAILED_BILL_BOOK]
GO
USE [master]
GO
ALTER DATABASE [db_book] SET  READ_WRITE 
GO
