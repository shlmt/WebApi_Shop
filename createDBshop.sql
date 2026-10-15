USE [shop]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 15/10/2026 21:39:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[categoryId] [int] IDENTITY(1,1) NOT NULL,
	[categoryName] [nchar](30) NOT NULL,
 CONSTRAINT [PK_CategoryId] PRIMARY KEY CLUSTERED 
(
	[categoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItems]    Script Date: 15/10/2026 21:39:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItems](
	[orderItemId] [int] IDENTITY(1,1) NOT NULL,
	[productId] [int] NOT NULL,
	[orderId] [int] NOT NULL,
	[quantity] [int] NOT NULL,
 CONSTRAINT [PK_OrderItems] PRIMARY KEY CLUSTERED 
(
	[orderItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 15/10/2026 21:39:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[orderId] [int] IDENTITY(1,1) NOT NULL,
	[orderDate] [datetime2](7) NOT NULL,
	[orderSum] [int] NOT NULL,
	[userId] [int] NOT NULL,
 CONSTRAINT [PK_OrderId] PRIMARY KEY CLUSTERED 
(
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 15/10/2026 21:39:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[productId] [int] IDENTITY(1,1) NOT NULL,
	[productName] [nchar](10) NOT NULL,
	[price] [int] NOT NULL,
	[categoryId] [int] NOT NULL,
	[description] [nchar](50) NULL,
	[imageUrl] [nchar](50) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 15/10/2026 21:39:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [nchar](20) NOT NULL,
	[lastName] [nchar](20) NOT NULL,
	[email] [nchar](20) NULL,
	[password] [nchar](20) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK__OrderItem__order__4316F928] FOREIGN KEY([orderId])
REFERENCES [dbo].[Orders] ([orderId])
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK__OrderItem__order__4316F928]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK__OrderItem__produ__440B1D61] FOREIGN KEY([productId])
REFERENCES [dbo].[Products] ([productId])
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK__OrderItem__produ__440B1D61]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK__Orders__userId__4222D4EF] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK__Orders__userId__4222D4EF]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK__Products__catego__44FF419A] FOREIGN KEY([categoryId])
REFERENCES [dbo].[Categories] ([categoryId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK__Products__catego__44FF419A]
GO


