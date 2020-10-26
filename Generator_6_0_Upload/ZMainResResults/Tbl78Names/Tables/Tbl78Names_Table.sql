USE [ATIS34]
GO
/****** Object:  Table [dbo].[Tbl78Names]    Script Date:  22.01.2019  10:32   ******/  
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Tbl78Names] (
            [NameID] [int] IDENTITY(1,1) NOT NULL,
            [NameName] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL,
            [FiSpeciesID] [int]  NOT NULL,
            [PlSpeciesID] [int]  NOT NULL,
            [CountID] [int]  NOT NULL,
            [Valid] [bit] NULL,
            [ValidYear] [nvarchar] (4) COLLATE Latin1_General_CI_AS NULL,
            [Language] [nvarchar] (3) COLLATE Latin1_General_CI_AS NULL,
            [Info] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [Writer] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
            [WriterDate] [datetime] NOT NULL,
            [Updater] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
            [UpdaterDate] [datetime] NOT NULL,
            [Memo] [nvarchar] (MAX) COLLATE Latin1_General_CI_AS NULL,
            [RowVersion] [timestamp] NULL,
            
CONSTRAINT [PK_Tbl78Names] PRIMARY KEY CLUSTERED 
(
	[NameID] ASC 
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_Tbl78Names_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_Tbl78Names_NameName_FKID] UNIQUE NONCLUSTERED 
(
	[NameName] ASC, [FiSpeciesID] ASC, [PlSpeciesID] ASC, [Language] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 
) ON [PRIMARY] 

GO

SET ANSI_PADDING OFF
GO

  
ALTER TABLE [dbo].[Tbl78Names] ADD  CONSTRAINT [DF_Tbl78Names_Valid]  DEFAULT ((0)) FOR [Valid]
GO

ALTER TABLE [dbo].[Tbl78Names]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl78Names_Tbl69FiSpeciesses] FOREIGN KEY ([FiSpeciesID])
REFERENCES [dbo].[Tbl69FiSpeciesses] ([FiSpeciesID])
GO
ALTER TABLE [dbo].[Tbl78Names] CHECK CONSTRAINT [FK_Tbl78Names_Tbl69FiSpeciesses]
GO

ALTER TABLE [dbo].[Tbl78Names]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl78Names_Tbl72PlSpeciesses] FOREIGN KEY ([PlSpeciesID])
REFERENCES [dbo].[Tbl72PlSpeciesses] ([PlSpeciesID])
GO
ALTER TABLE [dbo].[Tbl78Names] CHECK CONSTRAINT [FK_Tbl78Names_Tbl72PlSpeciesses]
GO
