USE [Library]
GO

ALTER TABLE [dbo].[Library] DROP CONSTRAINT [FK_Library_Client]
GO

ALTER TABLE [dbo].[Library] DROP CONSTRAINT [FK_Library_Book]
GO

/****** Object:  Table [dbo].[Library]    Script Date: 15.05.2023 10:56:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Library]') AND type in (N'U'))
DROP TABLE [dbo].[Library]
GO

/****** Object:  Table [dbo].[Library]    Script Date: 15.05.2023 10:56:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Library](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_Book] [int] NOT NULL,
	[ID_Client] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Active] [tinyint] NOT NULL,
 CONSTRAINT [PK_Library] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Library]  WITH CHECK ADD  CONSTRAINT [FK_Library_Book] FOREIGN KEY([ID_Book])
REFERENCES [dbo].[Book] ([ID])
GO

ALTER TABLE [dbo].[Library] CHECK CONSTRAINT [FK_Library_Book]
GO

ALTER TABLE [dbo].[Library]  WITH CHECK ADD  CONSTRAINT [FK_Library_Client] FOREIGN KEY([ID_Client])
REFERENCES [dbo].[Client] ([ID])
GO

ALTER TABLE [dbo].[Library] CHECK CONSTRAINT [FK_Library_Client]
GO


