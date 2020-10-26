USE [ATIS34]
GO
/****** Object:  Table [dbo].[Tbl15Subdivisions]    Script Date:  12.12.2019  12:32   ******/  
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Tbl15Subdivisions] (
            [SubdivisionID] [int] IDENTITY(1,1) NOT NULL,
            [SubdivisionName] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL,
            [DivisionID] [int] NOT NULL,
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
            
CONSTRAINT [PK_Tbl15Subdivisions] PRIMARY KEY CLUSTERED 
(
	[SubdivisionID] ASC 
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_Tbl15Subdivisions_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_Tbl15Subdivisions_SubdivisionName] UNIQUE NONCLUSTERED 
(
	[SubdivisionName] ASC, [DivisionID] ASC, [Author] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 
) ON [PRIMARY] 

GO

SET ANSI_PADDING OFF
GO

  
ALTER TABLE [dbo].[Tbl15Subdivisions] ADD  CONSTRAINT [DF_Tbl15Subdivisions_Valid]  DEFAULT ((0)) FOR [Valid]
GO

ALTER TABLE [dbo].[Tbl15Subdivisions]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl15Subdivisions_Tbl09Divisions] FOREIGN KEY ([DivisionID])
REFERENCES [dbo].[Tbl09Divisions] ([DivisionID])
GO
ALTER TABLE [dbo].[Tbl15Subdivisions] CHECK CONSTRAINT [FK_Tbl15Subdivisions_Tbl09Divisions]
GO
