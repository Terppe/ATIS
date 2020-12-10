USE [ATIS34]
GO
/****** Object:  Table [dbo].[Tbl48Subfamilies]    Script Date:  10.12.2020  10:32   ******/  
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Tbl48Subfamilies] (
            [SubfamilyID] [int] IDENTITY(1,1) NOT NULL,
            [SubfamilyName] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL,
            [FamilyID] [int] NOT NULL,
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
            
CONSTRAINT [PK_Tbl48Subfamilies] PRIMARY KEY CLUSTERED 
(
	[SubfamilyID] ASC 
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_Tbl48Subfamilies_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_Tbl48Subfamilies_SubfamilyName] UNIQUE NONCLUSTERED 
(
	[SubfamilyName] ASC, [FamilyID] ASC, [Author] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 
) ON [PRIMARY] 

GO

SET ANSI_PADDING OFF
GO

  
ALTER TABLE [dbo].[Tbl48Subfamilies] ADD  CONSTRAINT [DF_Tbl48Subfamilies_Valid]  DEFAULT ((0)) FOR [Valid]
GO

ALTER TABLE [dbo].[Tbl48Subfamilies]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl48Subfamilies_Tbl45Families] FOREIGN KEY ([FamilyID])
REFERENCES [dbo].[Tbl45Families] ([FamilyID])
GO
ALTER TABLE [dbo].[Tbl48Subfamilies] CHECK CONSTRAINT [FK_Tbl48Subfamilies_Tbl45Families]
GO
