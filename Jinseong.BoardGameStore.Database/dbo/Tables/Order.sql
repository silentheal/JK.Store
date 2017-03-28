CREATE TABLE [dbo].[Order]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[OrderNumber] UNIQUEIDENTIFIER DEFAULT NewID(),
	[PurchaserEmail] NVARCHAR(1000) NULL,
	[Completed] DATETIME NULL,
	[ShipCareOf] NVARCHAR(1000) NULL,
	[ShippingAddressID] INT NULL,
	[BillingAddressID] INT NULL,
	[Created] DATETIME NULL DEFAULT GetUtcDate(),
	[Modified] DATETIME NULL DEFAULT GetUtcDate(), 
    CONSTRAINT [PK_Order] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Order_BillingAddress] FOREIGN KEY (BillingAddressID) REFERENCES [Address](ID),
    CONSTRAINT [FK_Order_ShippingAddress] FOREIGN KEY (ShippingAddressID) REFERENCES [Address](ID)
)
