USE [ATIS34]
GO
/****** Object:  Table [dbo].[Tbl81Images]    Script Date:  22.01.2019  10:32   ******/  
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Tbl81Images] (
            [ImageID] [int] IDENTITY(1,1) NOT NULL,
            [FiSpeciesID] [int]  NOT NULL,
            [PlSpeciesID] [int]  NOT NULL,
            [CountID] [int]  NOT NULL,
            [Valid] [bit] NULL,
            [ValidYear] [nvarchar] (4) COLLATE Latin1_General_CI_AS NULL,
            [ShotDate] [date] NULL,
            [Info] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [Writer] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
            [WriterDate] [datetime] NOT NULL,
            [Updater] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
            [UpdaterDate] [datetime] NOT NULL,
            [Memo] [nvarchar] (MAX) COLLATE Latin1_General_CI_AS NULL,
            [ImageData] [varbinary] (MAX) NULL,
            [ImageMimeType] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL,
            [Filestream] [varbinary] (MAX) FILESTREAM,
            [FilestreamID] [UNIQUEIDENTIFIER]  ROWGUIDCOL CONSTRAINT uk_medien_stream UNIQUE NOT NULL,
            [RowVersion] [timestamp] NULL,
            
CONSTRAINT [PK_Tbl81Images] PRIMARY KEY CLUSTERED 
(
	[ImageID] ASC 
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_Tbl81Images_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 
) ON [PRIMARY] 

GO

SET ANSI_PADDING OFF
GO

  
ALTER TABLE [dbo].[Tbl81Images] ADD  CONSTRAINT [DF_Tbl81Images_Valid]  DEFAULT ((0)) FOR [Valid]
GO

ALTER TABLE [dbo].[Tbl81Images]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl81Images_Tbl69FiSpeciesses] FOREIGN KEY ([FiSpeciesID])
REFERENCES [dbo].[Tbl69FiSpeciesses] ([FiSpeciesID])
GO
ALTER TABLE [dbo].[Tbl81Images] CHECK CONSTRAINT [FK_Tbl81Images_Tbl69FiSpeciesses]
GO

ALTER TABLE [dbo].[Tbl81Images]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl81Images_Tbl72PlSpeciesses] FOREIGN KEY ([PlSpeciesID])
REFERENCES [dbo].[Tbl72PlSpeciesses] ([PlSpeciesID])
GO
ALTER TABLE [dbo].[Tbl81Images] CHECK CONSTRAINT [FK_Tbl81Images_Tbl72PlSpeciesses]
GO
