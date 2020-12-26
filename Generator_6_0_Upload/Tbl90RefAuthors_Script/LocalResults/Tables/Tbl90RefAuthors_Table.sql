USE [ATIS34]
GO
/****** Object:  Table [dbo].[Tbl90RefAuthors]    Script Date:  30.03.2019  10:32   ******/  
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Tbl90RefAuthors] (
            [RefAuthorID] [int] IDENTITY(1,1) NOT NULL,
            [CountID] [int]  NOT NULL,
            [Valid] [bit] NULL,
            [ValidYear] [nvarchar] (4) COLLATE Latin1_General_CI_AS NULL,
            [RefAuthorName] [nvarchar] (200) COLLATE Latin1_General_CI_AS NULL,
            [PublicationYear] [nvarchar] (4) COLLATE Latin1_General_CI_AS NULL,
            [ArticelTitle] [nvarchar] (300) COLLATE Latin1_General_CI_AS NULL,
            [BookName] [nvarchar] (200) COLLATE Latin1_General_CI_AS NULL,
            [Page] [nvarchar] (60) COLLATE Latin1_General_CI_AS NULL,
            [Page1] [nvarchar] (60) COLLATE Latin1_General_CI_AS NULL,
            [Publisher] [nvarchar] (200) COLLATE Latin1_General_CI_AS NULL,
            [PublicationPlace] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [ISBN] [nvarchar] (60) COLLATE Latin1_General_CI_AS NULL,
            [Notes] [nvarchar] (MAX) COLLATE Latin1_General_CI_AS NULL,
            [Info] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [Writer] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
            [WriterDate] [datetime] NOT NULL,
            [Updater] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
            [UpdaterDate] [datetime] NOT NULL,
            [Memo] [nvarchar] (MAX) COLLATE Latin1_General_CI_AS NULL,
            [RowVersion] [timestamp] NULL,
            
CONSTRAINT [PK_Tbl90RefAuthors] PRIMARY KEY CLUSTERED 
(
	[RefAuthorID] ASC 
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_Tbl90RefAuthors_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 
) ON [PRIMARY] 

GO

SET ANSI_PADDING OFF
GO

  
ALTER TABLE [dbo].[Tbl90RefAuthors] ADD  CONSTRAINT [DF_Tbl90RefAuthors_Valid]  DEFAULT ((0)) FOR [Valid]
GO
