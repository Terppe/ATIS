USE [ATIS34]
GO
/****** Object:  Table [dbo].[TblUserProfiles]    Script Date:   26.02.2019  10:32   ******/  
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TblUserProfiles] (
            [UserProfileID] [int] IDENTITY(1,1) NOT NULL,
            [Email] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL,
            [LastName] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [FirstName] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [Title] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [Password] [nvarchar] (250) COLLATE Latin1_General_CI_AS NOT NULL,
            [Role] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
            [Flag] [bit] NULL,
            [StartDate] [datetime] NULL,
            [EndDate] [datetime] NULL,
            [Notes] [nvarchar] (MAX) COLLATE Latin1_General_CI_AS NULL,
            [Colour] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [CountID] [int]  NOT NULL,
            [BirthDate] [datetime] NULL,
            [Gender] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL,
            [Country] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [Postcode] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL,
            [City] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [Street1] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [Street2] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [Tel] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [Mobil] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [Fax] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [HomePageURL] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [Business] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [Company] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [Writer] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
            [WriterDate] [datetime] NOT NULL,
            [Updater] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
            [UpdaterDate] [datetime] NOT NULL,
            [Memo] [nvarchar] (MAX) COLLATE Latin1_General_CI_AS NULL,
            [Filestream] [varbinary] (MAX) NULL,
            [ImageMimeType] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL,
            [FilestreamID] [uniqueidentifier]  NULL,
            [Signature] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL,
            [MailNewsletter] [bit] NULL,
            [MaulHTML] [bit] NULL,
            [Known] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [RowVersion] [timestamp] NULL,
            
CONSTRAINT [PK_UserProfiles] PRIMARY KEY CLUSTERED 
(
	[UserProfileID] ASC 
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_UserProfiles_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_UserProfiles_Name] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 
) ON [PRIMARY] 

GO

SET ANSI_PADDING OFF
GO

