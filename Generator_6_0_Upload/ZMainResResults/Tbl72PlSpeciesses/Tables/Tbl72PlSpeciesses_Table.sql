USE [ATIS34]
GO
/****** Object:  Table [dbo].[Tbl72PlSpeciesses]    Script Date:  13.12.2019  12:32   ******/  
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Tbl72PlSpeciesses] (
            [PlSpeciesID] [int] IDENTITY(1,1) NOT NULL,
            [PlSpeciesName] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL,
            [Subspecies] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [Divers] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [GenusID] [int] NOT NULL,
            [SpeciesgroupID] [int] NULL,
            [CountID] [int]  NOT NULL,
            [Valid] [bit] NULL,
            [ValidYear] [nvarchar] (4) COLLATE Latin1_General_CI_AS NULL,
            [MemoSpecies] [nvarchar] (MAX) COLLATE Latin1_General_CI_AS NULL,
            [TradeName] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [Author] [nvarchar] (60) COLLATE Latin1_General_CI_AS NULL,
            [AuthorYear] [nvarchar] (4) COLLATE Latin1_General_CI_AS NULL,
            [Importer] [nvarchar] (60) COLLATE Latin1_General_CI_AS NULL,
            [ImportingYear] [nvarchar] (4) COLLATE Latin1_General_CI_AS NULL,
            [BasinHeight] [int] NULL,
            [PlantLength] [decimal](3,1) NULL,
            [Difficult1] [bit] NULL,
            [Difficult2] [bit] NULL,
            [Difficult3] [bit] NULL,
            [Difficult4] [bit] NULL,
            [MemoTech] [nvarchar] (MAX) COLLATE Latin1_General_CI_AS NULL,
            [Ph1] [decimal](2,1) NULL,
            [Ph2] [decimal](2,1) NULL,
            [Temp1] [int] NULL,
            [Temp2] [int] NULL,
            [Hardness1] [int] NULL,
            [Hardness2] [int] NULL,
            [CarboHardness1] [int] NULL,
            [CarboHardness2] [int] NULL,
            [Writer] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
            [WriterDate] [datetime] NOT NULL,
            [Updater] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
            [UpdaterDate] [datetime] NOT NULL,
            [MemoBuilt] [nvarchar] (MAX) NULL,
            [MemoColor] [nvarchar] (MAX) NULL,
            [MemoReproduction] [nvarchar] (MAX) NULL,
            [MemoCulture] [nvarchar] (MAX) NULL,
            [MemoGlobal] [nvarchar] (MAX) NULL,
            [RowVersion] [timestamp] NULL,
            
CONSTRAINT [PK_Tbl72PlSpeciesses] PRIMARY KEY CLUSTERED 
(
	[PlSpeciesID] ASC 
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_Tbl72PlSpeciesses_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_Tbl72PlSpeciesses_PlSpeciesName] UNIQUE NONCLUSTERED 
(
	[GenusID] ASC, [PlSpeciesName] ASC, [Subspecies] ASC, [Divers] ASC, [Author] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 
) ON [PRIMARY] 

GO

SET ANSI_PADDING OFF
GO

  
ALTER TABLE [dbo].[Tbl72PlSpeciesses] ADD  CONSTRAINT [DF_Tbl72PlSpeciesses_Valid]  DEFAULT ((0)) FOR [Valid]
GO

ALTER TABLE [dbo].[Tbl72PlSpeciesses]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl72PlSpeciesses_Tbl66Genusses] FOREIGN KEY ([GenusID])
REFERENCES [dbo].[Tbl66Genusses] ([GenusID])
GO
ALTER TABLE [dbo].[Tbl72PlSpeciesses] CHECK CONSTRAINT [FK_Tbl72PlSpeciesses_Tbl66Genusses]
GO

ALTER TABLE [dbo].[Tbl72PlSpeciesses]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl72PlSpeciesses_Tbl68Speciesgroups] FOREIGN KEY ([SpeciesgroupID])
REFERENCES [dbo].[Tbl68Speciesgroups] ([SpeciesgroupID])
GO
ALTER TABLE [dbo].[Tbl72PlSpeciesses] CHECK CONSTRAINT [FK_Tbl72PlSpeciesses_Tbl68Speciesgroups]
GO
