CREATE TABLE [dbo].[Address]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Line1] NVARCHAR(50) NULL, 
    [Line2] NVARCHAR(50) NULL, 
    [City] NVARCHAR(50) NULL, 
    [State] NVARCHAR(50) NULL, 
    [Zip] NCHAR(10) NULL, 
    [Country] NVARCHAR(50) NULL
)
