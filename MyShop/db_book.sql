USE MASTER
GO
IF DB_ID('db_book') IS NOT NULL
	DROP DATABASE [db_book]
GO
CREATE DATABASE [db_book]
GO
USE [db_book]
GO
CREATE TABLE [dbo].[ACCOUNT](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fullname] [nvarchar](50) NULL,
	[phone] [varchar](16) NULL,
	[address] [nvarchar](100) NULL,
	[username] [nvarchar](50) NULL,
	[password] [varchar](max) NULL,
	[entropy] [nvarchar](max) NULL,
	[role_id] [int] NULL,
 CONSTRAINT [PK_ACCOUNT] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BILL]    Script Date: 4/18/2023 8:46:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BILL](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[customer_id] [int] NULL,
	[total_price] [int] NULL,
	[transaction_date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BOOK]    Script Date: 4/18/2023 8:46:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOOK](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](100) NULL,
	[author] [nvarchar](50) NULL,
	[image] [varchar](255) NULL,
	[genre_id] [int] NULL,
	[description] [nvarchar](max) NULL,
	[published_date] [date] NULL,
	[price] [int] NULL,
	[quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BOOK_PROMOTION]    Script Date: 4/18/2023 8:46:55 AM ******/
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
/****** Object:  Table [dbo].[DETAILED_BILL]    Script Date: 4/18/2023 8:46:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DETAILED_BILL](
	[bill_id] [int] NOT NULL,
	[book_id] [int] NOT NULL,
	[number] [int] NULL,
	[price] [int] NULL,
 CONSTRAINT [PK_DETAILED_BILL] PRIMARY KEY CLUSTERED 
(
	[bill_id] ASC,
	[book_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GENRE]    Script Date: 4/18/2023 8:46:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GENRE](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PROMOTION]    Script Date: 4/18/2023 8:46:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PROMOTION](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[start_date] [date] NULL,
	[expired_date] [date] NULL,
	[value] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ROLE]    Script Date: 4/18/2023 8:46:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ROLE](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
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
USE [db_book]
GO
INSERT [dbo].[GENRE] ([name]) VALUES (N'Fantasy')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Science Fiction')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Dystopian')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Adventure')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Romance')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Detective & Mystery')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Horror')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Thriller')
INSERT [dbo].[GENRE] ([name]) VALUES (N'LGBTQ+')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Historical Fiction')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Young Adult (YA)')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Children’s Fiction')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Memoir & Autobiography')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Biography')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Cooking')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Art & Photography')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Self-Help/Personal Development')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Motivational/Inspirational')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Health & Fitness')
INSERT [dbo].[GENRE] ([name]) VALUES (N'History')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Crafts, Hobbies & Home')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Families & Relationships')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Humor & Entertainment')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Business & Money')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Law & Criminology')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Politics & Social Sciences')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Religion & Spirituality')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Education & Teaching')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Travel')
INSERT [dbo].[GENRE] ([name]) VALUES (N'True Crime')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Bildungsroman (coming-of-age novel)')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Gothic Fiction')
INSERT [dbo].[GENRE] ([name]) VALUES (N'Literary Fiction')
GO
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Great Gatsby', N'F. Scott Fitzgerald', N'The_Great_Gatsby.jpg', 10, NULL, CAST(N'1925-04-10' AS Date), 364000, 50)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'To Kill a Mockingbird', N'Harper Lee', N'To_Kill_a_Mockingbird.jpg', 10, NULL, CAST(N'1960-07-11' AS Date), 296000, 75)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'1984', N'George Orwell', N'1984.jpg', 3, NULL, CAST(N'1949-06-08' AS Date), 250000, 100)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'Pride and Prejudice', N'Jane Austen', N'Pride_and_Prejudice.jpg', 10, NULL, CAST(N'1813-01-28' AS Date), 227000, 120)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Catcher in the Rye', N'J.D. Salinger', N'The_Catcher_in_the_Rye.jpg', 31, NULL, CAST(N'1951-07-16' AS Date), 273000, 90)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'To the Lighthouse', N'Virginia Woolf', N'To_the_Lighthouse.jpg', 10, NULL, CAST(N'1927-05-05' AS Date), 318000, 60)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Hobbit', N'J.R.R. Tolkien', N'The_Hobbit.jpg', 1, NULL, CAST(N'1937-09-21' AS Date), 341000, 80)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Picture of Dorian Gray', N'Oscar Wilde', N'The_Picture_of_Dorian_Gray.jpg', 32, NULL, CAST(N'1890-07-01' AS Date), 387000, 40)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'Jane Eyre', N'Charlotte Bronte', N'Jane_Eyre.jpg', 10, NULL, CAST(N'1847-10-16' AS Date), 250000, 95)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'Wuthering Heights', N'Emily Bronte', N'Wuthering_Heights.jpg', 32, NULL, CAST(N'1847-12-19' AS Date), 296000, 70)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Lord of the Rings', N'J.R.R. Tolkien', N'The_Lord_of_the_Rings.jpg', 1, NULL, CAST(N'1954-07-29' AS Date), 432000, 85)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'One Hundred Years of Solitude', N'Gabriel Garcia Marquez', N'One_Hundred_Years_of_Solitude.jpg', 10, NULL, CAST(N'1967-06-05' AS Date), 273000, 65)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Sun Also Rises', N'Ernest Hemingway', N'The_Sun_Also_Rises.jpg', 10, NULL, CAST(N'1926-10-22' AS Date), 318000, 55)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Sound and the Fury', N'William Faulkner', N'The_Sound_and_the_Fury.jpg', 10, NULL, CAST(N'1929-10-07' AS Date), 341000, 45)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'Brave New World', N'Aldous Huxley', N'Brave_New_World.jpg', 3, NULL, CAST(N'1932-06-17' AS Date), 227000, 110)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Grapes of Wrath', N'John Steinbeck', N'The_Grapes_of_Wrath.jpg', 10, NULL, CAST(N'1939-04-14' AS Date), 296000, 75)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'Crime and Punishment', N'Fyodor Dostoevsky', N'Crime_and_Punishment.jpg', 6, NULL, CAST(N'1866-12-01' AS Date), 387000, 43)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'Moby-Dick', N'Herman Melville', N'Moby_Dick.jpg', 4, NULL, CAST(N'1851-10-18' AS Date), 409000, 35)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Brothers Karamazov', N'Fyodor Dostoevsky', N'The_Brothers_Karamazov.jpg', 10, NULL, CAST(N'1880-11-26' AS Date), 364000, 50)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'Anna Karenina', N'Leo Tolstoy', N'Anna_Karenina.jpg', 10, NULL, CAST(N'1877-01-01' AS Date), 273000, 90)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Odyssey', N'Homer', N'The_Odyssey.jpg', 10, NULL, NULL, 205000, 150)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Iliad', N'Homer', N'The_Iliad.jpg', 10, NULL, NULL, 205000, 140)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Divine Comedy', N'Dante Alighieri', N'The_Divine_Comedy.jpg', 27, NULL, CAST(N'1320-01-01' AS Date), 387000, 45)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Canterbury Tales', N'Geoffrey Chaucer', N'The_Canterbury_Tales.jpg', 10, NULL, CAST(N'1400-01-01' AS Date), 318000, 60)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Adventures of Huckleberry Finn', N'Mark Twain', N'The_Adventures_of_Huckleberry_Finn.jpg', 4, NULL, CAST(N'1884-12-10' AS Date), 227000, 100)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Adventures of Tom Sawyer', N'Mark Twain', N'The_Adventures_of_Tom_Sawyer.jpg', 4, NULL, CAST(N'1876-12-01' AS Date), 205000, 120)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The War of the Worlds', N'H.G. Wells', N'The_War_of_the_Worlds.jpg', 2, NULL, CAST(N'1898-04-01' AS Date), 250000, 95)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'Frankenstein', N'Mary Shelley', N'Frankenstein.jpg', 7, NULL, CAST(N'1818-01-01' AS Date), 273000, 80)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'Dracula', N'Bram Stoker', N'Dracula.jpg', 7, NULL, CAST(N'1897-05-26' AS Date), 296000, 70)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Time Machine', N'H.G. Wells', N'The_Time_Machine.jpg', 2, NULL, CAST(N'1895-01-01' AS Date), 227000, 105)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'Harry Potter and the Goblet of Fire', N'J.K. Rowling', N'harry_potter_goblet_of_fire.jpg', 1, NULL, CAST(N'2000-07-08' AS Date), 250000, 50)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Lovely Bones', N'Alice Sebold', N'the_lovely_bones.jpg', 8, NULL, CAST(N'2002-07-03' AS Date), 296000, 30)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Corrections', N'Jonathan Franzen', N'the_corrections.jpg', 22, NULL, CAST(N'2001-09-01' AS Date), 227000, 40)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Secret Life of Bees', N'Sue Monk Kidd', N'secret_life_of_bees.jpg', 10, NULL, CAST(N'2002-11-08' AS Date), 205000, 60)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Da Vinci Code', N'Dan Brown', N'da_vinci_code.jpg', 8, NULL, CAST(N'2003-03-18' AS Date), 273000, 20)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Hours', N'Michael Cunningham', N'the_hours.jpg', 33, NULL, CAST(N'2000-11-11' AS Date), 182000, 25)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'White Teeth', N'Zadie Smith', N'white_teeth.jpg', 33, NULL, CAST(N'2000-01-27' AS Date), 250000, 35)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'Life of Pi', N'Yann Martel', N'life_of_pi.jpg', 4, NULL, CAST(N'2001-09-11' AS Date), 318000, 45)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Road', N'Cormac McCarthy', N'the_road.jpg', 3, NULL, CAST(N'2006-09-26' AS Date), 205000, 30)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Kite Runner', N'Khaled Hosseini', N'the_kite_runner.jpg', 10, NULL, CAST(N'2003-05-29' AS Date), 227000, 40)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'Middlesex', N'Jeffrey Eugenides', N'middlesex.jpg', 9, NULL, CAST(N'2002-09-04' AS Date), 273000, 50)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'Atonement', N'Ian McEwan', N'atonement.jpg', 10, NULL, CAST(N'2001-09-10' AS Date), 296000, 20)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Curious Incident of the Dog in the Night-Time', N'Mark Haddon', N'curious_incident.jpg', 11, NULL, CAST(N'2003-05-18' AS Date), 227000, 30)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Catcher in the Rye', N'J.D. Salinger', N'catcher_in_the_rye.jpg', 31, NULL, CAST(N'2001-08-15' AS Date), 182000, 35)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Poisonwood Bible', N'Barbara Kingsolver', N'poisonwood_bible.jpg', 10, NULL, CAST(N'2004-03-01' AS Date), 250000, 40)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'Bel Canto', N'Ann Patchett', N'bel_canto.jpg', 33, NULL, CAST(N'2001-05-22' AS Date), 205000, 25)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Girl with the Dragon Tattoo', N'Stieg Larsson', N'girl_with_dragon_tattoo.jpg', 30, NULL, CAST(N'2005-08-01' AS Date), 341000, 30)
INSERT [dbo].[BOOK] ([title], [author], [image], [genre_id], [description], [published_date], [price], [quantity]) VALUES (N'The Memory Keeper''s Daughter', N'Kim Edwards', N'memory_keepers_daughter.jpg', 33, NULL, CAST(N'2005-06-14' AS Date), 296000, 20)
GO
INSERT [dbo].[ROLE] ([name]) VALUES (N'Admin')
INSERT [dbo].[ROLE] ([name]) VALUES (N'Customer')
GO
INSERT [dbo].[ACCOUNT] ([fullname], [phone], [address], [username], [password], [entropy], [role_id]) VALUES (N'Nguyễn Minh Quang', N'+84839994855', N'135b Trần Hưng Đạo, phường Cầu Ông Lãnh, Quận 1, Tp.HCM', N'minhquang', N'AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA1Sr0zDCzkkCcOOWMW9PduQAAAAACAAAAAAAQZgAAAAEAACAAAACHZspEIzx7Ll9Jh/c1o5IzNTMdp2uZUuyOtyRLu4LguwAAAAAOgAAAAAIAACAAAAAWYkyIXWixVobJUbvzipYMoMxybiQZLMD8utiLLTKAoyAAAACDYogNtv5DZsQa01IIfTBDOQUjng2tGmdnUy1IGt2ZnUAAAAAbtodErymkTFZL6Yqk6ekpTa90P80qf9t8M0sIkI4BP8XqafCDvvkGmHs6TZrLwkaOmXhcIcRuswdmwTIdr/bo', N'FuFUajN7GLvPwopHRuw3cE99sOM=', 1)
INSERT [dbo].[ACCOUNT] ([fullname], [phone], [address], [username], [password], [entropy], [role_id]) VALUES (N'Lê Hoàng Khanh Nguyên', NULL, NULL, N'khanhnguyen', N'AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA1Sr0zDCzkkCcOOWMW9PduQAAAAACAAAAAAAQZgAAAAEAACAAAACHZspEIzx7Ll9Jh/c1o5IzNTMdp2uZUuyOtyRLu4LguwAAAAAOgAAAAAIAACAAAAAWYkyIXWixVobJUbvzipYMoMxybiQZLMD8utiLLTKAoyAAAACDYogNtv5DZsQa01IIfTBDOQUjng2tGmdnUy1IGt2ZnUAAAAAbtodErymkTFZL6Yqk6ekpTa90P80qf9t8M0sIkI4BP8XqafCDvvkGmHs6TZrLwkaOmXhcIcRuswdmwTIdr/bo', N'FuFUajN7GLvPwopHRuw3cE99sOM=', 1)
INSERT [dbo].[ACCOUNT] ([fullname], [phone], [address], [username], [password], [entropy], [role_id]) VALUES (N'Lăng Thảo Thảo', NULL, NULL, N'langthao', N'AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA1Sr0zDCzkkCcOOWMW9PduQAAAAACAAAAAAAQZgAAAAEAACAAAACHZspEIzx7Ll9Jh/c1o5IzNTMdp2uZUuyOtyRLu4LguwAAAAAOgAAAAAIAACAAAAAWYkyIXWixVobJUbvzipYMoMxybiQZLMD8utiLLTKAoyAAAACDYogNtv5DZsQa01IIfTBDOQUjng2tGmdnUy1IGt2ZnUAAAAAbtodErymkTFZL6Yqk6ekpTa90P80qf9t8M0sIkI4BP8XqafCDvvkGmHs6TZrLwkaOmXhcIcRuswdmwTIdr/bo', N'FuFUajN7GLvPwopHRuw3cE99sOM=', 1)
INSERT [dbo].[ACCOUNT] ([fullname], [phone], [address], [username], [password], [entropy], [role_id]) VALUES (N'Lê Hoài Phương', NULL, NULL, N'hoaiphuong', N'AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA1Sr0zDCzkkCcOOWMW9PduQAAAAACAAAAAAAQZgAAAAEAACAAAACHZspEIzx7Ll9Jh/c1o5IzNTMdp2uZUuyOtyRLu4LguwAAAAAOgAAAAAIAACAAAAAWYkyIXWixVobJUbvzipYMoMxybiQZLMD8utiLLTKAoyAAAACDYogNtv5DZsQa01IIfTBDOQUjng2tGmdnUy1IGt2ZnUAAAAAbtodErymkTFZL6Yqk6ekpTa90P80qf9t8M0sIkI4BP8XqafCDvvkGmHs6TZrLwkaOmXhcIcRuswdmwTIdr/bo', N'FuFUajN7GLvPwopHRuw3cE99sOM=', 1)
INSERT [dbo].[ACCOUNT] ([fullname], [phone], [address], [username], [password], [entropy], [role_id]) VALUES (N'Bành Hảo Toàn', NULL, NULL, NULL, NULL, NULL, 2)
GO


declare @date_from as date
set @date_from = '4-10-2023'
declare @date_to as date
set @date_to = '4-18-2023'
select id,customer_id,total_price,transaction_date from BILL where transaction_date between @date_from and @date_to	
select * from BOOK

select * from DETAILED_BILL

declare @total_price as INT
set @total_price = 0

select id from BILL where total_price=@total_price

select db.price,db.number,db.book_id,b.quantity,b.title
from DETAILED_BILL as db
join BOOk as b on db.book_id=b.id
where db.bill_id=13

--lay thong ke theo ngay
create procedure GetDailyRevenue @start_date date, @end_date date
as
begin

	declare @date date
	declare @result table(transaction_date DATE, total_price INT)
	declare @temp_value INT

	set @date = @start_date
	--insert into result table the list of tupples(@date-price) that @start_date <= @date <= @end_date
	while @date <= @end_date
	begin
		set @temp_value = (select bill.total_price from BILL as bill where bill.transaction_date = @date)
		if(@temp_value is null)
		begin
			set @temp_value= 0
		end

		insert into @result(transaction_date, total_price) values(@date, @temp_value)
		--increase @date
		set @date = DATEADD(DAY, 1, @date)
	end

	select result.transaction_date as 'date', result.total_price as 'revenue'  from @result as result
	return
end

go

--lay thong ke tuan
--lay danh sach cac tuan dang co
create procedure GetListOfWeeks
AS
begin
declare @currentWeekOffSet INT = 0;
declare @startDate DATE = (select min(bill.transaction_date) from BILL as bill)
declare @maxWeek INT = (select weeks.*
from (select DATEDIFF(DAY, min(bill.transaction_date), GETDATE())/7 as 'tong_tuan'
from BILL as bill) as weeks)

declare @weekTable table(weekID INT, startDateOfWeek DATE)

while @currentWeekOffSet < @maxWeek
begin
	insert into @weekTable(weekID, startDateOfWeek) values(@currentWeekOffSet, DATEADD(DAY, @currentWeekOffSet*7, @startDate))
	set @currentWeekOffSet = @currentWeekOffSet + 1;
end

select wt.weekID as 'week', wt.startDateOfWeek 'start_date'
from @weekTable as wt
return
end

--bat dau lay thong ke theo tuan
go
--thuc hien lay thong ke theo tuan
create procedure GetWeeklyRevenue @start_date_start_week DATE, @start_date_end_week DATE
as
begin
	declare @total_revenue_of_a_week INT
	--declare @temp_value INT
	declare @exec_date DATE
	declare @flag_date DATE
	declare @result table(start_date_of_week DATE, total_revenue INT) 
	
	set @total_revenue_of_a_week = 0
	set @exec_date = @start_date_start_week
	set @flag_date = @exec_date
	
	while @exec_date < @start_date_end_week
	begin
		--set @temp_value = (select bill.total_price from BILL as bill where bill.transaction_date = @exec_date)
		--if(@temp_value is null)
		--begin
			--set @temp_value = 0
		--end
		--set @total_revenue_of_a_week = @total_revenue_of_a_week + @temp_value
		--set @exec_date = DATEADD(day, 1, @exec_date)
		--if(DATEDIFF(DAY, @flag_date, @exec_date) = 7)
		--begin
			--insert into @result(start_date_of_week, total_revenue) values(@flag_date, @total_revenue_of_a_week)
			--set @total_revenue_of_a_week = 0
			--set @flag_date = @exec_date
		--end
		set @flag_date = @exec_date
		set @exec_date = DATEADD(DAY, 7, @flag_date)
		set @total_revenue_of_a_week = (select SUM(bill.total_price) from BILL as bill where bill.transaction_date >= @flag_date and bill.transaction_date <= @exec_date)
		if(@total_revenue_of_a_week is null)
		begin
			set @total_revenue_of_a_week = 0
		end
		insert into @result(start_date_of_week, total_revenue) values(@flag_date, @total_revenue_of_a_week)
	end

	select result.start_date_of_week as 'date', result.total_revenue as 'revenue' from @result as result
	return 

end
go

--lay thong ke theo thang
create procedure GetMonthlyRevenue @start_date_start_month DATE, @start_date_end_month DATE
as
begin
	declare @total_revenue_month INT
	declare @exec_date DATE
	declare @flag_date DATE
	declare @result table(start_date_of_month DATE, total_revenue INT)

	--initialize
	set @exec_date = @start_date_start_month
	set @flag_date = @exec_date

	while @exec_date < @start_date_end_month
	begin
		set @flag_date = @exec_date
		set @exec_date = DATEADD(MONTH, 1, @flag_date)
		set @total_revenue_month = (select SUM(bill.total_price) from BILL as bill where bill.transaction_date >= @flag_date and bill.transaction_date <= @exec_date)
		if(@total_revenue_month is null)
		begin
			set @total_revenue_month = 0
		end
		insert into @result(start_date_of_month, total_revenue) values(@flag_date, @total_revenue_month)
	end

	select result.start_date_of_month as 'date', result.total_revenue as 'revenue' from @result as result
	return
end

--lay thong tin thong ke theo nam
go
--thuc hien lay thong ke theo thang
create procedure GetYearlyRevenue @start_date_start_year DATE, @start_date_end_year DATE
as
begin
	declare @exec_date DATE
	declare @flag_date DATE
	declare @total_revenue_a_year INT
	declare @result table(start_date_of_year DATE, total_revenue INT)

	set @exec_date = @start_date_start_year
	set @flag_date = @exec_date
	set @total_revenue_a_year = 0

	while @exec_date < @start_date_end_year
	begin
		set @flag_date = @exec_date
		set @exec_date = DATEADD(YEAR, 1, @exec_date)
		set @total_revenue_a_year = (select SUM(bill.total_price) from BILL as bill where bill.transaction_date >= @flag_date and bill.transaction_date <= @exec_date)
		if(@total_revenue_a_year is null)
		begin
			set @total_revenue_a_year = 0
		end
		insert into @result(start_date_of_year, total_revenue) values(@flag_date, @total_revenue_a_year)
	end

	select result.start_date_of_year as 'date', result.total_revenue as 'revenue' from @result as result
	return
end
go
