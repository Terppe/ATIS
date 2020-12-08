USE [ATIS34]
GO
/****** Object:  Table [dbo].[Tbl18Superclasses]    Script Date:  04.11.2020  12:32   ******/  
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Tbl18Superclasses] (
            [SuperclassID] [int] IDENTITY(1,1) NOT NULL,
            [SuperclassName] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL,
            [SubphylumID] [int] NOT NULL,
            [SubdivisionID] [int] NOT NULL,
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
            
CONSTRAINT [PK_Tbl18Superclasses] PRIMARY KEY CLUSTERED 
(
	[SuperclassID] ASC 
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_Tbl18Superclasses_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_Tbl18Superclasses_SuperclassName] UNIQUE NONCLUSTERED 
(
	[SuperclassName] ASC, [SubdivisionID] ASC, [SubphylumID] ASC, [Author] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 
) ON [PRIMARY] 

GO

SET ANSI_PADDING OFF
GO

  
ALTER TABLE [dbo].[Tbl18Superclasses] ADD  CONSTRAINT [DF_Tbl18Superclasses_Valid]  DEFAULT ((0)) FOR [Valid]
GO

ALTER TABLE [dbo].[Tbl18Superclasses]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl18Superclasses_Tbl12Subphylums] FOREIGN KEY ([SubphylumID])
REFERENCES [dbo].[Tbl12Subphylums] ([SubphylumID])
GO
ALTER TABLE [dbo].[Tbl18Superclasses] CHECK CONSTRAINT [FK_Tbl18Superclasses_Tbl12Subphylums]
GO

ALTER TABLE [dbo].[Tbl18Superclasses]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl18Superclasses_Tbl15Subdivisions] FOREIGN KEY ([SubdivisionID])
REFERENCES [dbo].[Tbl15Subdivisions] ([SubdivisionID])
GO
ALTER TABLE [dbo].[Tbl18Superclasses] CHECK CONSTRAINT [FK_Tbl18Superclasses_Tbl15Subdivisions]
GO
