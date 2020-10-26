USE [ATIS32]
GO
/****** Object:  Table [dbo].[TblCountries]    Script Date:   29.11.2018 12:32     ******/  
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TblCountries] (
            [CountryID] [int] IDENTITY(1,1) NOT NULL,
            [Name] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL,
            [Regex] [nvarchar] (250) COLLATE Latin1_General_CI_AS NULL,
            
CONSTRAINT [PK_TblCountries] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC 
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 
) ON [PRIMARY] 

GO

SET ANSI_PADDING OFF
GO

