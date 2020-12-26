USE [ATIS34]
GO
/****** Object:  Table [dbo].[Tbl03Regnums]    Script Date:  09.11.2017  12:32     ******/  
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Tbl03Regnums] (
            [RegnumID] [int] IDENTITY(1,1) NOT NULL,
            [RegnumName] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL,
            [Subregnum] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
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
            
CONSTRAINT [PK_Tbl03Regnums] PRIMARY KEY CLUSTERED 
(
	[RegnumID] ASC 
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_Tbl03Regnums_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_Tbl03Regnums_RegnumName] UNIQUE NONCLUSTERED 
(
	[RegnumName] ASC, [Subregnum] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 
) ON [PRIMARY] 

GO

SET ANSI_PADDING OFF
GO

  
ALTER TABLE [dbo].[Tbl03Regnums] ADD  CONSTRAINT [DF_Tbl03Regnums_Valid]  DEFAULT ((0)) FOR [Valid]
GO
