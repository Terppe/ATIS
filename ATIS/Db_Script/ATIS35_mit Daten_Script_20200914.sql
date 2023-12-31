USE [master]
GO
/****** Object:  Database [ATIS35]    Script Date: 14.09.2020 17:12:59 ******/
CREATE DATABASE [ATIS35]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ATIS35', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ATIS35.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB ), 
 FILEGROUP [VIDEOFILES] CONTAINS FILESTREAM  DEFAULT
( NAME = N'Video_Stream', FILENAME = N'D:\DB_FS_ATIS35\Video_Stream' , MAXSIZE = UNLIMITED)
 LOG ON 
( NAME = N'ATIS35_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ATIS35_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ATIS35] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ATIS35].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ATIS35] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ATIS35] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ATIS35] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ATIS35] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ATIS35] SET ARITHABORT OFF 
GO
ALTER DATABASE [ATIS35] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ATIS35] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ATIS35] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ATIS35] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ATIS35] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ATIS35] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ATIS35] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ATIS35] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ATIS35] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ATIS35] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ATIS35] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ATIS35] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ATIS35] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ATIS35] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ATIS35] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ATIS35] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ATIS35] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ATIS35] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ATIS35] SET  MULTI_USER 
GO
ALTER DATABASE [ATIS35] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ATIS35] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ATIS35] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ATIS35] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ATIS35] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ATIS35] SET QUERY_STORE = OFF
GO
USE [ATIS35]
GO
/****** Object:  Table [dbo].[Tbl03Regnums]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl03Regnums](
	[RegnumID] [int] IDENTITY(1,1) NOT NULL,
	[RegnumName] [nvarchar](100) NOT NULL,
	[Subregnum] [nvarchar](100) NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Synonym] [nvarchar](max) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[EngName] [nvarchar](200) NULL,
	[GerName] [nvarchar](200) NULL,
	[FraName] [nvarchar](200) NULL,
	[PorName] [nvarchar](200) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl03Regnums] PRIMARY KEY CLUSTERED 
(
	[RegnumID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl06Phylums]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl06Phylums](
	[PhylumID] [int] IDENTITY(1,1) NOT NULL,
	[PhylumName] [nvarchar](100) NOT NULL,
	[RegnumID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Synonym] [nvarchar](max) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[EngName] [nvarchar](200) NULL,
	[GerName] [nvarchar](200) NULL,
	[FraName] [nvarchar](200) NULL,
	[PorName] [nvarchar](200) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl06Phylums] PRIMARY KEY CLUSTERED 
(
	[PhylumID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl09Divisions]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl09Divisions](
	[DivisionID] [int] IDENTITY(1,1) NOT NULL,
	[DivisionName] [nvarchar](100) NOT NULL,
	[RegnumID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Synonym] [nvarchar](max) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[EngName] [nvarchar](200) NULL,
	[GerName] [nvarchar](200) NULL,
	[FraName] [nvarchar](200) NULL,
	[PorName] [nvarchar](200) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl09Divisions] PRIMARY KEY CLUSTERED 
(
	[DivisionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl12Subphylums]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl12Subphylums](
	[SubphylumID] [int] IDENTITY(1,1) NOT NULL,
	[SubphylumName] [nvarchar](100) NOT NULL,
	[PhylumID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Synonym] [nvarchar](max) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[EngName] [nvarchar](200) NULL,
	[GerName] [nvarchar](200) NULL,
	[FraName] [nvarchar](200) NULL,
	[PorName] [nvarchar](200) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl12Subphylums] PRIMARY KEY CLUSTERED 
(
	[SubphylumID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl15Subdivisions]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl15Subdivisions](
	[SubdivisionID] [int] IDENTITY(1,1) NOT NULL,
	[SubdivisionName] [nvarchar](100) NOT NULL,
	[DivisionID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Synonym] [nvarchar](max) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[EngName] [nvarchar](200) NULL,
	[GerName] [nvarchar](200) NULL,
	[FraName] [nvarchar](200) NULL,
	[PorName] [nvarchar](200) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl15Subdivisions] PRIMARY KEY CLUSTERED 
(
	[SubdivisionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl18Superclasses]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl18Superclasses](
	[SuperclassID] [int] IDENTITY(1,1) NOT NULL,
	[SuperclassName] [nvarchar](100) NOT NULL,
	[SubphylumID] [int] NOT NULL,
	[SubdivisionID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Synonym] [nvarchar](max) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[EngName] [nvarchar](200) NULL,
	[GerName] [nvarchar](200) NULL,
	[FraName] [nvarchar](200) NULL,
	[PorName] [nvarchar](200) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl18Superclasses] PRIMARY KEY CLUSTERED 
(
	[SuperclassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl21Classes]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl21Classes](
	[ClassID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](100) NOT NULL,
	[SuperclassID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Synonym] [nvarchar](max) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[EngName] [nvarchar](200) NULL,
	[GerName] [nvarchar](200) NULL,
	[FraName] [nvarchar](200) NULL,
	[PorName] [nvarchar](200) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl21Classes] PRIMARY KEY CLUSTERED 
(
	[ClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl24Subclasses]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl24Subclasses](
	[SubclassID] [int] IDENTITY(1,1) NOT NULL,
	[SubclassName] [nvarchar](100) NOT NULL,
	[ClassID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Synonym] [nvarchar](max) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[EngName] [nvarchar](200) NULL,
	[GerName] [nvarchar](200) NULL,
	[FraName] [nvarchar](200) NULL,
	[PorName] [nvarchar](200) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl24Subclasses] PRIMARY KEY CLUSTERED 
(
	[SubclassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl27Infraclasses]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl27Infraclasses](
	[InfraclassID] [int] IDENTITY(1,1) NOT NULL,
	[InfraclassName] [nvarchar](100) NOT NULL,
	[SubclassID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Synonym] [nvarchar](max) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[EngName] [nvarchar](200) NULL,
	[GerName] [nvarchar](200) NULL,
	[FraName] [nvarchar](200) NULL,
	[PorName] [nvarchar](200) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl27Infraclasses] PRIMARY KEY CLUSTERED 
(
	[InfraclassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl30Legios]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl30Legios](
	[LegioID] [int] IDENTITY(1,1) NOT NULL,
	[LegioName] [nvarchar](100) NOT NULL,
	[InfraclassID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Synonym] [nvarchar](max) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[EngName] [nvarchar](200) NULL,
	[GerName] [nvarchar](200) NULL,
	[FraName] [nvarchar](200) NULL,
	[PorName] [nvarchar](200) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl30Legios] PRIMARY KEY CLUSTERED 
(
	[LegioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl33Ordos]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl33Ordos](
	[OrdoID] [int] IDENTITY(1,1) NOT NULL,
	[OrdoName] [nvarchar](100) NOT NULL,
	[LegioID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Synonym] [nvarchar](max) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[EngName] [nvarchar](200) NULL,
	[GerName] [nvarchar](200) NULL,
	[FraName] [nvarchar](200) NULL,
	[PorName] [nvarchar](200) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl33Ordos] PRIMARY KEY CLUSTERED 
(
	[OrdoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl36Subordos]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl36Subordos](
	[SubordoID] [int] IDENTITY(1,1) NOT NULL,
	[SubordoName] [nvarchar](100) NOT NULL,
	[OrdoID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Synonym] [nvarchar](max) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[EngName] [nvarchar](200) NULL,
	[GerName] [nvarchar](200) NULL,
	[FraName] [nvarchar](200) NULL,
	[PorName] [nvarchar](200) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl36Subordos] PRIMARY KEY CLUSTERED 
(
	[SubordoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl39Infraordos]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl39Infraordos](
	[InfraordoID] [int] IDENTITY(1,1) NOT NULL,
	[InfraordoName] [nvarchar](100) NOT NULL,
	[SubordoID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Synonym] [nvarchar](max) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[EngName] [nvarchar](200) NULL,
	[GerName] [nvarchar](200) NULL,
	[FraName] [nvarchar](200) NULL,
	[PorName] [nvarchar](200) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl39Infraordos] PRIMARY KEY CLUSTERED 
(
	[InfraordoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl42Superfamilies]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl42Superfamilies](
	[SuperfamilyID] [int] IDENTITY(1,1) NOT NULL,
	[SuperfamilyName] [nvarchar](100) NOT NULL,
	[InfraordoID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Synonym] [nvarchar](max) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[EngName] [nvarchar](200) NULL,
	[GerName] [nvarchar](200) NULL,
	[FraName] [nvarchar](200) NULL,
	[PorName] [nvarchar](200) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl42Superfamilies] PRIMARY KEY CLUSTERED 
(
	[SuperfamilyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl45Families]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl45Families](
	[FamilyID] [int] IDENTITY(1,1) NOT NULL,
	[FamilyName] [nvarchar](100) NOT NULL,
	[SuperfamilyID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Synonym] [nvarchar](max) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[EngName] [nvarchar](200) NULL,
	[GerName] [nvarchar](200) NULL,
	[FraName] [nvarchar](200) NULL,
	[PorName] [nvarchar](200) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl45Families] PRIMARY KEY CLUSTERED 
(
	[FamilyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl48Subfamilies]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl48Subfamilies](
	[SubfamilyID] [int] IDENTITY(1,1) NOT NULL,
	[SubfamilyName] [nvarchar](100) NOT NULL,
	[FamilyID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Synonym] [nvarchar](max) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[EngName] [nvarchar](200) NULL,
	[GerName] [nvarchar](200) NULL,
	[FraName] [nvarchar](200) NULL,
	[PorName] [nvarchar](200) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl48Subfamilies] PRIMARY KEY CLUSTERED 
(
	[SubfamilyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl51Infrafamilies]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl51Infrafamilies](
	[InfrafamilyID] [int] IDENTITY(1,1) NOT NULL,
	[InfrafamilyName] [nvarchar](100) NOT NULL,
	[SubfamilyID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Synonym] [nvarchar](max) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[EngName] [nvarchar](200) NULL,
	[GerName] [nvarchar](200) NULL,
	[FraName] [nvarchar](200) NULL,
	[PorName] [nvarchar](200) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl51Infrafamilies] PRIMARY KEY CLUSTERED 
(
	[InfrafamilyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl54Supertribusses]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl54Supertribusses](
	[SupertribusID] [int] IDENTITY(1,1) NOT NULL,
	[SupertribusName] [nvarchar](100) NOT NULL,
	[InfrafamilyID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Synonym] [nvarchar](max) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[EngName] [nvarchar](200) NULL,
	[GerName] [nvarchar](200) NULL,
	[FraName] [nvarchar](200) NULL,
	[PorName] [nvarchar](200) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl54Supertribusses] PRIMARY KEY CLUSTERED 
(
	[SupertribusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl57Tribusses]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl57Tribusses](
	[TribusID] [int] IDENTITY(1,1) NOT NULL,
	[TribusName] [nvarchar](100) NOT NULL,
	[SupertribusID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Synonym] [nvarchar](max) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[EngName] [nvarchar](200) NULL,
	[GerName] [nvarchar](200) NULL,
	[FraName] [nvarchar](200) NULL,
	[PorName] [nvarchar](200) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl57Tribusses] PRIMARY KEY CLUSTERED 
(
	[TribusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl60Subtribusses]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl60Subtribusses](
	[SubtribusID] [int] IDENTITY(1,1) NOT NULL,
	[SubtribusName] [nvarchar](100) NOT NULL,
	[TribusID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Synonym] [nvarchar](max) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[EngName] [nvarchar](200) NULL,
	[GerName] [nvarchar](200) NULL,
	[FraName] [nvarchar](200) NULL,
	[PorName] [nvarchar](200) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl60Subtribusses] PRIMARY KEY CLUSTERED 
(
	[SubtribusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl63Infratribusses]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl63Infratribusses](
	[InfratribusID] [int] IDENTITY(1,1) NOT NULL,
	[InfratribusName] [nvarchar](100) NOT NULL,
	[SubtribusID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Synonym] [nvarchar](max) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[EngName] [nvarchar](200) NULL,
	[GerName] [nvarchar](200) NULL,
	[FraName] [nvarchar](200) NULL,
	[PorName] [nvarchar](200) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl63Infratribusses] PRIMARY KEY CLUSTERED 
(
	[InfratribusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl66Genusses]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl66Genusses](
	[GenusID] [int] IDENTITY(1,1) NOT NULL,
	[GenusName] [nvarchar](100) NOT NULL,
	[InfratribusID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Synonym] [nvarchar](max) NULL,
	[Author] [nvarchar](100) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[EngName] [nvarchar](200) NULL,
	[GerName] [nvarchar](200) NULL,
	[FraName] [nvarchar](200) NULL,
	[PorName] [nvarchar](200) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl66Genusses] PRIMARY KEY CLUSTERED 
(
	[GenusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl68Speciesgroups]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl68Speciesgroups](
	[SpeciesgroupID] [int] IDENTITY(1,1) NOT NULL,
	[SpeciesgroupName] [nvarchar](100) NOT NULL,
	[Subspeciesgroup] [nvarchar](100) NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Synonym] [nvarchar](max) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[EngName] [nvarchar](200) NULL,
	[GerName] [nvarchar](200) NULL,
	[FraName] [nvarchar](200) NULL,
	[PorName] [nvarchar](200) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl68Speciesgroups] PRIMARY KEY CLUSTERED 
(
	[SpeciesgroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl69FiSpeciesses]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl69FiSpeciesses](
	[FiSpeciesID] [int] IDENTITY(1,1) NOT NULL,
	[FiSpeciesName] [nvarchar](100) NOT NULL,
	[Subspecies] [nvarchar](100) NULL,
	[Divers] [nvarchar](100) NULL,
	[GenusID] [int] NOT NULL,
	[SpeciesgroupID] [int] NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[MemoSpecies] [nvarchar](max) NULL,
	[TradeName] [nvarchar](100) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Importer] [nvarchar](60) NULL,
	[ImportingYear] [nvarchar](4) NULL,
	[TypeSpecies] [bit] NULL,
	[LNumber] [nvarchar](10) NULL,
	[LOrigin] [nvarchar](50) NULL,
	[LDANumber] [nvarchar](10) NULL,
	[LDAOrigin] [nvarchar](50) NULL,
	[BasinLength] [int] NULL,
	[FishLength] [decimal](4, 1) NULL,
	[Karnivore] [bit] NULL,
	[Herbivore] [bit] NULL,
	[Limnivore] [bit] NULL,
	[Omnivore] [bit] NULL,
	[MemoFoods] [nvarchar](max) NULL,
	[Difficult1] [bit] NULL,
	[Difficult2] [bit] NULL,
	[Difficult3] [bit] NULL,
	[Difficult4] [bit] NULL,
	[RegionTop] [bit] NULL,
	[RegionMiddle] [bit] NULL,
	[RegionBottom] [bit] NULL,
	[MemoRegion] [nvarchar](max) NULL,
	[MemoTech] [nvarchar](max) NULL,
	[Ph1] [decimal](2, 1) NULL,
	[Ph2] [decimal](2, 1) NULL,
	[Temp1] [int] NULL,
	[Temp2] [int] NULL,
	[Hardness1] [int] NULL,
	[Hardness2] [int] NULL,
	[CarboHardness1] [int] NULL,
	[CarboHardness2] [int] NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[MemoHusbandry] [nvarchar](max) NULL,
	[MemoBreeding] [nvarchar](max) NULL,
	[MemoBuilt] [nvarchar](max) NULL,
	[MemoColor] [nvarchar](max) NULL,
	[MemoSozial] [nvarchar](max) NULL,
	[MemoDomorphism] [nvarchar](max) NULL,
	[MemoSpecial] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl69FiSpeciesses] PRIMARY KEY CLUSTERED 
(
	[FiSpeciesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl72PlSpeciesses]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl72PlSpeciesses](
	[PlSpeciesID] [int] IDENTITY(1,1) NOT NULL,
	[PlSpeciesName] [nvarchar](100) NOT NULL,
	[Subspecies] [nvarchar](100) NULL,
	[Divers] [nvarchar](100) NULL,
	[GenusID] [int] NOT NULL,
	[SpeciesgroupID] [int] NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[MemoSpecies] [nvarchar](max) NULL,
	[TradeName] [nvarchar](100) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Importer] [nvarchar](60) NULL,
	[ImportingYear] [nvarchar](4) NULL,
	[BasinHeight] [int] NULL,
	[PlantLength] [decimal](3, 1) NULL,
	[Difficult1] [bit] NULL,
	[Difficult2] [bit] NULL,
	[Difficult3] [bit] NULL,
	[Difficult4] [bit] NULL,
	[MemoTech] [nvarchar](max) NULL,
	[Ph1] [decimal](2, 1) NULL,
	[Ph2] [decimal](2, 1) NULL,
	[Temp1] [int] NULL,
	[Temp2] [int] NULL,
	[Hardness1] [int] NULL,
	[Hardness2] [int] NULL,
	[CarboHardness1] [int] NULL,
	[CarboHardness2] [int] NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[MemoBuilt] [nvarchar](max) NULL,
	[MemoColor] [nvarchar](max) NULL,
	[MemoReproduction] [nvarchar](max) NULL,
	[MemoCulture] [nvarchar](max) NULL,
	[MemoGlobal] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl72PlSpeciesses] PRIMARY KEY CLUSTERED 
(
	[PlSpeciesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl78Names]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl78Names](
	[NameID] [int] IDENTITY(1,1) NOT NULL,
	[NameName] [nvarchar](100) NOT NULL,
	[FiSpeciesID] [int] NOT NULL,
	[PlSpeciesID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Language] [nvarchar](3) NULL,
	[Info] [nvarchar](100) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl78Names] PRIMARY KEY CLUSTERED 
(
	[NameID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl81Images]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl81Images](
	[ImageID] [int] IDENTITY(1,1) NOT NULL,
	[FiSpeciesID] [int] NOT NULL,
	[PlSpeciesID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[ShotDate] [date] NULL,
	[Info] [nvarchar](100) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[ImageData] [varbinary](max) NULL,
	[ImageMimeType] [nvarchar](50) NULL,
	[Filestream] [varbinary](max) FILESTREAM  NULL,
	[FilestreamID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl81Images] PRIMARY KEY CLUSTERED 
(
	[ImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY] FILESTREAM_ON [VIDEOFILES],
 CONSTRAINT [IX_Tbl81Images_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [uk_medien_stream] UNIQUE NONCLUSTERED 
(
	[FilestreamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] FILESTREAM_ON [VIDEOFILES]
GO
/****** Object:  Table [dbo].[Tbl84Synonyms]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl84Synonyms](
	[SynonymID] [int] IDENTITY(1,1) NOT NULL,
	[SynonymName] [nvarchar](100) NOT NULL,
	[FiSpeciesID] [int] NOT NULL,
	[PlSpeciesID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl84Synonyms] PRIMARY KEY CLUSTERED 
(
	[SynonymID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl87Geographics]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl87Geographics](
	[GeographicID] [int] IDENTITY(1,1) NOT NULL,
	[Address] [nvarchar](100) NULL,
	[Continent] [nvarchar](100) NULL,
	[Country] [nvarchar](100) NULL,
	[FiSpeciesID] [int] NOT NULL,
	[PlSpeciesID] [int] NOT NULL,
	[CountID] [int] NOT NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
	[Latitude1] [float] NOT NULL,
	[Longitude1] [float] NOT NULL,
	[Latitude2] [float] NOT NULL,
	[Longitude2] [float] NOT NULL,
	[Latitude3] [float] NOT NULL,
	[Longitude3] [float] NOT NULL,
	[ZoomLevel] [float] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Author] [nvarchar](60) NULL,
	[AuthorYear] [nvarchar](4) NULL,
	[Http] [nvarchar](100) NULL,
	[Info] [nvarchar](100) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl87Geographics] PRIMARY KEY CLUSTERED 
(
	[GeographicID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl90RefAuthors]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl90RefAuthors](
	[RefAuthorID] [int] IDENTITY(1,1) NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[RefAuthorName] [nvarchar](200) NULL,
	[PublicationYear] [nvarchar](4) NULL,
	[ArticelTitle] [nvarchar](300) NULL,
	[BookName] [nvarchar](200) NULL,
	[Page] [nvarchar](60) NULL,
	[Page1] [nvarchar](60) NULL,
	[Publisher] [nvarchar](200) NULL,
	[PublicationPlace] [nvarchar](100) NULL,
	[ISBN] [nvarchar](60) NULL,
	[Notes] [nvarchar](max) NULL,
	[Info] [nvarchar](100) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl90RefAuthors] PRIMARY KEY CLUSTERED 
(
	[RefAuthorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl90References]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl90References](
	[ReferenceID] [int] IDENTITY(1,1) NOT NULL,
	[FiSpeciesID] [int] NULL,
	[PlSpeciesID] [int] NULL,
	[GenusID] [int] NULL,
	[InfratribusID] [int] NULL,
	[SubtribusID] [int] NULL,
	[TribusID] [int] NULL,
	[SupertribusID] [int] NULL,
	[InfrafamilyID] [int] NULL,
	[SubfamilyID] [int] NULL,
	[FamilyID] [int] NULL,
	[SuperfamilyID] [int] NULL,
	[InfraordoID] [int] NULL,
	[SubordoID] [int] NULL,
	[OrdoID] [int] NULL,
	[LegioID] [int] NULL,
	[InfraclassID] [int] NULL,
	[SubclassID] [int] NULL,
	[ClassID] [int] NULL,
	[SuperclassID] [int] NULL,
	[SubdivisionID] [int] NULL,
	[SubphylumID] [int] NULL,
	[DivisionID] [int] NULL,
	[PhylumID] [int] NULL,
	[RegnumID] [int] NULL,
	[RefExpertID] [int] NULL,
	[RefSourceID] [int] NULL,
	[RefAuthorID] [int] NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl90References] PRIMARY KEY CLUSTERED 
(
	[ReferenceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl90RefExperts]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl90RefExperts](
	[RefExpertID] [int] IDENTITY(1,1) NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[RefExpertName] [nvarchar](300) NULL,
	[Notes] [nvarchar](max) NULL,
	[Info] [nvarchar](100) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl90RefExperts] PRIMARY KEY CLUSTERED 
(
	[RefExpertID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl90RefSources]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl90RefSources](
	[RefSourceID] [int] IDENTITY(1,1) NOT NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[RefSourceName] [nvarchar](300) NULL,
	[SourceYear] [nvarchar](4) NULL,
	[Author] [nvarchar](400) NULL,
	[Notes] [nvarchar](max) NULL,
	[Info] [nvarchar](100) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl90RefSources] PRIMARY KEY CLUSTERED 
(
	[RefSourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl93Comments]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl93Comments](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[FiSpeciesID] [int] NULL,
	[PlSpeciesID] [int] NULL,
	[GenusID] [int] NULL,
	[InfratribusID] [int] NULL,
	[SubtribusID] [int] NULL,
	[TribusID] [int] NULL,
	[SupertribusID] [int] NULL,
	[InfrafamilyID] [int] NULL,
	[SubfamilyID] [int] NULL,
	[FamilyID] [int] NULL,
	[SuperfamilyID] [int] NULL,
	[InfraordoID] [int] NULL,
	[SubordoID] [int] NULL,
	[OrdoID] [int] NULL,
	[LegioID] [int] NULL,
	[InfraclassID] [int] NULL,
	[SubclassID] [int] NULL,
	[ClassID] [int] NULL,
	[SuperclassID] [int] NULL,
	[SubdivisionID] [int] NULL,
	[SubphylumID] [int] NULL,
	[DivisionID] [int] NULL,
	[PhylumID] [int] NULL,
	[RegnumID] [int] NULL,
	[CountID] [int] NOT NULL,
	[Valid] [bit] NULL,
	[ValidYear] [nvarchar](4) NULL,
	[Info] [nvarchar](100) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Tbl93Comments] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblCountries]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblCountries](
	[CountryID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Regex] [nvarchar](250) NULL,
 CONSTRAINT [PK_TblCountries] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblUserProfiles]    Script Date: 14.09.2020 17:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblUserProfiles](
	[UserProfileID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NULL,
	[FirstName] [nvarchar](100) NULL,
	[Title] [nvarchar](100) NULL,
	[Password] [nvarchar](250) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
	[Flag] [bit] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Notes] [nvarchar](max) NULL,
	[Colour] [nvarchar](100) NULL,
	[CountID] [int] NOT NULL,
	[BirthDate] [datetime] NULL,
	[Gender] [nvarchar](50) NULL,
	[Country] [nvarchar](100) NULL,
	[Postcode] [nvarchar](50) NULL,
	[City] [nvarchar](100) NULL,
	[Street1] [nvarchar](100) NULL,
	[Street2] [nvarchar](100) NULL,
	[Tel] [nvarchar](100) NULL,
	[Mobil] [nvarchar](100) NULL,
	[Fax] [nvarchar](100) NULL,
	[HomePageURL] [nvarchar](100) NULL,
	[Business] [nvarchar](100) NULL,
	[Company] [nvarchar](100) NULL,
	[Writer] [nvarchar](50) NOT NULL,
	[WriterDate] [datetime] NOT NULL,
	[Updater] [nvarchar](50) NOT NULL,
	[UpdaterDate] [datetime] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[Filestream] [varbinary](max) NULL,
	[ImageMimeType] [nvarchar](50) NULL,
	[FilestreamID] [uniqueidentifier] NULL,
	[Signature] [nvarchar](50) NULL,
	[MailNewsletter] [bit] NULL,
	[MaulHTML] [bit] NULL,
	[Known] [nvarchar](100) NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_UserProfiles] PRIMARY KEY CLUSTERED 
(
	[UserProfileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Tbl03Regnums] ON 

INSERT [dbo].[Tbl03Regnums] ([RegnumID], [RegnumName], [Subregnum], [CountID], [Valid], [ValidYear], [Synonym], [Author], [AuthorYear], [Info], [EngName], [GerName], [FraName], [PorName], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (4, N'Animalia', NULL, 798682585, NULL, NULL, NULL, NULL, NULL, NULL, N'animal', NULL, N'animaux', NULL, N'RT', CAST(N'2020-06-06T17:17:06.437' AS DateTime), N'RT', CAST(N'2020-06-06T17:17:06.437' AS DateTime), NULL)
INSERT [dbo].[Tbl03Regnums] ([RegnumID], [RegnumName], [Subregnum], [CountID], [Valid], [ValidYear], [Synonym], [Author], [AuthorYear], [Info], [EngName], [GerName], [FraName], [PorName], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (5, N'Plantae', NULL, 205224770, NULL, NULL, NULL, NULL, NULL, NULL, N'planta', N'Pflanzen', NULL, NULL, N'RT', CAST(N'2020-06-06T17:29:07.160' AS DateTime), N'RT', CAST(N'2020-06-06T17:29:07.160' AS DateTime), NULL)
INSERT [dbo].[Tbl03Regnums] ([RegnumID], [RegnumName], [Subregnum], [CountID], [Valid], [ValidYear], [Synonym], [Author], [AuthorYear], [Info], [EngName], [GerName], [FraName], [PorName], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (6, N'Test11', NULL, 665292332, 1, NULL, N'fgfbffgb', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'RT', CAST(N'2020-06-07T17:55:23.433' AS DateTime), N'RT', CAST(N'2020-08-23T17:41:23.253' AS DateTime), NULL)
INSERT [dbo].[Tbl03Regnums] ([RegnumID], [RegnumName], [Subregnum], [CountID], [Valid], [ValidYear], [Synonym], [Author], [AuthorYear], [Info], [EngName], [GerName], [FraName], [PorName], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (7, N'Test10', N'ttt', 544901095, NULL, NULL, N'gbgbfff 
ffffffffffffffffffffffff vvvvvvvvvvvvv ccccccccccccccc', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'RT', CAST(N'2020-07-05T14:38:41.800' AS DateTime), N'RT', CAST(N'2020-08-18T18:58:35.477' AS DateTime), NULL)
INSERT [dbo].[Tbl03Regnums] ([RegnumID], [RegnumName], [Subregnum], [CountID], [Valid], [ValidYear], [Synonym], [Author], [AuthorYear], [Info], [EngName], [GerName], [FraName], [PorName], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (8, N'Test12', N'hfhfh', 86510373, NULL, NULL, N'gfgfgfg', N'fgfgfgf', NULL, NULL, NULL, NULL, NULL, NULL, N'RT', CAST(N'2020-07-05T14:39:38.747' AS DateTime), N'RT', CAST(N'2020-07-05T14:39:38.747' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Tbl03Regnums] OFF
GO
SET IDENTITY_INSERT [dbo].[Tbl06Phylums] ON 

INSERT [dbo].[Tbl06Phylums] ([PhylumID], [PhylumName], [RegnumID], [CountID], [Valid], [ValidYear], [Synonym], [Author], [AuthorYear], [Info], [EngName], [GerName], [FraName], [PorName], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (1, N'Chordata', 4, 596549600, NULL, NULL, NULL, NULL, NULL, N'dfgdfd', NULL, NULL, NULL, NULL, N'RT', CAST(N'2020-06-07T14:27:54.847' AS DateTime), N'RT', CAST(N'2020-06-07T14:27:54.847' AS DateTime), NULL)
INSERT [dbo].[Tbl06Phylums] ([PhylumID], [PhylumName], [RegnumID], [CountID], [Valid], [ValidYear], [Synonym], [Author], [AuthorYear], [Info], [EngName], [GerName], [FraName], [PorName], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (2, N'Euglenophycota', 5, 596842380, NULL, NULL, NULL, NULL, NULL, N'jklljllui', NULL, NULL, NULL, NULL, N'RT', CAST(N'2020-06-07T14:32:32.617' AS DateTime), N'RT', CAST(N'2020-06-07T14:32:32.617' AS DateTime), NULL)
INSERT [dbo].[Tbl06Phylums] ([PhylumID], [PhylumName], [RegnumID], [CountID], [Valid], [ValidYear], [Synonym], [Author], [AuthorYear], [Info], [EngName], [GerName], [FraName], [PorName], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (4, N'Test2', 6, 163231119, 1, N'1910', NULL, NULL, NULL, N' geändert update dapper', NULL, NULL, NULL, NULL, N'RT', CAST(N'2020-06-07T14:37:37.737' AS DateTime), N'RT', CAST(N'2020-08-06T00:00:00.000' AS DateTime), N'dapper')
INSERT [dbo].[Tbl06Phylums] ([PhylumID], [PhylumName], [RegnumID], [CountID], [Valid], [ValidYear], [Synonym], [Author], [AuthorYear], [Info], [EngName], [GerName], [FraName], [PorName], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (5, N'Test3', 4, 352902290, NULL, NULL, NULL, NULL, NULL, N'dfaffhfghg geändert UOW', NULL, NULL, NULL, NULL, N'RT', CAST(N'2020-06-07T15:19:09.430' AS DateTime), N'RT', CAST(N'2020-06-07T17:43:34.060' AS DateTime), NULL)
INSERT [dbo].[Tbl06Phylums] ([PhylumID], [PhylumName], [RegnumID], [CountID], [Valid], [ValidYear], [Synonym], [Author], [AuthorYear], [Info], [EngName], [GerName], [FraName], [PorName], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (6, N'Test4', 7, 318292551, 1, NULL, NULL, NULL, NULL, N'jhhsgsggf', NULL, NULL, NULL, NULL, N'RT', CAST(N'2020-06-07T17:47:33.847' AS DateTime), N'RT', CAST(N'2020-06-07T17:50:02.700' AS DateTime), NULL)
INSERT [dbo].[Tbl06Phylums] ([PhylumID], [PhylumName], [RegnumID], [CountID], [Valid], [ValidYear], [Synonym], [Author], [AuthorYear], [Info], [EngName], [GerName], [FraName], [PorName], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (7, N'Test6', 6, 720360824, NULL, NULL, NULL, NULL, NULL, N'dfaffhfghg geändert 
UOW aus copy', NULL, NULL, NULL, NULL, N'RT', CAST(N'2020-06-07T17:48:51.930' AS DateTime), N'RT', CAST(N'2020-08-18T18:59:39.087' AS DateTime), NULL)
INSERT [dbo].[Tbl06Phylums] ([PhylumID], [PhylumName], [RegnumID], [CountID], [Valid], [ValidYear], [Synonym], [Author], [AuthorYear], [Info], [EngName], [GerName], [FraName], [PorName], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (9, N'Test Dapper 1', 4, 787472663, NULL, N'1920', NULL, NULL, NULL, N'Dapper', NULL, NULL, NULL, NULL, N'RT', CAST(N'2020-06-08T19:28:30.077' AS DateTime), N'RT', CAST(N'2020-06-08T19:28:30.077' AS DateTime), NULL)
INSERT [dbo].[Tbl06Phylums] ([PhylumID], [PhylumName], [RegnumID], [CountID], [Valid], [ValidYear], [Synonym], [Author], [AuthorYear], [Info], [EngName], [GerName], [FraName], [PorName], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (12, N'Dapper2', 4, 606596649, NULL, N'1922', NULL, NULL, NULL, N'Dapper 2 update update', NULL, NULL, NULL, NULL, N'RT', CAST(N'2020-06-08T19:49:25.853' AS DateTime), N'RT', CAST(N'2020-06-09T14:43:20.850' AS DateTime), NULL)
INSERT [dbo].[Tbl06Phylums] ([PhylumID], [PhylumName], [RegnumID], [CountID], [Valid], [ValidYear], [Synonym], [Author], [AuthorYear], [Info], [EngName], [GerName], [FraName], [PorName], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (13, N'dapper3', 4, 771883411, NULL, N'1930', NULL, NULL, NULL, N'Dapper neu update cvcncncnc', NULL, NULL, NULL, NULL, N'RT', CAST(N'2020-06-09T14:44:24.743' AS DateTime), N'RT', CAST(N'2020-06-18T15:57:55.613' AS DateTime), NULL)
INSERT [dbo].[Tbl06Phylums] ([PhylumID], [PhylumName], [RegnumID], [CountID], [Valid], [ValidYear], [Synonym], [Author], [AuthorYear], [Info], [EngName], [GerName], [FraName], [PorName], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (14, N'Dapper4', 4, 103637072, NULL, N'1931', NULL, NULL, NULL, N'Dapper4 neu', NULL, NULL, NULL, NULL, N'RT', CAST(N'2020-06-09T14:47:38.500' AS DateTime), N'RT', CAST(N'2020-06-09T14:47:38.500' AS DateTime), NULL)
INSERT [dbo].[Tbl06Phylums] ([PhylumID], [PhylumName], [RegnumID], [CountID], [Valid], [ValidYear], [Synonym], [Author], [AuthorYear], [Info], [EngName], [GerName], [FraName], [PorName], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (15, N'Test6', 7, 952053112, NULL, NULL, NULL, NULL, NULL, N'dsdsdsd', NULL, NULL, NULL, NULL, N'RT', CAST(N'2020-06-18T15:58:46.467' AS DateTime), N'RT', CAST(N'2020-06-18T15:58:46.467' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Tbl06Phylums] OFF
GO
SET IDENTITY_INSERT [dbo].[Tbl09Divisions] ON 

INSERT [dbo].[Tbl09Divisions] ([DivisionID], [DivisionName], [RegnumID], [CountID], [Valid], [ValidYear], [Synonym], [Author], [AuthorYear], [Info], [EngName], [GerName], [FraName], [PorName], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (1, N'Test Division 1', 6, 452830, 0, NULL, N'Corydoras', N'Meier', N'1910', NULL, NULL, N'Panzerwels', NULL, NULL, N'RT', CAST(N'2020-05-05T00:00:00.000' AS DateTime), N'RT', CAST(N'2020-05-05T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Tbl09Divisions] ([DivisionID], [DivisionName], [RegnumID], [CountID], [Valid], [ValidYear], [Synonym], [Author], [AuthorYear], [Info], [EngName], [GerName], [FraName], [PorName], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (2, N'Test Division 2', 7, 6094567, 0, NULL, N'Betta', N'Schulze', N'1966', NULL, NULL, N'Kampffisch', NULL, NULL, N'RT', CAST(N'2020-06-05T00:00:00.000' AS DateTime), N'RT', CAST(N'2020-05-06T00:00:00.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Tbl09Divisions] OFF
GO
SET IDENTITY_INSERT [dbo].[Tbl12Subphylums] ON 

INSERT [dbo].[Tbl12Subphylums] ([SubphylumID], [SubphylumName], [PhylumID], [CountID], [Valid], [ValidYear], [Synonym], [Author], [AuthorYear], [Info], [EngName], [GerName], [FraName], [PorName], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (1, N'Subphylum 1', 4, 210452, 0, NULL, N'Cora', N'Schulze', NULL, NULL, NULL, NULL, NULL, NULL, N'RT', CAST(N'2020-05-05T00:00:00.000' AS DateTime), N'RT', CAST(N'2020-05-05T00:00:00.000' AS DateTime), N'fgdgdg')
INSERT [dbo].[Tbl12Subphylums] ([SubphylumID], [SubphylumName], [PhylumID], [CountID], [Valid], [ValidYear], [Synonym], [Author], [AuthorYear], [Info], [EngName], [GerName], [FraName], [PorName], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (2, N'Subphylum 2', 6, 5678324, 0, NULL, N'Ballustus', N'Fritz', NULL, NULL, NULL, NULL, NULL, NULL, N'RT', CAST(N'2020-05-06T00:00:00.000' AS DateTime), N'RT', CAST(N'2020-05-06T00:00:00.000' AS DateTime), N'lululul')
SET IDENTITY_INSERT [dbo].[Tbl12Subphylums] OFF
GO
SET IDENTITY_INSERT [dbo].[Tbl15Subdivisions] ON 

INSERT [dbo].[Tbl15Subdivisions] ([SubdivisionID], [SubdivisionName], [DivisionID], [CountID], [Valid], [ValidYear], [Synonym], [Author], [AuthorYear], [Info], [EngName], [GerName], [FraName], [PorName], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (1, N'Subdivision 1', 1, 5265874, 0, NULL, N'Valisneria', N'Georgus', NULL, NULL, NULL, N'Valisnerien', NULL, NULL, N'RT', CAST(N'2020-05-06T00:00:00.000' AS DateTime), N'RT', CAST(N'2020-05-06T00:00:00.000' AS DateTime), N'efgregrwg')
INSERT [dbo].[Tbl15Subdivisions] ([SubdivisionID], [SubdivisionName], [DivisionID], [CountID], [Valid], [ValidYear], [Synonym], [Author], [AuthorYear], [Info], [EngName], [GerName], [FraName], [PorName], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (2, N'Subdivision 2', 2, 52694210, 0, NULL, N'Rothodent', N'carlus', N'1952', NULL, NULL, N'Rothodendren', NULL, NULL, N'RT', CAST(N'2020-05-05T00:00:00.000' AS DateTime), N'RT', CAST(N'2020-05-05T00:00:00.000' AS DateTime), N'jikukrgedf')
SET IDENTITY_INSERT [dbo].[Tbl15Subdivisions] OFF
GO
SET IDENTITY_INSERT [dbo].[Tbl90RefAuthors] ON 

INSERT [dbo].[Tbl90RefAuthors] ([RefAuthorID], [CountID], [Valid], [ValidYear], [RefAuthorName], [PublicationYear], [ArticelTitle], [BookName], [Page], [Page1], [Publisher], [PublicationPlace], [ISBN], [Notes], [Info], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (1, 524630, 0, NULL, N'Author Test 1', N'1955', N'Title 1', N'BookName 1', NULL, N'55', N'Rudi', N'New York', NULL, N'dgdgdgdg', NULL, N'RT', CAST(N'2020-05-05T00:00:00.000' AS DateTime), N'RT', CAST(N'2020-05-05T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Tbl90RefAuthors] ([RefAuthorID], [CountID], [Valid], [ValidYear], [RefAuthorName], [PublicationYear], [ArticelTitle], [BookName], [Page], [Page1], [Publisher], [PublicationPlace], [ISBN], [Notes], [Info], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (3, 63254, 0, NULL, N'Author Test 2', N'1956', N'Title 2', N'BookName 2', NULL, N'56', N'Paul', N'Hamburg', NULL, N'jkkkikiz', NULL, N'RT', CAST(N'2020-05-06T00:00:00.000' AS DateTime), N'RT', CAST(N'2020-05-06T00:00:00.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Tbl90RefAuthors] OFF
GO
SET IDENTITY_INSERT [dbo].[Tbl90References] ON 

INSERT [dbo].[Tbl90References] ([ReferenceID], [FiSpeciesID], [PlSpeciesID], [GenusID], [InfratribusID], [SubtribusID], [TribusID], [SupertribusID], [InfrafamilyID], [SubfamilyID], [FamilyID], [SuperfamilyID], [InfraordoID], [SubordoID], [OrdoID], [LegioID], [InfraclassID], [SubclassID], [ClassID], [SuperclassID], [SubdivisionID], [SubphylumID], [DivisionID], [PhylumID], [RegnumID], [RefExpertID], [RefSourceID], [RefAuthorID], [CountID], [Valid], [ValidYear], [Info], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8, NULL, NULL, NULL, 398902520, NULL, NULL, N'Test1', N'RT', CAST(N'2020-07-28T20:18:43.013' AS DateTime), N'RT', CAST(N'2020-07-28T20:18:43.013' AS DateTime), N'xcvxvcxvcxxcc')
INSERT [dbo].[Tbl90References] ([ReferenceID], [FiSpeciesID], [PlSpeciesID], [GenusID], [InfratribusID], [SubtribusID], [TribusID], [SupertribusID], [InfrafamilyID], [SubfamilyID], [FamilyID], [SuperfamilyID], [InfraordoID], [SubordoID], [OrdoID], [LegioID], [InfraclassID], [SubclassID], [ClassID], [SuperclassID], [SubdivisionID], [SubphylumID], [DivisionID], [PhylumID], [RegnumID], [RefExpertID], [RefSourceID], [RefAuthorID], [CountID], [Valid], [ValidYear], [Info], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8, NULL, NULL, NULL, 584019946, NULL, NULL, N'Test2', N'RT', CAST(N'2020-07-28T20:19:03.127' AS DateTime), N'RT', CAST(N'2020-07-28T20:19:03.127' AS DateTime), N'ghgjgjhghjg')
INSERT [dbo].[Tbl90References] ([ReferenceID], [FiSpeciesID], [PlSpeciesID], [GenusID], [InfratribusID], [SubtribusID], [TribusID], [SupertribusID], [InfrafamilyID], [SubfamilyID], [FamilyID], [SuperfamilyID], [InfraordoID], [SubordoID], [OrdoID], [LegioID], [InfraclassID], [SubclassID], [ClassID], [SuperclassID], [SubdivisionID], [SubphylumID], [DivisionID], [PhylumID], [RegnumID], [RefExpertID], [RefSourceID], [RefAuthorID], [CountID], [Valid], [ValidYear], [Info], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (3, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8, NULL, NULL, NULL, 589936624, NULL, NULL, N'Test3', N'RT', CAST(N'2020-07-28T20:19:59.767' AS DateTime), N'RT', CAST(N'2020-07-28T20:19:59.770' AS DateTime), NULL)
INSERT [dbo].[Tbl90References] ([ReferenceID], [FiSpeciesID], [PlSpeciesID], [GenusID], [InfratribusID], [SubtribusID], [TribusID], [SupertribusID], [InfrafamilyID], [SubfamilyID], [FamilyID], [SuperfamilyID], [InfraordoID], [SubordoID], [OrdoID], [LegioID], [InfraclassID], [SubclassID], [ClassID], [SuperclassID], [SubdivisionID], [SubphylumID], [DivisionID], [PhylumID], [RegnumID], [RefExpertID], [RefSourceID], [RefAuthorID], [CountID], [Valid], [ValidYear], [Info], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 6, 1, NULL, NULL, 15119599, NULL, NULL, N'Neu angelegt', N'RT', CAST(N'2020-09-03T17:43:03.300' AS DateTime), N'RT', CAST(N'2020-09-03T17:43:03.300' AS DateTime), NULL)
INSERT [dbo].[Tbl90References] ([ReferenceID], [FiSpeciesID], [PlSpeciesID], [GenusID], [InfratribusID], [SubtribusID], [TribusID], [SupertribusID], [InfrafamilyID], [SubfamilyID], [FamilyID], [SuperfamilyID], [InfraordoID], [SubordoID], [OrdoID], [LegioID], [InfraclassID], [SubclassID], [ClassID], [SuperclassID], [SubdivisionID], [SubphylumID], [DivisionID], [PhylumID], [RegnumID], [RefExpertID], [RefSourceID], [RefAuthorID], [CountID], [Valid], [ValidYear], [Info], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 6, NULL, 4, NULL, 998600273, NULL, NULL, N'Neue Datenbank', N'RT', CAST(N'2020-09-03T17:43:33.950' AS DateTime), N'RT', CAST(N'2020-09-03T17:43:33.950' AS DateTime), NULL)
INSERT [dbo].[Tbl90References] ([ReferenceID], [FiSpeciesID], [PlSpeciesID], [GenusID], [InfratribusID], [SubtribusID], [TribusID], [SupertribusID], [InfrafamilyID], [SubfamilyID], [FamilyID], [SuperfamilyID], [InfraordoID], [SubordoID], [OrdoID], [LegioID], [InfraclassID], [SubclassID], [ClassID], [SuperclassID], [SubdivisionID], [SubphylumID], [DivisionID], [PhylumID], [RegnumID], [RefExpertID], [RefSourceID], [RefAuthorID], [CountID], [Valid], [ValidYear], [Info], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 6, NULL, NULL, 1, 554699623, NULL, NULL, N'Neuer Author ffffffffffffffffffffffffff', N'RT', CAST(N'2020-09-03T17:44:11.670' AS DateTime), N'RT', CAST(N'2020-09-03T17:44:11.670' AS DateTime), NULL)
INSERT [dbo].[Tbl90References] ([ReferenceID], [FiSpeciesID], [PlSpeciesID], [GenusID], [InfratribusID], [SubtribusID], [TribusID], [SupertribusID], [InfrafamilyID], [SubfamilyID], [FamilyID], [SuperfamilyID], [InfraordoID], [SubordoID], [OrdoID], [LegioID], [InfraclassID], [SubclassID], [ClassID], [SuperclassID], [SubdivisionID], [SubphylumID], [DivisionID], [PhylumID], [RegnumID], [RefExpertID], [RefSourceID], [RefAuthorID], [CountID], [Valid], [ValidYear], [Info], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (7, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 4, NULL, 2, NULL, NULL, 5254151, NULL, NULL, N'Neu Test 2', N'RT', CAST(N'2020-09-03T17:46:26.023' AS DateTime), N'RT', CAST(N'2020-09-03T17:46:26.023' AS DateTime), NULL)
INSERT [dbo].[Tbl90References] ([ReferenceID], [FiSpeciesID], [PlSpeciesID], [GenusID], [InfratribusID], [SubtribusID], [TribusID], [SupertribusID], [InfrafamilyID], [SubfamilyID], [FamilyID], [SuperfamilyID], [InfraordoID], [SubordoID], [OrdoID], [LegioID], [InfraclassID], [SubclassID], [ClassID], [SuperclassID], [SubdivisionID], [SubphylumID], [DivisionID], [PhylumID], [RegnumID], [RefExpertID], [RefSourceID], [RefAuthorID], [CountID], [Valid], [ValidYear], [Info], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 4, NULL, NULL, 4, NULL, 488125849, NULL, NULL, N'Neu datenbank 2 Test 2', N'RT', CAST(N'2020-09-03T17:46:56.397' AS DateTime), N'RT', CAST(N'2020-09-03T17:46:56.397' AS DateTime), NULL)
INSERT [dbo].[Tbl90References] ([ReferenceID], [FiSpeciesID], [PlSpeciesID], [GenusID], [InfratribusID], [SubtribusID], [TribusID], [SupertribusID], [InfrafamilyID], [SubfamilyID], [FamilyID], [SuperfamilyID], [InfraordoID], [SubordoID], [OrdoID], [LegioID], [InfraclassID], [SubclassID], [ClassID], [SuperclassID], [SubdivisionID], [SubphylumID], [DivisionID], [PhylumID], [RegnumID], [RefExpertID], [RefSourceID], [RefAuthorID], [CountID], [Valid], [ValidYear], [Info], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (9, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 4, NULL, NULL, NULL, 3, 284068020, NULL, NULL, N'Neu Author Test 2', N'RT', CAST(N'2020-09-03T17:47:25.030' AS DateTime), N'RT', CAST(N'2020-09-03T17:47:25.030' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Tbl90References] OFF
GO
SET IDENTITY_INSERT [dbo].[Tbl90RefExperts] ON 

INSERT [dbo].[Tbl90RefExperts] ([RefExpertID], [CountID], [Valid], [ValidYear], [RefExpertName], [Notes], [Info], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (1, 3695231, 0, NULL, N'Meier', N'uzizkuk', NULL, N'RT', CAST(N'2020-05-05T00:00:00.000' AS DateTime), N'RT', CAST(N'2020-05-05T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Tbl90RefExperts] ([RefExpertID], [CountID], [Valid], [ValidYear], [RefExpertName], [Notes], [Info], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (2, 785120, 0, NULL, N'Schulze', N'asacsaca', NULL, N'RT', CAST(N'2020-05-06T00:00:00.000' AS DateTime), N'RT', CAST(N'2020-05-06T00:00:00.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Tbl90RefExperts] OFF
GO
SET IDENTITY_INSERT [dbo].[Tbl90RefSources] ON 

INSERT [dbo].[Tbl90RefSources] ([RefSourceID], [CountID], [Valid], [ValidYear], [RefSourceName], [SourceYear], [Author], [Notes], [Info], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (1, 420158, 0, NULL, N'Datenbank 1', N'1955', N'Terppe', N'hhhfhhjhj', NULL, N'RT', CAST(N'2020-05-05T00:00:00.000' AS DateTime), N'RT', CAST(N'2020-05-05T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Tbl90RefSources] ([RefSourceID], [CountID], [Valid], [ValidYear], [RefSourceName], [SourceYear], [Author], [Notes], [Info], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (4, 852412, 0, NULL, N'Datenbank 2', N'1960', N'Jan', N'fdfdd', NULL, N'RT', CAST(N'2020-05-06T00:00:00.000' AS DateTime), N'RT', CAST(N'2020-05-06T00:00:00.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Tbl90RefSources] OFF
GO
SET IDENTITY_INSERT [dbo].[Tbl93Comments] ON 

INSERT [dbo].[Tbl93Comments] ([CommentID], [FiSpeciesID], [PlSpeciesID], [GenusID], [InfratribusID], [SubtribusID], [TribusID], [SupertribusID], [InfrafamilyID], [SubfamilyID], [FamilyID], [SuperfamilyID], [InfraordoID], [SubordoID], [OrdoID], [LegioID], [InfraclassID], [SubclassID], [ClassID], [SuperclassID], [SubdivisionID], [SubphylumID], [DivisionID], [PhylumID], [RegnumID], [CountID], [Valid], [ValidYear], [Info], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8, 111585692, NULL, NULL, N'Test4', N'RT', CAST(N'2020-07-28T20:20:25.840' AS DateTime), N'RT', CAST(N'2020-07-28T20:20:25.843' AS DateTime), N'ffbdngdn')
INSERT [dbo].[Tbl93Comments] ([CommentID], [FiSpeciesID], [PlSpeciesID], [GenusID], [InfratribusID], [SubtribusID], [TribusID], [SupertribusID], [InfrafamilyID], [SubfamilyID], [FamilyID], [SuperfamilyID], [InfraordoID], [SubordoID], [OrdoID], [LegioID], [InfraclassID], [SubclassID], [ClassID], [SuperclassID], [SubdivisionID], [SubphylumID], [DivisionID], [PhylumID], [RegnumID], [CountID], [Valid], [ValidYear], [Info], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 6, 129989399, NULL, NULL, N'Neu Comment gggggggggggggggggggggggg', N'RT', CAST(N'2020-09-03T17:44:49.423' AS DateTime), N'RT', CAST(N'2020-09-03T17:44:49.423' AS DateTime), NULL)
INSERT [dbo].[Tbl93Comments] ([CommentID], [FiSpeciesID], [PlSpeciesID], [GenusID], [InfratribusID], [SubtribusID], [TribusID], [SupertribusID], [InfrafamilyID], [SubfamilyID], [FamilyID], [SuperfamilyID], [InfraordoID], [SubordoID], [OrdoID], [LegioID], [InfraclassID], [SubclassID], [ClassID], [SuperclassID], [SubdivisionID], [SubphylumID], [DivisionID], [PhylumID], [RegnumID], [CountID], [Valid], [ValidYear], [Info], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) VALUES (3, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 4, NULL, 477066069, NULL, NULL, N'Neu Comment Test 2', N'RT', CAST(N'2020-09-03T17:47:55.683' AS DateTime), N'RT', CAST(N'2020-09-03T17:47:55.683' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Tbl93Comments] OFF
GO
SET IDENTITY_INSERT [dbo].[TblUserProfiles] ON 

INSERT [dbo].[TblUserProfiles] ([UserProfileID], [Email], [LastName], [FirstName], [Title], [Password], [Role], [Flag], [StartDate], [EndDate], [Notes], [Colour], [CountID], [BirthDate], [Gender], [Country], [Postcode], [City], [Street1], [Street2], [Tel], [Mobil], [Fax], [HomePageURL], [Business], [Company], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo], [Filestream], [ImageMimeType], [FilestreamID], [Signature], [MailNewsletter], [MaulHTML], [Known]) VALUES (1, N'rudolf@terppe.de', NULL, NULL, NULL, N'erdQ5bRF3u8fYp8WQlKBK5yqfQMovOk8kns4coIjitMHkV9q26OU1332JRFjwhEOKKfqWxGq6AWTBf4i1Id/Dg==', N'Administrator', NULL, CAST(N'2020-09-14T17:01:04.630' AS DateTime), CAST(N'2020-09-14T17:01:04.630' AS DateTime), NULL, NULL, 677947716, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'RT', CAST(N'2020-09-14T17:01:04.633' AS DateTime), N'RT', CAST(N'2020-09-14T17:01:04.633' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[TblUserProfiles] ([UserProfileID], [Email], [LastName], [FirstName], [Title], [Password], [Role], [Flag], [StartDate], [EndDate], [Notes], [Colour], [CountID], [BirthDate], [Gender], [Country], [Postcode], [City], [Street1], [Street2], [Tel], [Mobil], [Fax], [HomePageURL], [Business], [Company], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo], [Filestream], [ImageMimeType], [FilestreamID], [Signature], [MailNewsletter], [MaulHTML], [Known]) VALUES (2, N'marion@terppe.de', NULL, NULL, NULL, N'/Zg8bxF0L2HN2DJGwOlcSEI1swVVVru4PrEMqnvpA+fwzTHZd7Q7lYMjF/5j+/Ii1ZPNFioT1qZ4Un7AS6et7Q==', N'User', NULL, CAST(N'2020-09-14T17:03:34.537' AS DateTime), CAST(N'2020-09-14T17:03:34.537' AS DateTime), NULL, NULL, 165492167, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'RT', CAST(N'2020-09-14T17:03:34.540' AS DateTime), N'RT', CAST(N'2020-09-14T17:03:34.540' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[TblUserProfiles] OFF
GO
/****** Object:  Index [IX_Tbl03Regnums_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl03Regnums] ADD  CONSTRAINT [IX_Tbl03Regnums_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl03Regnums_RegnumName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl03Regnums] ADD  CONSTRAINT [IX_Tbl03Regnums_RegnumName] UNIQUE NONCLUSTERED 
(
	[RegnumName] ASC,
	[Subregnum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl06Phylums_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl06Phylums] ADD  CONSTRAINT [IX_Tbl06Phylums_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl06Phylums_PhylumName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl06Phylums] ADD  CONSTRAINT [IX_Tbl06Phylums_PhylumName] UNIQUE NONCLUSTERED 
(
	[PhylumName] ASC,
	[RegnumID] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl09Divisions_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl09Divisions] ADD  CONSTRAINT [IX_Tbl09Divisions_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl09Divisions_DivisionName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl09Divisions] ADD  CONSTRAINT [IX_Tbl09Divisions_DivisionName] UNIQUE NONCLUSTERED 
(
	[DivisionName] ASC,
	[RegnumID] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl12Subphylums_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl12Subphylums] ADD  CONSTRAINT [IX_Tbl12Subphylums_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl12Subphylums_SubphylumName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl12Subphylums] ADD  CONSTRAINT [IX_Tbl12Subphylums_SubphylumName] UNIQUE NONCLUSTERED 
(
	[SubphylumName] ASC,
	[PhylumID] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl15Subdivisions_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl15Subdivisions] ADD  CONSTRAINT [IX_Tbl15Subdivisions_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl15Subdivisions_SubdivisionName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl15Subdivisions] ADD  CONSTRAINT [IX_Tbl15Subdivisions_SubdivisionName] UNIQUE NONCLUSTERED 
(
	[SubdivisionName] ASC,
	[DivisionID] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl18Superclasses_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl18Superclasses] ADD  CONSTRAINT [IX_Tbl18Superclasses_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl18Superclasses_SuperclassName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl18Superclasses] ADD  CONSTRAINT [IX_Tbl18Superclasses_SuperclassName] UNIQUE NONCLUSTERED 
(
	[SuperclassName] ASC,
	[SubdivisionID] ASC,
	[SubphylumID] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl21Classes_ClassName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl21Classes] ADD  CONSTRAINT [IX_Tbl21Classes_ClassName] UNIQUE NONCLUSTERED 
(
	[ClassName] ASC,
	[SuperclassID] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl21Classes_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl21Classes] ADD  CONSTRAINT [IX_Tbl21Classes_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl24Subclasses_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl24Subclasses] ADD  CONSTRAINT [IX_Tbl24Subclasses_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl24Subclasses_SubclassName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl24Subclasses] ADD  CONSTRAINT [IX_Tbl24Subclasses_SubclassName] UNIQUE NONCLUSTERED 
(
	[SubclassName] ASC,
	[ClassID] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl27Infraclasses_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl27Infraclasses] ADD  CONSTRAINT [IX_Tbl27Infraclasses_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl27Infraclasses_InfraclassName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl27Infraclasses] ADD  CONSTRAINT [IX_Tbl27Infraclasses_InfraclassName] UNIQUE NONCLUSTERED 
(
	[InfraclassName] ASC,
	[SubclassID] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl30Legios_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl30Legios] ADD  CONSTRAINT [IX_Tbl30Legios_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl30Legios_LegioName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl30Legios] ADD  CONSTRAINT [IX_Tbl30Legios_LegioName] UNIQUE NONCLUSTERED 
(
	[LegioName] ASC,
	[InfraclassID] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl33Ordos_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl33Ordos] ADD  CONSTRAINT [IX_Tbl33Ordos_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl33Ordos_OrdoName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl33Ordos] ADD  CONSTRAINT [IX_Tbl33Ordos_OrdoName] UNIQUE NONCLUSTERED 
(
	[OrdoName] ASC,
	[LegioID] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl36Subordos_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl36Subordos] ADD  CONSTRAINT [IX_Tbl36Subordos_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl36Subordos_SubordoName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl36Subordos] ADD  CONSTRAINT [IX_Tbl36Subordos_SubordoName] UNIQUE NONCLUSTERED 
(
	[SubordoName] ASC,
	[OrdoID] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl39Infraordos_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl39Infraordos] ADD  CONSTRAINT [IX_Tbl39Infraordos_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl39Infraordos_InfraordoName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl39Infraordos] ADD  CONSTRAINT [IX_Tbl39Infraordos_InfraordoName] UNIQUE NONCLUSTERED 
(
	[InfraordoName] ASC,
	[SubordoID] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl42Superfamilies_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl42Superfamilies] ADD  CONSTRAINT [IX_Tbl42Superfamilies_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl42Superfamilies_SuperfamilyName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl42Superfamilies] ADD  CONSTRAINT [IX_Tbl42Superfamilies_SuperfamilyName] UNIQUE NONCLUSTERED 
(
	[SuperfamilyName] ASC,
	[InfraordoID] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl45Families_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl45Families] ADD  CONSTRAINT [IX_Tbl45Families_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl45Families_FamilyName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl45Families] ADD  CONSTRAINT [IX_Tbl45Families_FamilyName] UNIQUE NONCLUSTERED 
(
	[FamilyName] ASC,
	[SuperfamilyID] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl48Subfamilies_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl48Subfamilies] ADD  CONSTRAINT [IX_Tbl48Subfamilies_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl48Subfamilies_SubfamilyName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl48Subfamilies] ADD  CONSTRAINT [IX_Tbl48Subfamilies_SubfamilyName] UNIQUE NONCLUSTERED 
(
	[SubfamilyName] ASC,
	[FamilyID] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl51Infrafamilies_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl51Infrafamilies] ADD  CONSTRAINT [IX_Tbl51Infrafamilies_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl51Infrafamilies_InfrafamilyName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl51Infrafamilies] ADD  CONSTRAINT [IX_Tbl51Infrafamilies_InfrafamilyName] UNIQUE NONCLUSTERED 
(
	[InfrafamilyName] ASC,
	[SubfamilyID] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl54Supertribusses_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl54Supertribusses] ADD  CONSTRAINT [IX_Tbl54Supertribusses_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl54Supertribusses_SupertribusName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl54Supertribusses] ADD  CONSTRAINT [IX_Tbl54Supertribusses_SupertribusName] UNIQUE NONCLUSTERED 
(
	[SupertribusName] ASC,
	[InfrafamilyID] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl57Tribusses_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl57Tribusses] ADD  CONSTRAINT [IX_Tbl57Tribusses_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl57Tribusses_TribusName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl57Tribusses] ADD  CONSTRAINT [IX_Tbl57Tribusses_TribusName] UNIQUE NONCLUSTERED 
(
	[TribusName] ASC,
	[SupertribusID] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl60Subtribusses_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl60Subtribusses] ADD  CONSTRAINT [IX_Tbl60Subtribusses_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl60Subtribusses_SubtribusName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl60Subtribusses] ADD  CONSTRAINT [IX_Tbl60Subtribusses_SubtribusName] UNIQUE NONCLUSTERED 
(
	[SubtribusName] ASC,
	[TribusID] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl63Infratribusses_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl63Infratribusses] ADD  CONSTRAINT [IX_Tbl63Infratribusses_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl63Infratribusses_InfratribusName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl63Infratribusses] ADD  CONSTRAINT [IX_Tbl63Infratribusses_InfratribusName] UNIQUE NONCLUSTERED 
(
	[InfratribusName] ASC,
	[SubtribusID] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl66Genusses_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl66Genusses] ADD  CONSTRAINT [IX_Tbl66Genusses_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl66Genusses_GenusName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl66Genusses] ADD  CONSTRAINT [IX_Tbl66Genusses_GenusName] UNIQUE NONCLUSTERED 
(
	[GenusName] ASC,
	[InfratribusID] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl68Speciesgroups_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl68Speciesgroups] ADD  CONSTRAINT [IX_Tbl68Speciesgroups_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl68Speciesgroups_SpeciesgroupName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl68Speciesgroups] ADD  CONSTRAINT [IX_Tbl68Speciesgroups_SpeciesgroupName] UNIQUE NONCLUSTERED 
(
	[SpeciesgroupName] ASC,
	[Subspeciesgroup] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl69FiSpeciesses_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl69FiSpeciesses] ADD  CONSTRAINT [IX_Tbl69FiSpeciesses_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl69FiSpeciesses_FiSpeciesName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl69FiSpeciesses] ADD  CONSTRAINT [IX_Tbl69FiSpeciesses_FiSpeciesName] UNIQUE NONCLUSTERED 
(
	[GenusID] ASC,
	[FiSpeciesName] ASC,
	[Subspecies] ASC,
	[Divers] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl72PlSpeciesses_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl72PlSpeciesses] ADD  CONSTRAINT [IX_Tbl72PlSpeciesses_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl72PlSpeciesses_PlSpeciesName]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl72PlSpeciesses] ADD  CONSTRAINT [IX_Tbl72PlSpeciesses_PlSpeciesName] UNIQUE NONCLUSTERED 
(
	[GenusID] ASC,
	[PlSpeciesName] ASC,
	[Subspecies] ASC,
	[Divers] ASC,
	[Author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl78Names_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl78Names] ADD  CONSTRAINT [IX_Tbl78Names_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl78Names_NameName_FKID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl78Names] ADD  CONSTRAINT [IX_Tbl78Names_NameName_FKID] UNIQUE NONCLUSTERED 
(
	[NameName] ASC,
	[FiSpeciesID] ASC,
	[PlSpeciesID] ASC,
	[Language] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl84Synonyms_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl84Synonyms] ADD  CONSTRAINT [IX_Tbl84Synonyms_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tbl84Synonyms_SynonymName_FKID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl84Synonyms] ADD  CONSTRAINT [IX_Tbl84Synonyms_SynonymName_FKID] UNIQUE NONCLUSTERED 
(
	[SynonymName] ASC,
	[Author] ASC,
	[FiSpeciesID] ASC,
	[PlSpeciesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl87Geographics_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl87Geographics] ADD  CONSTRAINT [IX_Tbl87Geographics_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl90RefAuthors_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl90RefAuthors] ADD  CONSTRAINT [IX_Tbl90RefAuthors_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl90References_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl90References] ADD  CONSTRAINT [IX_Tbl90References_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl90RefExperts_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl90RefExperts] ADD  CONSTRAINT [IX_Tbl90RefExperts_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl90RefSources_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl90RefSources] ADD  CONSTRAINT [IX_Tbl90RefSources_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tbl93Comments_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[Tbl93Comments] ADD  CONSTRAINT [IX_Tbl93Comments_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserProfiles_CountID]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[TblUserProfiles] ADD  CONSTRAINT [IX_UserProfiles_CountID] UNIQUE NONCLUSTERED 
(
	[CountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserProfiles_Name]    Script Date: 14.09.2020 17:13:00 ******/
ALTER TABLE [dbo].[TblUserProfiles] ADD  CONSTRAINT [IX_UserProfiles_Name] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Tbl03Regnums] ADD  CONSTRAINT [DF_Tbl03Regnums_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl06Phylums] ADD  CONSTRAINT [DF_Tbl06Phylums_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl09Divisions] ADD  CONSTRAINT [DF_Tbl09Divisions_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl12Subphylums] ADD  CONSTRAINT [DF_Tbl12Subphylums_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl15Subdivisions] ADD  CONSTRAINT [DF_Tbl15Subdivisions_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl18Superclasses] ADD  CONSTRAINT [DF_Tbl18Superclasses_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl21Classes] ADD  CONSTRAINT [DF_Tbl21Classes_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl24Subclasses] ADD  CONSTRAINT [DF_Tbl24Subclasses_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl27Infraclasses] ADD  CONSTRAINT [DF_Tbl27Infraclasses_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl30Legios] ADD  CONSTRAINT [DF_Tbl30Legios_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl33Ordos] ADD  CONSTRAINT [DF_Tbl33Ordos_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl36Subordos] ADD  CONSTRAINT [DF_Tbl36Subordos_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl39Infraordos] ADD  CONSTRAINT [DF_Tbl39Infraordos_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl42Superfamilies] ADD  CONSTRAINT [DF_Tbl42Superfamilies_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl45Families] ADD  CONSTRAINT [DF_Tbl45Families_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl48Subfamilies] ADD  CONSTRAINT [DF_Tbl48Subfamilies_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl51Infrafamilies] ADD  CONSTRAINT [DF_Tbl51Infrafamilies_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl54Supertribusses] ADD  CONSTRAINT [DF_Tbl54Supertribusses_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl57Tribusses] ADD  CONSTRAINT [DF_Tbl57Tribusses_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl60Subtribusses] ADD  CONSTRAINT [DF_Tbl60Subtribusses_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl63Infratribusses] ADD  CONSTRAINT [DF_Tbl63Infratribusses_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl66Genusses] ADD  CONSTRAINT [DF_Tbl66Genusses_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl68Speciesgroups] ADD  CONSTRAINT [DF_Tbl68Speciesgroups_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl69FiSpeciesses] ADD  CONSTRAINT [DF_Tbl69FiSpeciesses_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl72PlSpeciesses] ADD  CONSTRAINT [DF_Tbl72PlSpeciesses_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl78Names] ADD  CONSTRAINT [DF_Tbl78Names_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl81Images] ADD  CONSTRAINT [DF_Tbl81Images_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl84Synonyms] ADD  CONSTRAINT [DF_Tbl84Synonyms_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl87Geographics] ADD  CONSTRAINT [DF_Tbl87Geographics_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl90RefAuthors] ADD  CONSTRAINT [DF_Tbl90RefAuthors_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl90References] ADD  CONSTRAINT [DF_Tbl90References_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl90RefExperts] ADD  CONSTRAINT [DF_Tbl90RefExperts_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl90RefSources] ADD  CONSTRAINT [DF_Tbl90RefSources_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl93Comments] ADD  CONSTRAINT [DF_Tbl93Comments_Valid]  DEFAULT ((0)) FOR [Valid]
GO
ALTER TABLE [dbo].[Tbl06Phylums]  WITH CHECK ADD  CONSTRAINT [FK_Tbl06Phylums_Tbl03Regnums] FOREIGN KEY([RegnumID])
REFERENCES [dbo].[Tbl03Regnums] ([RegnumID])
GO
ALTER TABLE [dbo].[Tbl06Phylums] CHECK CONSTRAINT [FK_Tbl06Phylums_Tbl03Regnums]
GO
ALTER TABLE [dbo].[Tbl09Divisions]  WITH CHECK ADD  CONSTRAINT [FK_Tbl09Divisions_Tbl03Regnums] FOREIGN KEY([RegnumID])
REFERENCES [dbo].[Tbl03Regnums] ([RegnumID])
GO
ALTER TABLE [dbo].[Tbl09Divisions] CHECK CONSTRAINT [FK_Tbl09Divisions_Tbl03Regnums]
GO
ALTER TABLE [dbo].[Tbl12Subphylums]  WITH CHECK ADD  CONSTRAINT [FK_Tbl12Subphylums_Tbl06Phylums] FOREIGN KEY([PhylumID])
REFERENCES [dbo].[Tbl06Phylums] ([PhylumID])
GO
ALTER TABLE [dbo].[Tbl12Subphylums] CHECK CONSTRAINT [FK_Tbl12Subphylums_Tbl06Phylums]
GO
ALTER TABLE [dbo].[Tbl15Subdivisions]  WITH CHECK ADD  CONSTRAINT [FK_Tbl15Subdivisions_Tbl09Divisions] FOREIGN KEY([DivisionID])
REFERENCES [dbo].[Tbl09Divisions] ([DivisionID])
GO
ALTER TABLE [dbo].[Tbl15Subdivisions] CHECK CONSTRAINT [FK_Tbl15Subdivisions_Tbl09Divisions]
GO
ALTER TABLE [dbo].[Tbl18Superclasses]  WITH CHECK ADD  CONSTRAINT [FK_Tbl18Superclasses_Tbl12Subphylums] FOREIGN KEY([SubphylumID])
REFERENCES [dbo].[Tbl12Subphylums] ([SubphylumID])
GO
ALTER TABLE [dbo].[Tbl18Superclasses] CHECK CONSTRAINT [FK_Tbl18Superclasses_Tbl12Subphylums]
GO
ALTER TABLE [dbo].[Tbl18Superclasses]  WITH CHECK ADD  CONSTRAINT [FK_Tbl18Superclasses_Tbl15Subdivisions] FOREIGN KEY([SubdivisionID])
REFERENCES [dbo].[Tbl15Subdivisions] ([SubdivisionID])
GO
ALTER TABLE [dbo].[Tbl18Superclasses] CHECK CONSTRAINT [FK_Tbl18Superclasses_Tbl15Subdivisions]
GO
ALTER TABLE [dbo].[Tbl21Classes]  WITH CHECK ADD  CONSTRAINT [FK_Tbl21Classes_Tbl18Superclasses] FOREIGN KEY([SuperclassID])
REFERENCES [dbo].[Tbl18Superclasses] ([SuperclassID])
GO
ALTER TABLE [dbo].[Tbl21Classes] CHECK CONSTRAINT [FK_Tbl21Classes_Tbl18Superclasses]
GO
ALTER TABLE [dbo].[Tbl24Subclasses]  WITH CHECK ADD  CONSTRAINT [FK_Tbl24Subclasses_Tbl21Classes] FOREIGN KEY([ClassID])
REFERENCES [dbo].[Tbl21Classes] ([ClassID])
GO
ALTER TABLE [dbo].[Tbl24Subclasses] CHECK CONSTRAINT [FK_Tbl24Subclasses_Tbl21Classes]
GO
ALTER TABLE [dbo].[Tbl27Infraclasses]  WITH CHECK ADD  CONSTRAINT [FK_Tbl27Infraclasses_Tbl24Sublasses] FOREIGN KEY([SubclassID])
REFERENCES [dbo].[Tbl24Subclasses] ([SubclassID])
GO
ALTER TABLE [dbo].[Tbl27Infraclasses] CHECK CONSTRAINT [FK_Tbl27Infraclasses_Tbl24Sublasses]
GO
ALTER TABLE [dbo].[Tbl30Legios]  WITH CHECK ADD  CONSTRAINT [FK_Tbl30Legios_Tbl27Infraclasses] FOREIGN KEY([InfraclassID])
REFERENCES [dbo].[Tbl27Infraclasses] ([InfraclassID])
GO
ALTER TABLE [dbo].[Tbl30Legios] CHECK CONSTRAINT [FK_Tbl30Legios_Tbl27Infraclasses]
GO
ALTER TABLE [dbo].[Tbl33Ordos]  WITH CHECK ADD  CONSTRAINT [FK_Tbl33Ordos_Tbl30Legios] FOREIGN KEY([LegioID])
REFERENCES [dbo].[Tbl30Legios] ([LegioID])
GO
ALTER TABLE [dbo].[Tbl33Ordos] CHECK CONSTRAINT [FK_Tbl33Ordos_Tbl30Legios]
GO
ALTER TABLE [dbo].[Tbl36Subordos]  WITH CHECK ADD  CONSTRAINT [FK_Tbl36Subordos_Tbl33Ordos] FOREIGN KEY([OrdoID])
REFERENCES [dbo].[Tbl33Ordos] ([OrdoID])
GO
ALTER TABLE [dbo].[Tbl36Subordos] CHECK CONSTRAINT [FK_Tbl36Subordos_Tbl33Ordos]
GO
ALTER TABLE [dbo].[Tbl39Infraordos]  WITH CHECK ADD  CONSTRAINT [FK_Tbl39Infraordos_Tbl36Subordos] FOREIGN KEY([SubordoID])
REFERENCES [dbo].[Tbl36Subordos] ([SubordoID])
GO
ALTER TABLE [dbo].[Tbl39Infraordos] CHECK CONSTRAINT [FK_Tbl39Infraordos_Tbl36Subordos]
GO
ALTER TABLE [dbo].[Tbl42Superfamilies]  WITH CHECK ADD  CONSTRAINT [FK_Tbl42Superfamilies_Tbl39Infraordos] FOREIGN KEY([InfraordoID])
REFERENCES [dbo].[Tbl39Infraordos] ([InfraordoID])
GO
ALTER TABLE [dbo].[Tbl42Superfamilies] CHECK CONSTRAINT [FK_Tbl42Superfamilies_Tbl39Infraordos]
GO
ALTER TABLE [dbo].[Tbl45Families]  WITH CHECK ADD  CONSTRAINT [FK_Tbl45Families_Tbl42Superfamilies] FOREIGN KEY([SuperfamilyID])
REFERENCES [dbo].[Tbl42Superfamilies] ([SuperfamilyID])
GO
ALTER TABLE [dbo].[Tbl45Families] CHECK CONSTRAINT [FK_Tbl45Families_Tbl42Superfamilies]
GO
ALTER TABLE [dbo].[Tbl48Subfamilies]  WITH CHECK ADD  CONSTRAINT [FK_Tbl48Subfamilies_Tbl45Families] FOREIGN KEY([FamilyID])
REFERENCES [dbo].[Tbl45Families] ([FamilyID])
GO
ALTER TABLE [dbo].[Tbl48Subfamilies] CHECK CONSTRAINT [FK_Tbl48Subfamilies_Tbl45Families]
GO
ALTER TABLE [dbo].[Tbl51Infrafamilies]  WITH CHECK ADD  CONSTRAINT [FK_Tbl51Infrafamilies_Tbl48Subfamilies] FOREIGN KEY([SubfamilyID])
REFERENCES [dbo].[Tbl48Subfamilies] ([SubfamilyID])
GO
ALTER TABLE [dbo].[Tbl51Infrafamilies] CHECK CONSTRAINT [FK_Tbl51Infrafamilies_Tbl48Subfamilies]
GO
ALTER TABLE [dbo].[Tbl54Supertribusses]  WITH CHECK ADD  CONSTRAINT [FK_Tbl54Supertribusses_Tbl51Infrafamilies] FOREIGN KEY([InfrafamilyID])
REFERENCES [dbo].[Tbl51Infrafamilies] ([InfrafamilyID])
GO
ALTER TABLE [dbo].[Tbl54Supertribusses] CHECK CONSTRAINT [FK_Tbl54Supertribusses_Tbl51Infrafamilies]
GO
ALTER TABLE [dbo].[Tbl57Tribusses]  WITH CHECK ADD  CONSTRAINT [FK_Tbl57Tribusses_Tbl54Supertribusses] FOREIGN KEY([SupertribusID])
REFERENCES [dbo].[Tbl54Supertribusses] ([SupertribusID])
GO
ALTER TABLE [dbo].[Tbl57Tribusses] CHECK CONSTRAINT [FK_Tbl57Tribusses_Tbl54Supertribusses]
GO
ALTER TABLE [dbo].[Tbl60Subtribusses]  WITH CHECK ADD  CONSTRAINT [FK_Tbl60Subtribusses_Tbl57Tribusses] FOREIGN KEY([TribusID])
REFERENCES [dbo].[Tbl57Tribusses] ([TribusID])
GO
ALTER TABLE [dbo].[Tbl60Subtribusses] CHECK CONSTRAINT [FK_Tbl60Subtribusses_Tbl57Tribusses]
GO
ALTER TABLE [dbo].[Tbl63Infratribusses]  WITH CHECK ADD  CONSTRAINT [FK_Tbl63Infratribusses_Tbl60Subtribusses] FOREIGN KEY([SubtribusID])
REFERENCES [dbo].[Tbl60Subtribusses] ([SubtribusID])
GO
ALTER TABLE [dbo].[Tbl63Infratribusses] CHECK CONSTRAINT [FK_Tbl63Infratribusses_Tbl60Subtribusses]
GO
ALTER TABLE [dbo].[Tbl66Genusses]  WITH CHECK ADD  CONSTRAINT [FK_Tbl66Genusses_Tbl63Infratribusses] FOREIGN KEY([InfratribusID])
REFERENCES [dbo].[Tbl63Infratribusses] ([InfratribusID])
GO
ALTER TABLE [dbo].[Tbl66Genusses] CHECK CONSTRAINT [FK_Tbl66Genusses_Tbl63Infratribusses]
GO
ALTER TABLE [dbo].[Tbl69FiSpeciesses]  WITH CHECK ADD  CONSTRAINT [FK_Tbl69FiSpeciesses_Tbl66Genusses] FOREIGN KEY([GenusID])
REFERENCES [dbo].[Tbl66Genusses] ([GenusID])
GO
ALTER TABLE [dbo].[Tbl69FiSpeciesses] CHECK CONSTRAINT [FK_Tbl69FiSpeciesses_Tbl66Genusses]
GO
ALTER TABLE [dbo].[Tbl69FiSpeciesses]  WITH CHECK ADD  CONSTRAINT [FK_Tbl69FiSpeciesses_Tbl68Speciesgroups] FOREIGN KEY([SpeciesgroupID])
REFERENCES [dbo].[Tbl68Speciesgroups] ([SpeciesgroupID])
GO
ALTER TABLE [dbo].[Tbl69FiSpeciesses] CHECK CONSTRAINT [FK_Tbl69FiSpeciesses_Tbl68Speciesgroups]
GO
ALTER TABLE [dbo].[Tbl72PlSpeciesses]  WITH CHECK ADD  CONSTRAINT [FK_Tbl72PlSpeciesses_Tbl66Genusses] FOREIGN KEY([GenusID])
REFERENCES [dbo].[Tbl66Genusses] ([GenusID])
GO
ALTER TABLE [dbo].[Tbl72PlSpeciesses] CHECK CONSTRAINT [FK_Tbl72PlSpeciesses_Tbl66Genusses]
GO
ALTER TABLE [dbo].[Tbl72PlSpeciesses]  WITH CHECK ADD  CONSTRAINT [FK_Tbl72PlSpeciesses_Tbl68Speciesgroups] FOREIGN KEY([SpeciesgroupID])
REFERENCES [dbo].[Tbl68Speciesgroups] ([SpeciesgroupID])
GO
ALTER TABLE [dbo].[Tbl72PlSpeciesses] CHECK CONSTRAINT [FK_Tbl72PlSpeciesses_Tbl68Speciesgroups]
GO
ALTER TABLE [dbo].[Tbl78Names]  WITH CHECK ADD  CONSTRAINT [FK_Tbl78Names_Tbl69FiSpeciesses] FOREIGN KEY([FiSpeciesID])
REFERENCES [dbo].[Tbl69FiSpeciesses] ([FiSpeciesID])
GO
ALTER TABLE [dbo].[Tbl78Names] CHECK CONSTRAINT [FK_Tbl78Names_Tbl69FiSpeciesses]
GO
ALTER TABLE [dbo].[Tbl78Names]  WITH CHECK ADD  CONSTRAINT [FK_Tbl78Names_Tbl72PlSpeciesses] FOREIGN KEY([PlSpeciesID])
REFERENCES [dbo].[Tbl72PlSpeciesses] ([PlSpeciesID])
GO
ALTER TABLE [dbo].[Tbl78Names] CHECK CONSTRAINT [FK_Tbl78Names_Tbl72PlSpeciesses]
GO
ALTER TABLE [dbo].[Tbl81Images]  WITH CHECK ADD  CONSTRAINT [FK_Tbl81Images_Tbl69FiSpeciesses] FOREIGN KEY([FiSpeciesID])
REFERENCES [dbo].[Tbl69FiSpeciesses] ([FiSpeciesID])
GO
ALTER TABLE [dbo].[Tbl81Images] CHECK CONSTRAINT [FK_Tbl81Images_Tbl69FiSpeciesses]
GO
ALTER TABLE [dbo].[Tbl81Images]  WITH CHECK ADD  CONSTRAINT [FK_Tbl81Images_Tbl72PlSpeciesses] FOREIGN KEY([PlSpeciesID])
REFERENCES [dbo].[Tbl72PlSpeciesses] ([PlSpeciesID])
GO
ALTER TABLE [dbo].[Tbl81Images] CHECK CONSTRAINT [FK_Tbl81Images_Tbl72PlSpeciesses]
GO
ALTER TABLE [dbo].[Tbl84Synonyms]  WITH CHECK ADD  CONSTRAINT [FK_Tbl84Synonyms_Tbl69FiSpeciesses] FOREIGN KEY([FiSpeciesID])
REFERENCES [dbo].[Tbl69FiSpeciesses] ([FiSpeciesID])
GO
ALTER TABLE [dbo].[Tbl84Synonyms] CHECK CONSTRAINT [FK_Tbl84Synonyms_Tbl69FiSpeciesses]
GO
ALTER TABLE [dbo].[Tbl84Synonyms]  WITH CHECK ADD  CONSTRAINT [FK_Tbl84Synonyms_Tbl72PlSpeciesses] FOREIGN KEY([PlSpeciesID])
REFERENCES [dbo].[Tbl72PlSpeciesses] ([PlSpeciesID])
GO
ALTER TABLE [dbo].[Tbl84Synonyms] CHECK CONSTRAINT [FK_Tbl84Synonyms_Tbl72PlSpeciesses]
GO
ALTER TABLE [dbo].[Tbl87Geographics]  WITH CHECK ADD  CONSTRAINT [FK_Tbl87Geographics_Tbl69FiSpeciesses] FOREIGN KEY([FiSpeciesID])
REFERENCES [dbo].[Tbl69FiSpeciesses] ([FiSpeciesID])
GO
ALTER TABLE [dbo].[Tbl87Geographics] CHECK CONSTRAINT [FK_Tbl87Geographics_Tbl69FiSpeciesses]
GO
ALTER TABLE [dbo].[Tbl87Geographics]  WITH CHECK ADD  CONSTRAINT [FK_Tbl87Geographics_Tbl72PlSpeciesses] FOREIGN KEY([PlSpeciesID])
REFERENCES [dbo].[Tbl72PlSpeciesses] ([PlSpeciesID])
GO
ALTER TABLE [dbo].[Tbl87Geographics] CHECK CONSTRAINT [FK_Tbl87Geographics_Tbl72PlSpeciesses]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl03Regnums] FOREIGN KEY([RegnumID])
REFERENCES [dbo].[Tbl03Regnums] ([RegnumID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl03Regnums]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl06Phylums] FOREIGN KEY([PhylumID])
REFERENCES [dbo].[Tbl06Phylums] ([PhylumID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl06Phylums]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl09Divisions] FOREIGN KEY([DivisionID])
REFERENCES [dbo].[Tbl09Divisions] ([DivisionID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl09Divisions]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl12Subphylums] FOREIGN KEY([SubphylumID])
REFERENCES [dbo].[Tbl12Subphylums] ([SubphylumID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl12Subphylums]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl15Subdivisions] FOREIGN KEY([SubdivisionID])
REFERENCES [dbo].[Tbl15Subdivisions] ([SubdivisionID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl15Subdivisions]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl18Superclasses] FOREIGN KEY([SuperclassID])
REFERENCES [dbo].[Tbl18Superclasses] ([SuperclassID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl18Superclasses]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl21Classes] FOREIGN KEY([ClassID])
REFERENCES [dbo].[Tbl21Classes] ([ClassID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl21Classes]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl24Subclasses] FOREIGN KEY([SubclassID])
REFERENCES [dbo].[Tbl24Subclasses] ([SubclassID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl24Subclasses]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl27Infraclasses] FOREIGN KEY([InfraclassID])
REFERENCES [dbo].[Tbl27Infraclasses] ([InfraclassID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl27Infraclasses]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl30Legios] FOREIGN KEY([LegioID])
REFERENCES [dbo].[Tbl30Legios] ([LegioID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl30Legios]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl33Ordos] FOREIGN KEY([OrdoID])
REFERENCES [dbo].[Tbl33Ordos] ([OrdoID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl33Ordos]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl36Subordos] FOREIGN KEY([SubordoID])
REFERENCES [dbo].[Tbl36Subordos] ([SubordoID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl36Subordos]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl39Infraordos] FOREIGN KEY([InfraordoID])
REFERENCES [dbo].[Tbl39Infraordos] ([InfraordoID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl39Infraordos]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl42Superfamilies] FOREIGN KEY([SuperfamilyID])
REFERENCES [dbo].[Tbl42Superfamilies] ([SuperfamilyID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl42Superfamilies]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl45Families] FOREIGN KEY([FamilyID])
REFERENCES [dbo].[Tbl45Families] ([FamilyID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl45Families]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl48Subfamilies] FOREIGN KEY([SubfamilyID])
REFERENCES [dbo].[Tbl48Subfamilies] ([SubfamilyID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl48Subfamilies]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl51Infrafamilies] FOREIGN KEY([InfrafamilyID])
REFERENCES [dbo].[Tbl51Infrafamilies] ([InfrafamilyID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl51Infrafamilies]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl54Supertribusses] FOREIGN KEY([SupertribusID])
REFERENCES [dbo].[Tbl54Supertribusses] ([SupertribusID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl54Supertribusses]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl57Tribusses] FOREIGN KEY([TribusID])
REFERENCES [dbo].[Tbl57Tribusses] ([TribusID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl57Tribusses]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl60Subtribusses] FOREIGN KEY([SubtribusID])
REFERENCES [dbo].[Tbl60Subtribusses] ([SubtribusID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl60Subtribusses]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl63Infratribusses] FOREIGN KEY([InfratribusID])
REFERENCES [dbo].[Tbl63Infratribusses] ([InfratribusID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl63Infratribusses]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl66Genusses] FOREIGN KEY([GenusID])
REFERENCES [dbo].[Tbl66Genusses] ([GenusID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl66Genusses]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl69FiSpeciesses] FOREIGN KEY([FiSpeciesID])
REFERENCES [dbo].[Tbl69FiSpeciesses] ([FiSpeciesID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl69FiSpeciesses]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl72PlSpeciesses] FOREIGN KEY([PlSpeciesID])
REFERENCES [dbo].[Tbl72PlSpeciesses] ([PlSpeciesID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl72PlSpeciesses]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl90RefAuthors] FOREIGN KEY([RefAuthorID])
REFERENCES [dbo].[Tbl90RefAuthors] ([RefAuthorID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl90RefAuthors]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl90RefExperts] FOREIGN KEY([RefExpertID])
REFERENCES [dbo].[Tbl90RefExperts] ([RefExpertID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl90RefExperts]
GO
ALTER TABLE [dbo].[Tbl90References]  WITH CHECK ADD  CONSTRAINT [FK_Tbl90References_Tbl90RefSources] FOREIGN KEY([RefSourceID])
REFERENCES [dbo].[Tbl90RefSources] ([RefSourceID])
GO
ALTER TABLE [dbo].[Tbl90References] CHECK CONSTRAINT [FK_Tbl90References_Tbl90RefSources]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl03Regnums] FOREIGN KEY([RegnumID])
REFERENCES [dbo].[Tbl03Regnums] ([RegnumID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl03Regnums]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl06Phylums] FOREIGN KEY([PhylumID])
REFERENCES [dbo].[Tbl06Phylums] ([PhylumID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl06Phylums]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl09Divisions] FOREIGN KEY([DivisionID])
REFERENCES [dbo].[Tbl09Divisions] ([DivisionID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl09Divisions]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl12Subphylums] FOREIGN KEY([SubphylumID])
REFERENCES [dbo].[Tbl12Subphylums] ([SubphylumID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl12Subphylums]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl15Subdivisions] FOREIGN KEY([SubdivisionID])
REFERENCES [dbo].[Tbl15Subdivisions] ([SubdivisionID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl15Subdivisions]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl18Superclasses] FOREIGN KEY([SuperclassID])
REFERENCES [dbo].[Tbl18Superclasses] ([SuperclassID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl18Superclasses]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl21Classes] FOREIGN KEY([ClassID])
REFERENCES [dbo].[Tbl21Classes] ([ClassID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl21Classes]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl24Subclasses] FOREIGN KEY([SubclassID])
REFERENCES [dbo].[Tbl24Subclasses] ([SubclassID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl24Subclasses]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl27Infraclasses] FOREIGN KEY([InfraclassID])
REFERENCES [dbo].[Tbl27Infraclasses] ([InfraclassID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl27Infraclasses]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl30Legios] FOREIGN KEY([LegioID])
REFERENCES [dbo].[Tbl30Legios] ([LegioID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl30Legios]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl33Ordos] FOREIGN KEY([OrdoID])
REFERENCES [dbo].[Tbl33Ordos] ([OrdoID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl33Ordos]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl36Subordos] FOREIGN KEY([SubordoID])
REFERENCES [dbo].[Tbl36Subordos] ([SubordoID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl36Subordos]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl39Infraordos] FOREIGN KEY([InfraordoID])
REFERENCES [dbo].[Tbl39Infraordos] ([InfraordoID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl39Infraordos]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl42Superfamilies] FOREIGN KEY([SuperfamilyID])
REFERENCES [dbo].[Tbl42Superfamilies] ([SuperfamilyID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl42Superfamilies]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl45Families] FOREIGN KEY([FamilyID])
REFERENCES [dbo].[Tbl45Families] ([FamilyID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl45Families]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl48Subfamilies] FOREIGN KEY([SubfamilyID])
REFERENCES [dbo].[Tbl48Subfamilies] ([SubfamilyID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl48Subfamilies]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl51Infrafamilies] FOREIGN KEY([InfrafamilyID])
REFERENCES [dbo].[Tbl51Infrafamilies] ([InfrafamilyID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl51Infrafamilies]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl54Supertribusses] FOREIGN KEY([SupertribusID])
REFERENCES [dbo].[Tbl54Supertribusses] ([SupertribusID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl54Supertribusses]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl57Tribusses] FOREIGN KEY([TribusID])
REFERENCES [dbo].[Tbl57Tribusses] ([TribusID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl57Tribusses]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl60Subtribusses] FOREIGN KEY([SubtribusID])
REFERENCES [dbo].[Tbl60Subtribusses] ([SubtribusID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl60Subtribusses]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl63Infratribusses] FOREIGN KEY([InfratribusID])
REFERENCES [dbo].[Tbl63Infratribusses] ([InfratribusID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl63Infratribusses]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl66Genusses] FOREIGN KEY([GenusID])
REFERENCES [dbo].[Tbl66Genusses] ([GenusID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl66Genusses]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl69FiSpeciesses] FOREIGN KEY([FiSpeciesID])
REFERENCES [dbo].[Tbl69FiSpeciesses] ([FiSpeciesID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl69FiSpeciesses]
GO
ALTER TABLE [dbo].[Tbl93Comments]  WITH CHECK ADD  CONSTRAINT [FK_Tbl93Comments_Tbl72PlSpeciesses] FOREIGN KEY([PlSpeciesID])
REFERENCES [dbo].[Tbl72PlSpeciesses] ([PlSpeciesID])
GO
ALTER TABLE [dbo].[Tbl93Comments] CHECK CONSTRAINT [FK_Tbl93Comments_Tbl72PlSpeciesses]
GO
USE [master]
GO
ALTER DATABASE [ATIS35] SET  READ_WRITE 
GO
