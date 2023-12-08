-- Create tables
--CREATE DATABASE [CoffeeShop];

CREATE TABLE [Item] 
(
    [ID]                  int           NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [Name] varchar(255) NOT NULL,
    [Description] varchar(255) NOT NULL,
    [Price] float NOT NULL,
    [StationID] int NOT NULL
);

CREATE TABLE [Order] 
(
    [ID]                  int           NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [StoreID] int,
    [DlvyID] int,
    [CustomerName] varchar(255),
    [Time] datetime
);

CREATE TABLE [OrderItem] (
    [ID]                  int           NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [OrderID] int,
    [ItemID] int,
    [Qty] int,
    [Completed] bit
);

CREATE TABLE [Store] (
    [ID]                  int           NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [StoreName] varchar(255)
);

CREATE TABLE [Dlvy] (
    [ID]                  int           NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [DlvyName] varchar(255)
);

CREATE TABLE [Station] (
    [ID]                  int           NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [StationName] varchar(255)
);

-- Establish relationships
ALTER TABLE [Item] ADD CONSTRAINT [Item_Fk_Station] FOREIGN KEY ([StationID]) REFERENCES [Station] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [Order] ADD CONSTRAINT [Order_Fk_Store] FOREIGN KEY ([StoreID]) REFERENCES [Store] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE [Order] ADD CONSTRAINT [Order_Fk_Dlvy] FOREIGN KEY ([DlvyID]) REFERENCES [Dlvy] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [OrderItem] ADD CONSTRAINT [OrderItem_Fk_Order] FOREIGN KEY ([OrderID]) REFERENCES [Order] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE [OrderItem] ADD CONSTRAINT [OrderItem_Fk_Item] FOREIGN KEY ([ItemID]) REFERENCES [Item] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
