USE [ATIS34]
GO
/****** Object:  Table [dbo].[Tbl69FiSpeciesses]    Script Date:  17.12.2020  10:32   ******/  
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Tbl69FiSpeciesses] (
            [FiSpeciesID] [int] IDENTITY(1,1) NOT NULL,
            [FiSpeciesName] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL,
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
            [TypeSpecies] [bit] NULL,
            [LNumber] [nvarchar] (10) COLLATE Latin1_General_CI_AS NULL,
            [LOrigin] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL,
            [LDANumber] [nvarchar] (10) COLLATE Latin1_General_CI_AS NULL,
            [LDAOrigin] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL,
            [BasinLength] [int] NULL,
            [FishLength] [decimal](4,1) NULL,
            [Karnivore] [bit] NULL,
            [Herbivore] [bit] NULL,
            [Limnivore] [bit] NULL,
            [Omnivore] [bit] NULL,
            [MemoFoods] [nvarchar] (MAX) COLLATE Latin1_General_CI_AS NULL,
            [Difficult1] [bit] NULL,
            [Difficult2] [bit] NULL,
            [Difficult3] [bit] NULL,
            [Difficult4] [bit] NULL,
            [RegionTop] [bit] NULL,
            [RegionMiddle] [bit] NULL,
            [RegionBottom] [bit] NULL,
            [MemoRegion] [nvarchar] (MAX) COLLATE Latin1_General_CI_AS NULL,
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
            [MemoHusbandry] [nvarchar] (MAX) COLLATE Latin1_General_CI_AS NULL,
            [MemoBreeding] [nvarchar] (MAX) COLLATE Latin1_General_CI_AS NULL,
            [MemoBuilt] [nvarchar] (MAX) COLLATE Latin1_General_CI_AS NULL,
            [MemoColor] [nvarchar] (MAX) COLLATE Latin1_General_CI_AS NULL,
            [MemoSozial] [nvarchar] (MAX) COLLATE Latin1_General_CI_AS NULL,
            [MemoDomorphism] [nvarchar] (MAX) COLLATE Latin1_General_CI_AS NULL,
            [MemoSpecial] [nvarchar] (MAX) COLLATE Latin1_General_CI_AS NULL,
            [RowVersion] [timestamp] NULL,
            
CONSTRAINT [PK_Tbl69FiSpeciesses] PRIMARY KEY CLUSTERED 
(
	[FiSpeciesID] ASC 
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_Tbl69FiSpeciesses_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_Tbl69FiSpeciesses_FiSpeciesName] UNIQUE NONCLUSTERED 
(
	[GenusID] ASC, [FiSpeciesName] ASC, [Subspecies] ASC, [Divers] ASC, [Author] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 
) ON [PRIMARY] 

GO

SET ANSI_PADDING OFF
GO

  
ALTER TABLE [dbo].[Tbl69FiSpeciesses] ADD  CONSTRAINT [DF_Tbl69FiSpeciesses_Valid]  DEFAULT ((0)) FOR [Valid]
GO

ALTER TABLE [dbo].[Tbl69FiSpeciesses]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl69FiSpeciesses_Tbl66Genusses] FOREIGN KEY ([GenusID])
REFERENCES [dbo].[Tbl66Genusses] ([GenusID])
GO
ALTER TABLE [dbo].[Tbl69FiSpeciesses] CHECK CONSTRAINT [FK_Tbl69FiSpeciesses_Tbl66Genusses]
GO

ALTER TABLE [dbo].[Tbl69FiSpeciesses]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl69FiSpeciesses_Tbl68Speciesgroups] FOREIGN KEY ([SpeciesgroupID])
REFERENCES [dbo].[Tbl68Speciesgroups] ([SpeciesgroupID])
GO
ALTER TABLE [dbo].[Tbl69FiSpeciesses] CHECK CONSTRAINT [FK_Tbl69FiSpeciesses_Tbl68Speciesgroups]
GO
