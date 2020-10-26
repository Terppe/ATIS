USE [ATIS32]
GO
/****** Object:  Table [dbo].[TblCounters]    Script Date:  3.1.2012  12:32     ******/  
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TblCounters] (
            [CounterID] [int] IDENTITY(1,1) NOT NULL,
            [Zahl] [int]  NOT NULL,
            [CounterName] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL,
            
CONSTRAINT [PK_TblCounters] PRIMARY KEY CLUSTERED 
(
	[CounterID] ASC 
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 
) ON [PRIMARY] 

GO

SET ANSI_PADDING OFF
GO

