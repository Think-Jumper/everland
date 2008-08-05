USE [eland]
GO
/****** Object:  Table [dbo].[HexType]    Script Date: 08/05/2008 15:51:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HexType](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_HexType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 08/05/2008 15:52:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[OpenId] [varchar](500) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Race]    Script Date: 08/05/2008 15:52:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Race](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Race] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Game]    Script Date: 08/05/2008 15:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Game](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Started] [datetime] NOT NULL,
	[Finished] [datetime] NULL,
 CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[World]    Script Date: 08/05/2008 15:52:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[World](
	[Id] [uniqueidentifier] NOT NULL,
	[GameId] [uniqueidentifier] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Height] [int] NOT NULL,
	[Width] [int] NOT NULL,
 CONSTRAINT [PK_World] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nation]    Script Date: 08/05/2008 15:51:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nation](
	[Id] [uniqueidentifier] NOT NULL,
	[RaceId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Nation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hex]    Script Date: 08/05/2008 15:51:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hex](
	[Id] [uniqueidentifier] NOT NULL,
	[WorldId] [uniqueidentifier] NOT NULL,
	[HexTypeId] [uniqueidentifier] NOT NULL,
	[X] [int] NOT NULL,
	[Y] [int] NOT NULL,
 CONSTRAINT [PK_Hex] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameSession]    Script Date: 08/05/2008 15:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameSession](
	[Id] [uniqueidentifier] NOT NULL,
	[GameId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[NationId] [uniqueidentifier] NOT NULL,
	[EnteredGame] [datetime] NOT NULL,
	[LeftGame] [datetime] NULL,
 CONSTRAINT [PK_GameSession] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_GameSession_Game]    Script Date: 08/05/2008 15:51:59 ******/
ALTER TABLE [dbo].[GameSession]  WITH CHECK ADD  CONSTRAINT [FK_GameSession_Game] FOREIGN KEY([GameId])
REFERENCES [dbo].[Game] ([Id])
GO
ALTER TABLE [dbo].[GameSession] CHECK CONSTRAINT [FK_GameSession_Game]
GO
/****** Object:  ForeignKey [FK_GameSession_Nation]    Script Date: 08/05/2008 15:51:59 ******/
ALTER TABLE [dbo].[GameSession]  WITH CHECK ADD  CONSTRAINT [FK_GameSession_Nation] FOREIGN KEY([NationId])
REFERENCES [dbo].[Nation] ([Id])
GO
ALTER TABLE [dbo].[GameSession] CHECK CONSTRAINT [FK_GameSession_Nation]
GO
/****** Object:  ForeignKey [FK_GameSession_Users]    Script Date: 08/05/2008 15:51:59 ******/
ALTER TABLE [dbo].[GameSession]  WITH CHECK ADD  CONSTRAINT [FK_GameSession_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[GameSession] CHECK CONSTRAINT [FK_GameSession_Users]
GO
/****** Object:  ForeignKey [FK_Hex_HexType]    Script Date: 08/05/2008 15:51:59 ******/
ALTER TABLE [dbo].[Hex]  WITH CHECK ADD  CONSTRAINT [FK_Hex_HexType] FOREIGN KEY([HexTypeId])
REFERENCES [dbo].[HexType] ([Id])
GO
ALTER TABLE [dbo].[Hex] CHECK CONSTRAINT [FK_Hex_HexType]
GO
/****** Object:  ForeignKey [FK_Hex_World]    Script Date: 08/05/2008 15:51:59 ******/
ALTER TABLE [dbo].[Hex]  WITH CHECK ADD  CONSTRAINT [FK_Hex_World] FOREIGN KEY([WorldId])
REFERENCES [dbo].[World] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Hex] CHECK CONSTRAINT [FK_Hex_World]
GO
/****** Object:  ForeignKey [FK_Nation_Race]    Script Date: 08/05/2008 15:51:59 ******/
ALTER TABLE [dbo].[Nation]  WITH CHECK ADD  CONSTRAINT [FK_Nation_Race] FOREIGN KEY([RaceId])
REFERENCES [dbo].[Race] ([Id])
GO
ALTER TABLE [dbo].[Nation] CHECK CONSTRAINT [FK_Nation_Race]
GO
/****** Object:  ForeignKey [FK_World_Game]    Script Date: 08/05/2008 15:52:00 ******/
ALTER TABLE [dbo].[World]  WITH CHECK ADD  CONSTRAINT [FK_World_Game] FOREIGN KEY([GameId])
REFERENCES [dbo].[Game] ([Id])
GO
ALTER TABLE [dbo].[World] CHECK CONSTRAINT [FK_World_Game]
GO
