USE [ATIS34]
GO
/****** Object:  Table [dbo].[Tbl12Subphylums]    Script Date:  13.06.2018  12:32   ******/  
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Tbl12Subphylums] (
            [SubphylumID] [int] IDENTITY(1,1) NOT NULL,
            [SubphylumName] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL,
            [PhylumID] [int] NOT NULL,
            [CountID] [int]  NOT NULL,
            [Valid] [bit] NULL,
            [ValidYear] [nvarchar] (4) COLLATE Latin1_General_CI_AS NULL,
            [Synonym] [nvarchar] (MAX) COLLATE Latin1_General_CI_AS NULL,
            [Author] [nvarchar] (60) COLLATE Latin1_General_CI_AS NULL,
            [AuthorYear] [nvarchar] (4) COLLATE Latin1_General_CI_AS NULL,
            [Info] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [EngName] [nvarchar] (200) COLLATE Latin1_General_CI_AS NULL,
            [GerName] [nvarchar] (200) COLLATE Latin1_General_CI_AS NULL,
            [FraName] [nvarchar] (200) COLLATE Latin1_General_CI_AS NULL,
            [PorName] [nvarchar] (200) COLLATE Latin1_General_CI_AS NULL,
            [Writer] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
            [WriterDate] [datetime] NOT NULL,
            [Updater] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
            [UpdaterDate] [datetime] NOT NULL,
            [Memo] [nvarchar] (MAX) COLLATE Latin1_General_CI_AS NULL,
            [RowVersion] [timestamp] NULL,
            
CONSTRAINT [PK_Tbl12Subphylums] PRIMARY KEY CLUSTERED 
(
	[SubphylumID] ASC 
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_Tbl12Subphylums_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_Tbl12Subphylums_SubphylumName] UNIQUE NONCLUSTERED 
(
	[SubphylumName] ASC, [PhylumID] ASC, [Author] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 
) ON [PRIMARY] 

GO

SET ANSI_PADDING OFF
GO

  
ALTER TABLE [dbo].[Tbl12Subphylums] ADD  CONSTRAINT [DF_Tbl12Subphylums_Valid]  DEFAULT ((0)) FOR [Valid]
GO

ALTER TABLE [dbo].[Tbl12Subphylums]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl12Subphylums_Tbl06Phylums] FOREIGN KEY ([PhylumID])
REFERENCES [dbo].[Tbl06Phylums] ([PhylumID])
GO
ALTER TABLE [dbo].[Tbl12Subphylums] CHECK CONSTRAINT [FK_Tbl12Subphylums_Tbl06Phylums]
GO
