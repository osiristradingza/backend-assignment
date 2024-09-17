USE [master]
GO
/****** Object:  Database [OT_Assessment_DB]    Script Date: 2024/09/17 10:19:04 ******/
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
/****** Object:  Table [dbo].[Accounts]    Script Date: 2024/09/17 10:19:04 ******/
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
	[Active] [bit] NULL,
	[BrandId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 2024/09/17 10:19:04 ******/
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
/****** Object:  Table [dbo].[Games]    Script Date: 2024/09/17 10:19:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Games](
	[GameId] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NULL,
	[Description] [varchar](255) NULL,
	[Theme] [varchar](100) NULL,
	[ProviderID] [uniqueidentifier] NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_Games] PRIMARY KEY CLUSTERED 
(
	[GameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Providers]    Script Date: 2024/09/17 10:19:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Providers](
	[ProviderID] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProviderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sessions]    Script Date: 2024/09/17 10:19:04 ******/
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
/****** Object:  Table [dbo].[Transactions]    Script Date: 2024/09/17 10:19:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[TransactionId] [uniqueidentifier] NOT NULL,
	[TransactionTypeId] [uniqueidentifier] NULL,
	[Date] [datetime] NULL,
	[Amount] [decimal](10, 2) NULL,
	[AccountId] [uniqueidentifier] NULL,
	[Unit] [int] NULL,
	[ExternalReferenceId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionsType]    Script Date: 2024/09/17 10:19:04 ******/
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
/****** Object:  Table [dbo].[Wagers]    Script Date: 2024/09/17 10:19:04 ******/
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
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [DF_Accounts_BrandId]  DEFAULT (newid()) FOR [BrandId]
GO
ALTER TABLE [dbo].[Countries] ADD  CONSTRAINT [DF_Countries_CountryID]  DEFAULT (newid()) FOR [CountryID]
GO
ALTER TABLE [dbo].[Countries] ADD  CONSTRAINT [DF_Countries_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
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
ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Transactions_TransactionDate]  DEFAULT (getdate()) FOR [Date]
GO
ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Transactions_ExternalReferenceId]  DEFAULT (newid()) FOR [ExternalReferenceId]
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
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_TransactionsType] FOREIGN KEY([TransactionTypeId])
REFERENCES [dbo].[TransactionsType] ([TransactionTypeId])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_TransactionsType]
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
/****** Object:  StoredProcedure [dbo].[sp_AddAccount]    Script Date: 2024/09/17 10:19:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE     PROCEDURE [dbo].[sp_AddAccount]
    @AccountID UNIQUEIDENTIFIER,
    @FirstName VARCHAR(100),
	@Surname VARCHAR(100),
	@Email VARCHAR(150),
	@Gender VARCHAR(10),
	@PhysicalAddress1 VARCHAR(150),
	@PhysicalAddress2 VARCHAR(150),
	@PhysicalAddress3 VARCHAR(150),
	@PhysicalCode VARCHAR(50),
	@PostalAddress1 VARCHAR(150),
	@PostalAddress2 VARCHAR(150),
	@PostalAddress3 VARCHAR(150),
	@PostalCode VARCHAR(50)
AS
BEGIN
    -- Start the TRY block
    BEGIN TRY
		Declare @AccountNumber varchar(150) = @Email;
		Declare @Username varchar(150) = @Email;
        -- Check if the account email already exists
        IF EXISTS (SELECT 1 FROM [dbo].[Accounts] WHERE Email = @Email)
        BEGIN
            -- If the email already exists, raise an error
            RAISERROR('An account with this email already exists.', 16, 1);
            RETURN;
        END

        -- Insert the new account if the email does not exist
        INSERT INTO [dbo].[Accounts] (AccountID, AccountNumber, Username, FirstName, Surname, Email, Gender, PhysicalAddress1, PhysicalAddress2, PhysicalAddress3, PhysicalCode, PostalAddress1, PostalAddress2, PostalAddress3, PostalCode, Active)
        VALUES (@AccountID, @AccountNumber, @Username, @FirstName, @Surname, @Email, @Gender, @PhysicalAddress1, @PhysicalAddress2, @PhysicalAddress3, @PhysicalCode, @PostalAddress1, @PostalAddress2, @PostalAddress3, @PostalCode,'1');

        -- Return the inserted AccountID
        SELECT @AccountID AS AccountID;
    END TRY

    -- Begin the CATCH block
    BEGIN CATCH
        -- Handle the error
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        -- Retrieve the error details
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Raise the error with the original details
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[sp_AddCountry]    Script Date: 2024/09/17 10:19:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create   PROCEDURE [dbo].[sp_AddCountry]
    @CountryID UNIQUEIDENTIFIER,
    @CountryCode VARCHAR(10),
	@CountryName VARCHAR(50)
AS
BEGIN
    -- Start the TRY block
    BEGIN TRY
        -- Check if the country code already exists
        IF EXISTS (SELECT 1 FROM [dbo].Countries WHERE CountryCode = @CountryCode)
        BEGIN
            -- If the code already exists, raise an error
            RAISERROR('A Country with this code already exists.', 16, 1);
            RETURN;
        END

        -- Insert the new provider if the name does not exist
        INSERT INTO [dbo].couCountries(CountryID, CountryCode,CountryName)
        VALUES (@CountryID, @CountryCode,@CountryName);

        -- Return the inserted CountryID
        SELECT @CountryID AS CountryID;
    END TRY

    -- Begin the CATCH block
    BEGIN CATCH
        -- Handle the error
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        -- Retrieve the error details
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Raise the error with the original details
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[sp_AddGame]    Script Date: 2024/09/17 10:19:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_AddGame]
    @GameID UNIQUEIDENTIFIER,
    @Name VARCHAR(50),
	@GameDescription varchar(255),
	@Theme varchar(100),
	@ProviderName varchar(50)
AS
BEGIN
    -- Start the TRY block
    BEGIN TRY
		Declare @ProviderID UNIQUEIDENTIFIER;
        -- Check if the game name already exists
        IF EXISTS (SELECT 1 FROM [dbo].[Games] WHERE [Name] = @Name)
        BEGIN
            -- If the name already exists, raise an error
            RAISERROR('A game with this name already exists.', 16, 1);
            RETURN;
        END

		IF NOT EXISTS (Select 1 FROM [dbo].Providers with (Nolock) WHERE [Name] = @ProviderName)
        BEGIN
            -- If the provider name does not exists, raise an error
            RAISERROR('There is no provider with this name that exists.', 16, 1);
            RETURN;
        END
		
		Select @ProviderID = providerId FROM [dbo].Providers with (Nolock) WHERE [Name] = @ProviderName

        -- Insert the new provider if the name does not exist
        INSERT INTO [dbo].[Games] (GameId, Name, Description, Theme, ProviderID)
        VALUES (@GameID, @Name, @GameDescription, @Theme, @ProviderID);

        -- Return the inserted GameID
        SELECT @GameID AS GameID;
    END TRY

    -- Begin the CATCH block
    BEGIN CATCH
        -- Handle the error
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        -- Retrieve the error details
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Raise the error with the original details
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[sp_AddProvider]    Script Date: 2024/09/17 10:19:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_AddProvider]
    @ProviderID UNIQUEIDENTIFIER,
    @Name VARCHAR(50)
AS
BEGIN
    -- Start the TRY block
    BEGIN TRY
        -- Check if the provider name already exists
        IF EXISTS (SELECT 1 FROM [dbo].[Providers] WHERE [Name] = @Name)
        BEGIN
            -- If the name already exists, raise an error
            RAISERROR('A provider with this name already exists.', 16, 1);
            RETURN;
        END

        -- Insert the new provider if the name does not exist
        INSERT INTO [dbo].[Providers] (ProviderID, Name)
        VALUES (@ProviderID, @Name);

        -- Return the inserted ProviderID
        SELECT @ProviderID AS ProviderID;
    END TRY

    -- Begin the CATCH block
    BEGIN CATCH
        -- Handle the error
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        -- Retrieve the error details
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Raise the error with the original details
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[sp_AddWager]    Script Date: 2024/09/17 10:19:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    PROCEDURE [dbo].[sp_AddWager]
    @WagerId UNIQUEIDENTIFIER,
    @Theme VARCHAR(100),
	@ProviderName varchar(50),
	@GameName varchar(50),
	@Username varchar(50),
	@TransactionType varchar(10),
	@Amount decimal(10,2),
	@CountryCode varchar(10),
	@NumberOfBets int,
	@TransactionId UNIQUEIDENTIFIER,
	@SessionId UNIQUEIDENTIFIER
AS
BEGIN
    -- Start the TRY block
    BEGIN TRY
		
		Declare @BrandId UNIQUEIDENTIFIER;	
		Declare @AccountId UNIQUEIDENTIFIER;
		Declare @ExternalReferenceId UNIQUEIDENTIFIER;
		Declare @TransactionTypeId UNIQUEIDENTIFIER;
		Declare @GameId UNIQUEIDENTIFIER;
		
        -- Check if the wager  already exists
        IF EXISTS (SELECT 1 FROM [dbo].Wagers WHERE WagerId = @WagerId)
        BEGIN
            -- If the ID already exists, raise an error
            RAISERROR('A wager with this ID already exists.', 16, 1);
            RETURN;
        END

		IF NOT EXISTS (Select 1 FROM [dbo].Games with (Nolock) WHERE [Name] = @GameName)
        BEGIN
            -- If the game name does not exists, raise an error
            RAISERROR('There is no game with this name that exists.', 16, 1);
            RETURN;
        END

		IF NOT EXISTS (Select 1 FROM [dbo].Providers with (Nolock) WHERE [Name] = @ProviderName)
        BEGIN
            -- If the provider name does not exists, raise an error
            RAISERROR('There is no provider with this name that exists.', 16, 1);
            RETURN;
        END
		
		IF NOT EXISTS (Select 1 FROM [dbo].Accounts with (Nolock) WHERE Username = @Username)
        BEGIN
            -- If the account username does not exists, raise an error
            RAISERROR('There is no account with this username that exists.', 16, 1);
            RETURN;
        END

		IF NOT EXISTS (Select 1 FROM [dbo].TransactionsType with (Nolock) WHERE TransactionTypeCode = @TransactionType)
        BEGIN
            -- If the account username does not exists, raise an error
            RAISERROR('There is no transactions type with this code that exists.', 16, 1);
            RETURN;
        END

		IF NOT EXISTS (Select 1 FROM [dbo].Countries with (Nolock) WHERE CountryCode = @CountryCode)
        BEGIN
            -- If the account username does not exists, raise an error
            RAISERROR('There is no country type with this code that exists.', 16, 1);
            RETURN;
        END

		Select top 1 @GameId = GameId FROM [dbo].Games g with (Nolock) join dbo.Providers p  with (Nolock) on g.ProviderID = p.ProviderID  
		WHERE g.[Name] = @GameName  and p.Name = @ProviderName and g.Theme = @Theme
		select top 1 @TransactionTypeId  = TransactionTypeId FROM [dbo].TransactionsType with (Nolock) WHERE TransactionTypeCode = @TransactionType
        select top 1 @AccountId = AccountID, @BrandId = BrandId FROM [dbo].Accounts with (Nolock) WHERE Username = @Username
        
		Insert into [dbo].[Transactions](TransactionId,[AccountId],[Amount],[Unit],[TransactionTypeId])
		values (@TransactionId,@AccountId,@Amount,@NumberOfBets,@TransactionTypeId)


		Select top 1 @ExternalReferenceId = ExternalReferenceId
		FROM [dbo].[Transactions] with (Nolock) WHERE TransactionId = @TransactionId order by  Date desc;

		Insert into [dbo].[Sessions](SessionId,[SessionData])
		values (@SessionId,@Username)

		insert into Wagers ([WagerId],[GameId],[TransactionId],[AccountId],[ExternalReferenceId],[TransactionTypeId],[SessionId],[Amount],[NumberOfBets],[CountryCode])
		values(@WagerId,@GameId,@TransactionId,@AccountId,@ExternalReferenceId,@TransactionTypeId,@SessionId,@Amount,@NumberOfBets,@CountryCode)
		-- Return the inserted GameID
        SELECT @WagerId AS WagerId,@Theme as Theme ,@ProviderName as Provider,@GameName as GameName,
		 @TransactionId as TransactionId , @BrandId as BrandId,@AccountId as AccountId, @Username as Username,
		 @ExternalReferenceId as ExternalReferenceId, @TransactionTypeId AS TransactionTypeId, @Amount as Amount,
		 getdate() as CreatedDateTime, @NumberOfBets as NumberOfBets, @CountryCode as CountryCode, @Username as SessionData , 34563 as Duration ;
    END TRY

    -- Begin the CATCH block
    BEGIN CATCH
        -- Handle the error
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        -- Retrieve the error details
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Raise the error with the original details
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllWages]    Script Date: 2024/09/17 10:19:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_GetAllWages]
    AS
BEGIN
    BEGIN TRY
        SELECT 
            WagerId, 
            GameId, 
            TransactionId, 
            AccountId, 
            ExternalReferenceId, 
            TransactionTypeId, 
            SessionId, 
            Amount, 
            NumberOfBets, 
            CountryCode, 
            Duration, 
            DateCreated
        FROM [dbo].[Wagers] WITH (NOLOCK)
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_GetPlayerWagersWithPagination]    Script Date: 2024/09/17 10:19:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetPlayerWagersWithPagination]
    @PlayerId UNIQUEIDENTIFIER,
    @Page INT,
    @PageSize INT
AS
BEGIN
    SET NOCOUNT ON;
	    -- Start the TRY block
    BEGIN TRY
    -- Calculate the starting row
    DECLARE @StartRow INT = (@Page - 1) * @PageSize + 1;

    -- Get the total number of rows for the player
    DECLARE @TotalRows INT;
    SELECT @TotalRows = COUNT(*)
    FROM Wagers
    WHERE accountId = @PlayerId;

    -- Fetch paginated data for the player
    SELECT 
        w.wagerId as WagerId,
        g.Name AS Game,
        p.Name as  Name,
        w.amount as Amount,
        w.DateCreated AS CreatedDate
    FROM Wagers w
    INNER JOIN Games g ON w.gameId = g.gameId
	inner join Providers p on g.ProviderID  = p.ProviderID
    WHERE w.accountId = @PlayerId
    ORDER BY w.DateCreated DESC
    OFFSET @StartRow - 1 ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Return pagination metadata
    SELECT 
        @Page AS Page,
        @PageSize AS PageSize,
        @TotalRows AS Total,
        CEILING((@TotalRows + 0.0) / @PageSize) AS TotalPages;

		 END TRY

    -- Begin the CATCH block
    BEGIN CATCH
        -- Handle the error
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        -- Retrieve the error details
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Raise the error with the original details
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTopSpenders]    Script Date: 2024/09/17 10:19:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetTopSpenders]
    @Count INT
AS
BEGIN
    SET NOCOUNT ON;
	    -- Start the TRY block
    BEGIN TRY
    -- Retrieve the top spenders based on total amount spent
		SELECT TOP (@Count) 
		    a.accountId as AccountId,
		    a.username as Username,
		    SUM(w.amount) AS TotalAmountSpent
		FROM Wagers w
		INNER JOIN Accounts a ON w.accountId = a.accountId
		GROUP BY a.accountId, a.username
		ORDER BY totalAmountSpent DESC;
	 END TRY
    -- Begin the CATCH block
     BEGIN CATCH
         -- Handle the error
         DECLARE @ErrorMessage NVARCHAR(4000);
         DECLARE @ErrorSeverity INT;
         DECLARE @ErrorState INT;
	 
         -- Retrieve the error details
         SELECT 
             @ErrorMessage = ERROR_MESSAGE(),
             @ErrorSeverity = ERROR_SEVERITY(),
             @ErrorState = ERROR_STATE();
	 
         -- Raise the error with the original details
         RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
     END CATCH
END;
GO
USE [master]
GO
ALTER DATABASE [OT_Assessment_DB] SET  READ_WRITE 
GO
