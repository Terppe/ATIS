USE [ATIS34]
GO
/****** Object:  Table [dbo].[Tbl84Synonyms]    Script Date:  13.11.2018  10:32   ******/  
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Tbl84Synonyms] (
            [SynonymID] [int] IDENTITY(1,1) NOT NULL,
            [SynonymName] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL,
            [FiSpeciesID] [int]  NOT NULL,
            [PlSpeciesID] [int]  NOT NULL,
            [CountID] [int]  NOT NULL,
            [Valid] [bit] NULL,
            [ValidYear] [nvarchar] (4) COLLATE Latin1_General_CI_AS NULL,
            [Author] [nvarchar] (60) COLLATE Latin1_General_CI_AS NULL,
            [AuthorYear] [nvarchar] (4) COLLATE Latin1_General_CI_AS NULL,
            [Info] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [Writer] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
            [WriterDate] [datetime] NOT NULL,
            [Updater] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
            [UpdaterDate] [datetime] NOT NULL,
            [Memo] [nvarchar] (MAX) COLLATE Latin1_General_CI_AS NULL,
            [RowVersion] [timestamp] NULL,
            
CONSTRAINT [PK_Tbl84Synonyms] PRIMARY KEY CLUSTERED 
(
	[SynonymID] ASC 
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_Tbl84Synonyms_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_Tbl84Synonyms_SynonymName_FKID] UNIQUE NONCLUSTERED 
(
	[SynonymName] ASC, [Author] ASC, [FiSpeciesID] ASC, [PlSpeciesID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 
) ON [PRIMARY] 

GO

SET ANSI_PADDING OFF
GO

  
ALTER TABLE [dbo].[Tbl84Synonyms] ADD  CONSTRAINT [DF_Tbl84Synonyms_Valid]  DEFAULT ((0)) FOR [Valid]
GO

ALTER TABLE [dbo].[Tbl84Synonyms]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl84Synonyms_Tbl69FiSpeciesses] FOREIGN KEY ([FiSpeciesID])
REFERENCES [dbo].[Tbl69FiSpeciesses] ([FiSpeciesID])
GO
ALTER TABLE [dbo].[Tbl84Synonyms] CHECK CONSTRAINT [FK_Tbl84Synonyms_Tbl69FiSpeciesses]
GO

ALTER TABLE [dbo].[Tbl84Synonyms]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl84Synonyms_Tbl72PlSpeciesses] FOREIGN KEY ([PlSpeciesID])
REFERENCES [dbo].[Tbl72PlSpeciesses] ([PlSpeciesID])
GO
ALTER TABLE [dbo].[Tbl84Synonyms] CHECK CONSTRAINT [FK_Tbl84Synonyms_Tbl72PlSpeciesses]
GO
