USE [ATIS34]
GO
/****** Object:  Table [dbo].[Tbl87Geographics]    Script Date:  22.01.2019  10:32   ******/  
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Tbl87Geographics] (
            [GeographicID] [int] IDENTITY(1,1) NOT NULL,
            [Address] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [Continent] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [Country] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [FiSpeciesID] [int]  NOT NULL,
            [PlSpeciesID] [int]  NOT NULL,
            [CountID] [int]  NOT NULL,
            [Latitude] [float]  NOT NULL,
            [Longitude] [float]  NOT NULL,
            [Latitude1] [float]  NOT NULL,
            [Longitude1] [float]  NOT NULL,
            [Latitude2] [float]  NOT NULL,
            [Longitude2] [float]  NOT NULL,
            [Latitude3] [float]  NOT NULL,
            [Longitude3] [float]  NOT NULL,
            [ZoomLevel] [float]  NOT NULL,
            [Valid] [bit] NULL,
            [ValidYear] [nvarchar] (4) COLLATE Latin1_General_CI_AS NULL,
            [Author] [nvarchar] (60) COLLATE Latin1_General_CI_AS NULL,
            [AuthorYear] [nvarchar] (4) COLLATE Latin1_General_CI_AS NULL,
            [Http] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [Info] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [Writer] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
            [WriterDate] [datetime] NOT NULL,
            [Updater] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
            [UpdaterDate] [datetime] NOT NULL,
            [Memo] [nvarchar] (MAX) COLLATE Latin1_General_CI_AS NULL,
            [RowVersion] [timestamp] NULL,
            
CONSTRAINT [PK_Tbl87Geographics] PRIMARY KEY CLUSTERED 
(
	[GeographicID] ASC 
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_Tbl87Geographics_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 
) ON [PRIMARY] 

GO

SET ANSI_PADDING OFF
GO

  
ALTER TABLE [dbo].[Tbl87Geographics] ADD  CONSTRAINT [DF_Tbl87Geographics_Valid]  DEFAULT ((0)) FOR [Valid]
GO

ALTER TABLE [dbo].[Tbl87Geographics]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl87Geographics_Tbl69FiSpeciesses] FOREIGN KEY ([FiSpeciesID])
REFERENCES [dbo].[Tbl69FiSpeciesses] ([FiSpeciesID])
GO
ALTER TABLE [dbo].[Tbl87Geographics] CHECK CONSTRAINT [FK_Tbl87Geographics_Tbl69FiSpeciesses]
GO

ALTER TABLE [dbo].[Tbl87Geographics]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl87Geographics_Tbl72PlSpeciesses] FOREIGN KEY ([PlSpeciesID])
REFERENCES [dbo].[Tbl72PlSpeciesses] ([PlSpeciesID])
GO
ALTER TABLE [dbo].[Tbl87Geographics] CHECK CONSTRAINT [FK_Tbl87Geographics_Tbl72PlSpeciesses]
GO
