CREATE TABLE [dbo].[Country]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[Abbreviation] NVARCHAR(10) NOT NULL,
    [Name] NVARCHAR(100) NOT NULL, 
	[Created] DATETIME NULL DEFAULT GetUtcDate(),
	[Modified] DATETIME NULL DEFAULT GetUtcDate(),
    CONSTRAINT [PK_Country] PRIMARY KEY ([Id])
)
