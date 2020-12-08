USE [ATIS32]
GO
/****** Object:  Table [dbo].[AspnetUsers]    Script Date:  16.03.2012  10:32   ******/  
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AspnetUsers] (
            [ApplicationId] [uniqueidentifier]  NOT NULL,
            [UserId] [uniqueidentifier]  NOT NULL,
            [UserName] [nvarchar] (256) NOT NULL,
            [LoweredUserName] [nvarchar] (256) NOT NULL,
            [MobileAlias] [nvarchar] (16) NULL,
            [IsAnonymous] [bit] NOT NULL,
            [LastActivityDate] [datetime] NOT NULL,
            
CONSTRAINT [PRIMARY KEY NONCLUSTERD] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC 
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 
) ON [PRIMARY] 

GO

SET ANSI_PADDING OFF
GO

  
ALTER TABLE [dbo].[AspnetUsers] ADD  CONSTRAINT [DF_AspnetUsers_Valid]  DEFAULT ((0)) FOR [Valid]
GO

ALTER TABLE [dbo].[AspnetUsers]  WITH CHECK ADD  CONSTRAINT 
[FK__aspnet_Us__Appli__41EDCAC5] FOREIGN KEY ([ApplicationId])
REFERENCES [dbo].[AspnetApplications] ([ApplicationId])
GO
ALTER TABLE [dbo].[AspnetUsers] CHECK CONSTRAINT [FK__aspnet_Us__Appli__41EDCAC5]
GO

ALTER TABLE [dbo].[AspnetUsers]  ADD  CONSTRAINT 
[(newid())] DEFAULT UserId
GO

ALTER TABLE [dbo].[AspnetUsers]  ADD  CONSTRAINT 
[(NULL)] DEFAULT MobileAlias
GO
