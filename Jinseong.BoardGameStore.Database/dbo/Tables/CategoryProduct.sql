CREATE TABLE [dbo].[CategoryProduct]
(
	[CategoryID] INT NOT NULL,
	[ProductID] INT NOT NULL,
	[Created] DATETIME NULL DEFAULT GetUtcDate(),
	[Modified] DATETIME NULL DEFAULT GetUtcDate(), 
    CONSTRAINT [PK_CategoryProduct] PRIMARY KEY ([ProductID], [CategoryID]), 
    CONSTRAINT [FK_CategoryProduct_Product] FOREIGN KEY (ProductID) REFERENCES Product([ID]), 
    CONSTRAINT [FK_CategoryProduct_Category] FOREIGN KEY (CategoryID) REFERENCES Category(ID), 
)
