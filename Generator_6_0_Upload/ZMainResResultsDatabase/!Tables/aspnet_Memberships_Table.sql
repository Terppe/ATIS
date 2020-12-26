USE [ATIS32]
GO
/****** Object:  Table [dbo].[aspnet_Memberships]    Script Date:  06.02.2012  10:32   ******/  
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[aspnet_Memberships] (
            [ApplicationId] [uniqueidentifier]  NOT NULL,
            [UserId] [uniqueidentifier]  NOT NULL,
            [Password] [nvarchar] (128) NOT NULL,
            [PasswordFormat] [int] NOT NULL,
            [PasswordSalt] [nvarchar] (128) NOT NULL,
            [MobilePIN] [nvarchar] (16) NULL,
            [Email] [nvarchar] (256) NULL,
            [LoweredEmail] [nvarchar] (256) NULL,
            [PasswordQuestion] [nvarchar] (256) NULL,
            [PasswordAnswer] [nvarchar] (128) NULL,
            [IsApproved] [bit] NOT NULL,
            [IsLockedOut] [bit] NOT NULL,
            [CreateDate] [datetime] NOT NULL,
            [LastLoginDate] [datetime] NOT NULL,
            [LastPasswordChangedDate] [datetime] NOT NULL,
            [LastLockoutDate] [datetime] NOT NULL,
            [FailedPasswordAttemptCount] [int] NOT NULL,
            [FailedPasswordAttemptWindowStart] [datetime] NOT NULL,
            [FailedPasswordAnswerAttemptCount] [int] NOT NULL,
            [FailedPasswordAnswerAttemptWindowStart] [datetime] NOT NULL,
            [Comment] [ntext] NULL,
            
CONSTRAINT [PRIMARY KEY NONCLUSTERD] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC 
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 
) ON [PRIMARY] 

GO

SET ANSI_PADDING OFF
GO

  
ALTER TABLE [dbo].[aspnet_Memberships] ADD  CONSTRAINT [DF_aspnet_Memberships_Valid]  DEFAULT ((0)) FOR [Valid]
GO

ALTER TABLE [dbo].[aspnet_Memberships]  WITH CHECK ADD  CONSTRAINT 
[FK__aspnet_Me__Appli__55F4C372] FOREIGN KEY ([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
ALTER TABLE [dbo].[aspnet_Memberships] CHECK CONSTRAINT [FK__aspnet_Me__Appli__55F4C372]
GO

ALTER TABLE [dbo].[aspnet_Memberships]  WITH CHECK ADD  CONSTRAINT 
[FK__aspnet_Me__UserI__56E8E7AB] FOREIGN KEY ([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[aspnet_Memberships] CHECK CONSTRAINT [FK__aspnet_Me__UserI__56E8E7AB]
GO

ALTER TABLE [dbo].[aspnet_Memberships]  ADD  CONSTRAINT 
[((0))] DEFAULT PasswordFormat
GO
