USE [master]
GO
/****** Object:  Database [OT_Assessment_DB]    Script Date: 2024/09/13 10:57:02 ******/
CREATE DATABASE [OT_Assessment_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OT_Assessment_DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\OT_Assessment_DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OT_Assessment_DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\OT_Assessment_DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [OT_Assessment_DB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OT_Assessment_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OT_Assessment_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OT_Assessment_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OT_Assessment_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OT_Assessment_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OT_Assessment_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [OT_Assessment_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OT_Assessment_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OT_Assessment_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OT_Assessment_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OT_Assessment_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OT_Assessment_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OT_Assessment_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OT_Assessment_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OT_Assessment_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OT_Assessment_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OT_Assessment_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OT_Assessment_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OT_Assessment_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OT_Assessment_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OT_Assessment_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OT_Assessment_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OT_Assessment_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OT_Assessment_DB] SET RECOVERY FULL 
GO
ALTER DATABASE [OT_Assessment_DB] SET  MULTI_USER 
GO
ALTER DATABASE [OT_Assessment_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OT_Assessment_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OT_Assessment_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OT_Assessment_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OT_Assessment_DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OT_Assessment_DB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'OT_Assessment_DB', N'ON'
GO
ALTER DATABASE [OT_Assessment_DB] SET QUERY_STORE = ON
GO
ALTER DATABASE [OT_Assessment_DB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [OT_Assessment_DB]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 2024/09/13 10:57:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountID] [uniqueidentifier] NOT NULL,
	[AccountNumber] [varchar](50) NULL,
	[Username] [varchar](50) NULL,
	[FirstName] [varchar](100) NULL,
	[Surname] [varchar](100) NULL,
	[Email] [varchar](150) NULL,
	[Gender] [varchar](10) NULL,
	[PhysicalAddress1] [varchar](150) NULL,
	[PhysicalAddress2] [varchar](150) NULL,
	[PhysicalAddress3] [varchar](150) NULL,
	[PhysicalCode] [varchar](50) NULL,
	[PostalAddress1] [varchar](150) NULL,
	[PostalAddress2] [varchar](150) NULL,
	[PostalAddress3] [varchar](150) NULL,
	[PostalCode] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 2024/09/13 10:57:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[CountryID] [uniqueidentifier] NOT NULL,
	[CountryCode] [varchar](10) NULL,
	[CountryName] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Games]    Script Date: 2024/09/13 10:57:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Games](
	[GameId] [uniqueidentifier] NOT NULL,
	[GameName] [varchar](50) NULL,
	[GameDescription] [varchar](255) NULL,
	[Theme] [varchar](100) NULL,
	[ProviderID] [uniqueidentifier] NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_Games] PRIMARY KEY CLUSTERED 
(
	[GameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Providers]    Script Date: 2024/09/13 10:57:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Providers](
	[ProviderID] [uniqueidentifier] NOT NULL,
	[ProviderName] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProviderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sessions]    Script Date: 2024/09/13 10:57:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sessions](
	[SessionId] [uniqueidentifier] NOT NULL,
	[SessionData] [varchar](max) NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_Sessions] PRIMARY KEY CLUSTERED 
(
	[SessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 2024/09/13 10:57:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[TransactionId] [uniqueidentifier] NOT NULL,
	[TransactionTypeId] [uniqueidentifier] NULL,
	[TransactionDate] [datetime] NULL,
	[TransactionAmount] [decimal](10, 2) NULL,
	[AccountId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionsType]    Script Date: 2024/09/13 10:57:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionsType](
	[TransactionTypeId] [uniqueidentifier] NOT NULL,
	[TransactionTypeCode] [varchar](10) NULL,
	[TransactionTypeDescription] [varchar](255) NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_TransactionsType] PRIMARY KEY CLUSTERED 
(
	[TransactionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wagers]    Script Date: 2024/09/13 10:57:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wagers](
	[WagerId] [uniqueidentifier] NOT NULL,
	[GameId] [uniqueidentifier] NOT NULL,
	[TransactionId] [uniqueidentifier] NULL,
	[AccountId] [uniqueidentifier] NULL,
	[ExternalReferenceId] [uniqueidentifier] NULL,
	[TransactionTypeId] [uniqueidentifier] NULL,
	[SessionId] [uniqueidentifier] NULL,
	[Amount] [decimal](10, 2) NULL,
	[NumberOfBets] [int] NULL,
	[CountryCode] [varchar](10) NULL,
	[Duration] [bigint] NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_Wagers] PRIMARY KEY CLUSTERED 
(
	[WagerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [DF_Accounts_AccountID]  DEFAULT (newid()) FOR [AccountID]
GO
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [DF_Accounts_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Countries] ADD  CONSTRAINT [DF_Countries_CountryID]  DEFAULT (newid()) FOR [CountryID]
GO
ALTER TABLE [dbo].[Games] ADD  CONSTRAINT [DF_Games_GameId]  DEFAULT (newid()) FOR [GameId]
GO
ALTER TABLE [dbo].[Games] ADD  CONSTRAINT [DF_Games_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Providers] ADD  DEFAULT (newid()) FOR [ProviderID]
GO
ALTER TABLE [dbo].[Providers] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Sessions] ADD  CONSTRAINT [DF_Sessions_SessionId]  DEFAULT (newid()) FOR [SessionId]
GO
ALTER TABLE [dbo].[Sessions] ADD  CONSTRAINT [DF_Sessions_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Transactions_TransactionId]  DEFAULT (newid()) FOR [TransactionId]
GO
ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Transactions_TransactionDate]  DEFAULT (getdate()) FOR [TransactionDate]
GO
ALTER TABLE [dbo].[TransactionsType] ADD  CONSTRAINT [DF_TransactionsType_TransactionTypeId]  DEFAULT (newid()) FOR [TransactionTypeId]
GO
ALTER TABLE [dbo].[TransactionsType] ADD  CONSTRAINT [DF_TransactionsType_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Wagers] ADD  CONSTRAINT [DF_Wagers_WagerId]  DEFAULT (newid()) FOR [WagerId]
GO
ALTER TABLE [dbo].[Wagers] ADD  CONSTRAINT [DF_Wagers_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Games]  WITH CHECK ADD  CONSTRAINT [FK_Games_Provider] FOREIGN KEY([ProviderID])
REFERENCES [dbo].[Providers] ([ProviderID])
GO
ALTER TABLE [dbo].[Games] CHECK CONSTRAINT [FK_Games_Provider]
GO
ALTER TABLE [dbo].[Wagers]  WITH CHECK ADD  CONSTRAINT [FK_Wagers_Accounts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([AccountID])
GO
ALTER TABLE [dbo].[Wagers] CHECK CONSTRAINT [FK_Wagers_Accounts]
GO
ALTER TABLE [dbo].[Wagers]  WITH CHECK ADD  CONSTRAINT [FK_Wagers_Games] FOREIGN KEY([GameId])
REFERENCES [dbo].[Games] ([GameId])
GO
ALTER TABLE [dbo].[Wagers] CHECK CONSTRAINT [FK_Wagers_Games]
GO
ALTER TABLE [dbo].[Wagers]  WITH CHECK ADD  CONSTRAINT [FK_Wagers_Sessions] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Sessions] ([SessionId])
GO
ALTER TABLE [dbo].[Wagers] CHECK CONSTRAINT [FK_Wagers_Sessions]
GO
ALTER TABLE [dbo].[Wagers]  WITH CHECK ADD  CONSTRAINT [FK_Wagers_Transactions] FOREIGN KEY([TransactionTypeId])
REFERENCES [dbo].[TransactionsType] ([TransactionTypeId])
GO
ALTER TABLE [dbo].[Wagers] CHECK CONSTRAINT [FK_Wagers_Transactions]
GO
ALTER TABLE [dbo].[Wagers]  WITH CHECK ADD  CONSTRAINT [FK_Wagers_TransactionsType] FOREIGN KEY([TransactionTypeId])
REFERENCES [dbo].[TransactionsType] ([TransactionTypeId])
GO
ALTER TABLE [dbo].[Wagers] CHECK CONSTRAINT [FK_Wagers_TransactionsType]
GO
USE [master]
GO
ALTER DATABASE [OT_Assessment_DB] SET  READ_WRITE 
GO
