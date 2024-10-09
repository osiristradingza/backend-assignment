-- Create table for Players
CREATE TABLE dbo.Players (
    PlayerId UNIQUEIDENTIFIER PRIMARY KEY,
    Username NVARCHAR(255) NOT NULL,
    CreatedDateTime DATETIME NOT NULL DEFAULT GETDATE()
);

-- Create table for CasinoWagers
CREATE TABLE dbo.CasinoWagers (
    WagerId UNIQUEIDENTIFIER PRIMARY KEY,
    TransactionId UNIQUEIDENTIFIER NOT NULL,
    BrandId UNIQUEIDENTIFIER NOT NULL,
	AccountId UNIQUEIDENTIFIER NOT NULL,
    GameName NVARCHAR(255) NOT NULL,
    Provider NVARCHAR(255) NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    TransactionTypeId UNIQUEIDENTIFIER NOT NULL,
    CreatedDateTime DATETIME NOT NULL,
    NumberOfBets INT NOT NULL,
    SessionData NVARCHAR(MAX),
    Duration BIGINT,
    PlayerId UNIQUEIDENTIFIER NOT NULL, -- Foreign key renamed to be consistent with Players table
    CountryCode NVARCHAR(10),
    ExternalReferenceId UNIQUEIDENTIFIER NOT NULL,
    Theme NVARCHAR(255),
    Username NVARCHAR(255),
    FOREIGN KEY (PlayerId) REFERENCES dbo.Players(PlayerId) 
);

-- Create indexes for faster lookups
CREATE NONCLUSTERED INDEX IDX_Player_Username ON dbo.Players(Username);
CREATE NONCLUSTERED INDEX IDX_CasinoWagers_PlayerId ON dbo.CasinoWagers(PlayerId);
CREATE NONCLUSTERED INDEX IDX_CasinoWagers_CreatedDateTime ON dbo.CasinoWagers(CreatedDateTime);
