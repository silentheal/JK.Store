CREATE TABLE [dbo].[Product]
(
	[ID] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(1000) NOT NULL,
	[Description] NTEXT NULL,
	[Price] money NULL,
	[Active] BIT NOT NULL DEFAULT(1),
	[Inventory] INT NOT NULL DEFAULT(0),
	[Created] DATETIME NULL DEFAULT GetUtcDate(),
	[Modified] DATETIME NULL DEFAULT GetUtcDate(),
    CONSTRAINT [PK_Product] PRIMARY KEY ([ID]), 
    CONSTRAINT [CK_Product_Inventory] CHECK (Inventory >= 0) 
)
