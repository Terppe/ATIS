USE [ATIS34]
GO
/****** Object:  Table [dbo].[Tbl90References]    Script Date:  29.11.2018  10:32   ******/  
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Tbl90References] (
            [ReferenceID] [int] IDENTITY(1,1) NOT NULL,
            [FiSpeciesID] [int]  NULL,
            [PlSpeciesID] [int]  NULL,
            [GenusID] [int]  NULL,
            [InfratribusID] [int]  NULL,
            [SubtribusID] [int]  NULL,
            [TribusID] [int]  NULL,
            [SupertribusID] [int]  NULL,
            [InfrafamilyID] [int]  NULL,
            [SubfamilyID] [int]  NULL,
            [FamilyID] [int]  NULL,
            [SuperfamilyID] [int]  NULL,
            [InfraordoID] [int]  NULL,
            [SubordoID] [int]  NULL,
            [OrdoID] [int]  NULL,
            [LegioID] [int]  NULL,
            [InfraclassID] [int]  NULL,
            [SubclassID] [int]  NULL,
            [ClassID] [int]  NULL,
            [SuperclassID] [int]  NULL,
            [SubdivisionID] [int]  NULL,
            [SubphylumID] [int]  NULL,
            [DivisionID] [int]  NULL,
            [PhylumID] [int]  NULL,
            [RegnumID] [int]  NULL,
            [RefExpertID] [int]  NULL,
            [RefSourceID] [int]  NULL,
            [RefAuthorID] [int]  NULL,
            [CountID] [int]  NOT NULL,
            [Valid] [bit] NULL,
            [ValidYear] [nvarchar] (4) COLLATE Latin1_General_CI_AS NULL,
            [Info] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL,
            [Writer] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
            [WriterDate] [datetime] NOT NULL,
            [Updater] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
            [UpdaterDate] [datetime] NOT NULL,
            [Memo] [nvarchar] (MAX) COLLATE Latin1_General_CI_AS NULL,
            [RowVersion] [timestamp] NULL,
            
CONSTRAINT [PK_Tbl90References] PRIMARY KEY CLUSTERED 
(
	[ReferenceID] ASC 
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 , 
CONSTRAINT [IX_Tbl90References_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 
) ON [PRIMARY] 

GO

SET ANSI_PADDING OFF
GO

  
ALTER TABLE [dbo].[Tbl90References] ADD  CONSTRAINT [DF_Tbl90References_Valid]  DEFAULT ((0)) FOR [Valid]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl90RefExperts] FOREIGN KEY ([RefExpertID])
REFERENCES [dbo].[Tbl90RefExperts] ([RefExpertID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl90RefExperts]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl90RefSources] FOREIGN KEY ([RefSourceID])
REFERENCES [dbo].[Tbl90RefSources] ([RefSourceID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl90RefSources]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl90RefAuthors] FOREIGN KEY ([RefAuthorID])
REFERENCES [dbo].[Tbl90RefAuthors] ([RefAuthorID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl90RefAuthors]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl69FiSpeciesses] FOREIGN KEY ([FiSpeciesID])
REFERENCES [dbo].[Tbl69FiSpeciesses] ([FiSpeciesID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl69FiSpeciesses]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl72PlSpeciesses] FOREIGN KEY ([PlSpeciesID])
REFERENCES [dbo].[Tbl72PlSpeciesses] ([PlSpeciesID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl72PlSpeciesses]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl66Genusses] FOREIGN KEY ([GenusID])
REFERENCES [dbo].[Tbl66Genusses] ([GenusID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl66Genusses]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl63Infratribusses] FOREIGN KEY ([InfratribusID])
REFERENCES [dbo].[Tbl63Infratribusses] ([InfratribusID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl63Infratribusses]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl60Subtribusses] FOREIGN KEY ([SubtribusID])
REFERENCES [dbo].[Tbl60Subtribusses] ([SubtribusID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl60Subtribusses]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl57Tribusses] FOREIGN KEY ([TribusID])
REFERENCES [dbo].[Tbl57Tribusses] ([TribusID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl57Tribusses]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl54Supertribusses] FOREIGN KEY ([SupertribusID])
REFERENCES [dbo].[Tbl54Supertribusses] ([SupertribusID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl54Supertribusses]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl51Infrafamilies] FOREIGN KEY ([InfrafamilyID])
REFERENCES [dbo].[Tbl51Infrafamilies] ([InfrafamilyID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl51Infrafamilies]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl48Subfamilies] FOREIGN KEY ([SubfamilyID])
REFERENCES [dbo].[Tbl48Subfamilies] ([SubfamilyID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl48Subfamilies]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl45Families] FOREIGN KEY ([FamilyID])
REFERENCES [dbo].[Tbl45Families] ([FamilyID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl45Families]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl42Superfamilies] FOREIGN KEY ([SuperfamilyID])
REFERENCES [dbo].[Tbl42Superfamilies] ([SuperfamilyID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl42Superfamilies]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl39Infraordos] FOREIGN KEY ([InfraordoID])
REFERENCES [dbo].[Tbl39Infraordos] ([InfraordoID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl39Infraordos]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl36Subordos] FOREIGN KEY ([SubordoID])
REFERENCES [dbo].[Tbl36Subordos] ([SubordoID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl36Subordos]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl33Ordos] FOREIGN KEY ([OrdoID])
REFERENCES [dbo].[Tbl33Ordos] ([OrdoID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl33Ordos]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl30Legios] FOREIGN KEY ([LegioID])
REFERENCES [dbo].[Tbl30Legios] ([LegioID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl30Legios]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl27Infraclasses] FOREIGN KEY ([InfraclassID])
REFERENCES [dbo].[Tbl27Infraclasses] ([InfraclassID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl27Infraclasses]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl24Subclasses] FOREIGN KEY ([SubclassID])
REFERENCES [dbo].[Tbl24Subclasses] ([SubclassID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl24Subclasses]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl21Classes] FOREIGN KEY ([ClassID])
REFERENCES [dbo].[Tbl21Classes] ([ClassID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl21Classes]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl18Superclasses] FOREIGN KEY ([SuperclassID])
REFERENCES [dbo].[Tbl18Superclasses] ([SuperclassID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl18Superclasses]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl15Subdivisions] FOREIGN KEY ([SubdivisionID])
REFERENCES [dbo].[Tbl15Subdivisions] ([SubdivisionID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl15Subdivisions]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl12Subphylums] FOREIGN KEY ([SubphylumID])
REFERENCES [dbo].[Tbl12Subphylums] ([SubphylumID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl12Subphylums]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl09Divisions] FOREIGN KEY ([DivisionID])
REFERENCES [dbo].[Tbl09Divisions] ([DivisionID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl09Divisions]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl06Phylums] FOREIGN KEY ([PhylumID])
REFERENCES [dbo].[Tbl06Phylums] ([PhylumID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl06Phylums]
GO

ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT 
[FK_Tbl90References_Tbl03Regnums] FOREIGN KEY ([RegnumID])
REFERENCES [dbo].[Tbl03Regnums] ([RegnumID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl03Regnums]
GO
