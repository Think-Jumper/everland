USE [eland]
GO
/****** Object:  Table [dbo].[World]    Script Date: 06/16/2009 15:37:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[World](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Height] [int] NOT NULL,
	[Width] [int] NOT NULL,
 CONSTRAINT [PK_World] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UnitType]    Script Date: 06/16/2009 15:37:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnitType](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UnitType_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[UnitType] ([Id], [Name]) VALUES (1, N'Peasant')
INSERT [dbo].[UnitType] ([Id], [Name]) VALUES (2, N'Soldier')
INSERT [dbo].[UnitType] ([Id], [Name]) VALUES (3, N'Archer')
INSERT [dbo].[UnitType] ([Id], [Name]) VALUES (4, N'Clubman')
/****** Object:  Table [dbo].[Unit]    Script Date: 06/16/2009 15:37:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Unit](
	[Id] [uniqueidentifier] NOT NULL,
	[Type] [int] NOT NULL,
	[Health] [int] NOT NULL,
	[MaximumHealth] [int] NOT NULL,
	[NationId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HexType]    Script Date: 06/16/2009 15:37:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HexType](
	[Id] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_HexType_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[HexType] ([Id], [Name]) VALUES (1, N'Plain')
INSERT [dbo].[HexType] ([Id], [Name]) VALUES (2, N'Grass')
INSERT [dbo].[HexType] ([Id], [Name]) VALUES (3, N'Hill')
INSERT [dbo].[HexType] ([Id], [Name]) VALUES (4, N'Mountain')
/****** Object:  Table [dbo].[Users]    Script Date: 06/16/2009 15:37:55 ******/
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
/****** Object:  Table [dbo].[Race]    Script Date: 06/16/2009 15:37:55 ******/
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
INSERT [dbo].[Race] ([Id], [Name]) VALUES (N'136c9e98-00dd-48a7-88ae-fd465665a214', N'Default')
/****** Object:  Table [dbo].[Hex]    Script Date: 06/16/2009 15:37:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hex](
	[Id] [uniqueidentifier] NOT NULL,
	[WorldId] [uniqueidentifier] NOT NULL,
	[HexTypeId] [int] NOT NULL,
	[X] [int] NOT NULL,
	[Y] [int] NOT NULL,
 CONSTRAINT [PK_Hex] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Game]    Script Date: 06/16/2009 15:37:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Game](
	[Id] [uniqueidentifier] NOT NULL,
	[WorldId] [uniqueidentifier] NOT NULL,
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
/****** Object:  Table [dbo].[Nation]    Script Date: 06/16/2009 15:37:55 ******/
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
INSERT [dbo].[Nation] ([Id], [RaceId], [Name]) VALUES (N'c467cec5-f7cc-43ff-b14d-55848eefdeb9', N'136c9e98-00dd-48a7-88ae-fd465665a214', N'Default Nation')
/****** Object:  Table [dbo].[GameSession]    Script Date: 06/16/2009 15:37:55 ******/
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
/****** Object:  ForeignKey [FK_Hex_World]    Script Date: 06/16/2009 15:37:55 ******/
ALTER TABLE [dbo].[Hex]  WITH CHECK ADD  CONSTRAINT [FK_Hex_World] FOREIGN KEY([WorldId])
REFERENCES [dbo].[World] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Hex] CHECK CONSTRAINT [FK_Hex_World]
GO
/****** Object:  ForeignKey [FK_Game_World]    Script Date: 06/16/2009 15:37:55 ******/
ALTER TABLE [dbo].[Game]  WITH CHECK ADD  CONSTRAINT [FK_Game_World] FOREIGN KEY([WorldId])
REFERENCES [dbo].[World] ([Id])
GO
ALTER TABLE [dbo].[Game] CHECK CONSTRAINT [FK_Game_World]
GO
/****** Object:  ForeignKey [FK_Nation_Race]    Script Date: 06/16/2009 15:37:55 ******/
ALTER TABLE [dbo].[Nation]  WITH CHECK ADD  CONSTRAINT [FK_Nation_Race] FOREIGN KEY([RaceId])
REFERENCES [dbo].[Race] ([Id])
GO
ALTER TABLE [dbo].[Nation] CHECK CONSTRAINT [FK_Nation_Race]
GO
/****** Object:  ForeignKey [FK_GameSession_Game]    Script Date: 06/16/2009 15:37:55 ******/
ALTER TABLE [dbo].[GameSession]  WITH CHECK ADD  CONSTRAINT [FK_GameSession_Game] FOREIGN KEY([GameId])
REFERENCES [dbo].[Game] ([Id])
GO
ALTER TABLE [dbo].[GameSession] CHECK CONSTRAINT [FK_GameSession_Game]
GO
/****** Object:  ForeignKey [FK_GameSession_Nation]    Script Date: 06/16/2009 15:37:55 ******/
ALTER TABLE [dbo].[GameSession]  WITH CHECK ADD  CONSTRAINT [FK_GameSession_Nation] FOREIGN KEY([NationId])
REFERENCES [dbo].[Nation] ([Id])
GO
ALTER TABLE [dbo].[GameSession] CHECK CONSTRAINT [FK_GameSession_Nation]
GO
/****** Object:  ForeignKey [FK_GameSession_Users]    Script Date: 06/16/2009 15:37:55 ******/
ALTER TABLE [dbo].[GameSession]  WITH CHECK ADD  CONSTRAINT [FK_GameSession_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[GameSession] CHECK CONSTRAINT [FK_GameSession_Users]
GO
