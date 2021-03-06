USE [pirates]
GO
/****** Object:  Table [dbo].[pirates]    Script Date: 9/27/2016 5:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pirates](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[rank] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[pirates_ships]    Script Date: 9/27/2016 5:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pirates_ships](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ships_id] [int] NULL,
	[pirates_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ships]    Script Date: 9/27/2016 5:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ships](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[shiptype] [varchar](255) NULL
) ON [PRIMARY]

GO
