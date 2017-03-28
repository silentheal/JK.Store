CREATE TABLE [dbo].[ProductImage]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[ProductID] INT NOT NULL,
	[Path] NVARCHAR(1000),
	[AltText] NTEXT,
	[Created] DATETIME NULL DEFAULT GetUtcDate(),
	[Modified] DATETIME NULL DEFAULT GetUtcDate(),
    CONSTRAINT [PK_ProductImage] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_ProductImage_Product] FOREIGN KEY (ProductID) REFERENCES Product([ID]) ON DELETE CASCADE
)
