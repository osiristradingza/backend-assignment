USE [OT_Assessment_DB]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayerAccount](
	[AccountId] [uniqueidentifier] NOT NULL,
	[Username] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Player] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WagerStatus](
	[WagerStatusId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](10) NOT NULL,
 CONSTRAINT [PK_WagerStatuss] PRIMARY KEY CLUSTERED 
(
	[WagerStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Game](
	[GameId] [uniqueidentifier] NOT NULL,
	[Theme] [varchar](50) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[ExternalReferenceId] [uniqueidentifier] NOT NULL,
	[StartDateTime] [datetime2](7) NOT NULL,
	[EndDateTime] [datetime2](7) NOT NULL,
	
 CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED 
(
	[GameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayerCasinoWager](
	[WagerId] [uniqueidentifier] NOT NULL,
	[Theme] [varchar](50) NOT NULL,
	[Provider] [varchar](100) NOT NULL,
	[GameName] [varchar](100) NOT NULL,
	[TransactionId] [uniqueidentifier] NOT NULL,
	[BrandId] [uniqueidentifier] NOT NULL,
	[AccountId] [uniqueidentifier] NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[ExternalReferenceId] [uniqueidentifier] NOT NULL,
	[TransactionTypeId] [uniqueidentifier] NOT NULL,
	[Amount] [money] NOT NULL,
	[CreatedDateTime] [datetime2](7) NOT NULL,
	[NumberOfBets] [int] NOT NULL,
	[CountryCode] [varchar](5) NOT NULL,
	[SessionData] [varchar](max) NOT NULL,
	[Duration] [bigint] NOT NULL,

	[WagerStatusId] INT NOT NULL DEFAULT (1),
	[ModifiedDateTime] [datetime2](7) NULL,
	[IsResulted] BIT,
	
 CONSTRAINT [PK_PlayerCasinoWager] PRIMARY KEY CLUSTERED 
(
	[WagerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[PlayerAccount] ADD  DEFAULT (newid()) FOR [AccountId]
GO
ALTER TABLE [dbo].[PlayerCasinoWager] ADD  DEFAULT (newid()) FOR [WagerId]
GO
ALTER TABLE [dbo].[PlayerCasinoWager]  WITH CHECK ADD  CONSTRAINT [FK_PlayerCasinoWager_PlayerAccount] FOREIGN KEY([AccountId])
REFERENCES [dbo].[PlayerAccount] ([AccountId])
GO
ALTER TABLE [dbo].[PlayerCasinoWager] CHECK CONSTRAINT [FK_PlayerCasinoWager_PlayerAccount]
GO

ALTER TABLE [dbo].[PlayerCasinoWager]  WITH CHECK ADD  CONSTRAINT [FK_PlayerCasinoWager_WagerStatus] FOREIGN KEY([WagerStatusId])
REFERENCES [dbo].[WagerStatus] ([WagerStatusId])
GO
ALTER TABLE [dbo].[PlayerCasinoWager] CHECK CONSTRAINT [FK_PlayerCasinoWager_WagerStatus]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetGameByExternaReferenceId]
@ExternaReferenceId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT [GameId]
      ,[Theme]
      ,[Name]
      ,[ExternalReferenceId]
      ,[StartDateTime]
      ,[EndDateTime]
  FROM [dbo].[Game]
  WHERE [ExternalReferenceId] = @ExternaReferenceId
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetAllCasinoWagers] 
AS
BEGIN
	SELECT [WagerId]
      ,[Theme]
      ,[Provider]
      ,[GameName]
      ,[TransactionId]
      ,[BrandId]
      ,[AccountId]
      ,[Username]
      ,[ExternalReferenceId]
      ,[TransactionTypeId]
      ,[Amount]
      ,[CreatedDateTime]
      ,[NumberOfBets]
      ,[CountryCode]
      ,[SessionData]
      ,[Duration]
	  ,[WagerStatusId]
	  ,[ModifiedDateTime]
	  ,[IsResulted]
  FROM [dbo].[PlayerCasinoWager]
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetAllPlayers] 
AS
BEGIN
	SELECT [AccountId]
      ,[Username]
	FROM [dbo].[PlayerAccount]
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetCasinoWagersByAccountId] 
@AccountId UNIQUEIDENTIFIER,
@Page INT = 1,
@PageSize INT = 10
AS
BEGIN
	
SET NOCOUNT ON;

	IF(@Page <=0)
    BEGIN
		SET @Page = 1;
    END
    IF(@PageSize<=0)
    BEGIN
		SET @PageSize = 2147483647;
    END
    DECLARE @SkipRows INT = (@Page - 1) * @PageSize,
			@TotalPages INT

  ; WITH CTE_PCW AS
    (
      SELECT [WagerId]
			  ,[Provider]
			  ,[GameName]
			  ,[Amount]
			  ,[CreatedDateTime]
		  FROM [dbo].[PlayerCasinoWager]
		  WHERE [AccountId] = @AccountId
		  ORDER BY [CreatedDateTime] DESC
		  OFFSET @SkipRows ROWS 
		FETCH NEXT @PageSize ROWS ONLY
    ),
    CTE_TotalRows AS
    (
        SELECT COUNT([WagerId]) AS total FROM [PlayerCasinoWager]
        WHERE [AccountId] = @AccountId
    )
  
    SELECT @Page AS [Page], @PageSize AS PageSize,Total, (Count(*) OVER() + @PageSize - 1)/@PageSize AS TotalPages, 
	[Data].[WagerId], [Data].[GameName] AS Game, [Data].[Provider], [Data].[Amount],[Data].[CreatedDateTime]
			    
	FROM [PlayerCasinoWager] AS [Data], CTE_TotalRows
    WHERE EXISTS (SELECT 1 FROM CTE_PCW WHERE CTE_PCW.[WagerId] = [Data].[WagerId])
	FOR JSON AUTO
    OPTION (RECOMPILE)

RETURN
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetTopSpenders] 
@Count INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT TOP (@Count)
      [AccountId],
	  [Username],
      SUM([Amount]) AS totalAmountSpend
      
	 FROM [dbo].[PlayerCasinoWager]
	 GROUP BY [AccountId],[Username]

	 ORDER BY SUM([Amount]) DESC
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spInsertPlayer] 
	@AccountId UNIQUEIDENTIFIER,
	@Username VARCHAR(50)
AS
BEGIN
	INSERT INTO [dbo].[PlayerAccount]
           ([AccountId], [Username])
     VALUES
           (@AccountId, @Username)
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spInsertCasinoWager]
	@Theme VARCHAR(50),
	@Provider VARCHAR(100),
	@GameName VARCHAR(100),
	@TransactionId UNIQUEIDENTIFIER,
	@BrandId UNIQUEIDENTIFIER,
	@AccountId UNIQUEIDENTIFIER,
	@Username VARCHAR(50),
	@ExternalReferenceId UNIQUEIDENTIFIER,
	@TransactionTypeId UNIQUEIDENTIFIER,
	@Amount MONEY,
	@CreatedDateTime DATETIME,
	@NumberOfBets INT,
	@CountryCode VARCHAR(5),
	@SessionData VARCHAR(MAX),
	@Duration BIGINT
AS
BEGIN
IF NOT EXISTS (SELECT 1 FROM [dbo].[PlayerAccount] WHERE [AccountId] = @AccountId)
BEGIN
	EXEC [dbo].[spInsertPlayer] @Username = @Username, @AccountId = @AccountId

	INSERT INTO [dbo].[PlayerCasinoWager]
           ([Theme]
           ,[Provider]
           ,[GameName]
           ,[TransactionId]
           ,[BrandId]
           ,[AccountId]
           ,[Username]
           ,[ExternalReferenceId]
           ,[TransactionTypeId]
           ,[Amount]
           ,[CreatedDateTime]
           ,[NumberOfBets]
           ,[CountryCode]
           ,[SessionData]
           ,[Duration]
		   ,[IsResulted])
     VALUES
           (@Theme
           ,@Provider
           ,@GameName
           ,@TransactionId
           ,@BrandId
           ,@AccountId
           ,@Username
           ,@ExternalReferenceId
           ,@TransactionTypeId
           ,@Amount
           ,@CreatedDateTime
           ,@NumberOfBets
           ,@CountryCode
           ,@SessionData
           ,@Duration
		   ,0)
END
ELSE
	BEGIN
	INSERT INTO [dbo].[PlayerCasinoWager]
           ([Theme]
           ,[Provider]
           ,[GameName]
           ,[TransactionId]
           ,[BrandId]
           ,[AccountId]
           ,[Username]
           ,[ExternalReferenceId]
           ,[TransactionTypeId]
           ,[Amount]
           ,[CreatedDateTime]
           ,[NumberOfBets]
           ,[CountryCode]
           ,[SessionData]
           ,[Duration]
		   ,[IsResulted])
     VALUES
           (@Theme
           ,@Provider
           ,@GameName
           ,@TransactionId
           ,@BrandId
           ,@AccountId
           ,@Username
           ,@ExternalReferenceId
           ,@TransactionTypeId
           ,@Amount
           ,@CreatedDateTime
           ,@NumberOfBets
           ,@CountryCode
           ,@SessionData
           ,@Duration
		   ,0)
	END

END
GO

SET IDENTITY_INSERT [dbo].[WagerStatus] ON 
GO
INSERT [dbo].[WagerStatus] ([WagerStatusId], [Name]) VALUES (1, N'Pending')
GO
INSERT [dbo].[WagerStatus] ([WagerStatusId], [Name]) VALUES (2, N'Cancelled')
GO
INSERT [dbo].[WagerStatus] ([WagerStatusId], [Name]) VALUES (3, N'Win')
GO
INSERT [dbo].[WagerStatus] ([WagerStatusId], [Name]) VALUES (4, N'Lose')
GO
SET IDENTITY_INSERT [dbo].[WagerStatus] OFF
GO

INSERT [dbo].[Game] ([GameId], [Theme], [Name], [ExternalReferenceId], [StartDateTime], [EndDateTime]) 
VALUES (N'7bf77369-e8d8-4cf3-973b-629175af37e2', N'adventure', N'Chess', N'49d7fd17-eeec-4836-a967-4e09da37e2b9', CAST(N'2024-09-08T20:00:00.0000000' AS DateTime2), CAST(N'2024-09-08T22:00:00.0000000' AS DateTime2))
GO