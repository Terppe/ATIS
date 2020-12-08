USE [ATIS32]
GO
/****** Object:  Table [dbo].[AspnetApplications]    Script Date:  16.03.2012  10:32   ******/  
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AspnetApplications] (
            [ApplicationName] [nvarchar] (256) NOT NULL,
            [LoweredApplicationName] [nvarchar] (256) NOT NULL,
            [ApplicationId] [uniqueidentifier]  NOT NULL,
            [Description] [nvarchar] (256) NULL,
            
CONSTRAINT [PRIMARY KEY NONCLUSTERD] PRIMARY KEY CLUSTERED 
(
	[ApplicationId] ASC 
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_aspnet_Applications_LoweredApplicationName] UNIQUE NONCLUSTERED 
(
	[LoweredApplicationName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_aspnet_Applications_ApplicationName] UNIQUE NONCLUSTERED 
(
	[ApplicationName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 
) ON [PRIMARY] 

GO

SET ANSI_PADDING OFF
GO

  
ALTER TABLE [dbo].[AspnetApplications] ADD  CONSTRAINT [DF_AspnetApplications_Valid]  DEFAULT ((0)) FOR [Valid]
GO

ALTER TABLE [dbo].[AspnetApplications]  ADD  CONSTRAINT 
[(newid())] DEFAULT ApplicationId
GO
