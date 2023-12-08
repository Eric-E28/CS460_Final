ALTER TABLE [Item] DROP CONSTRAINT [Item_Fk_Station];

ALTER TABLE [OrderItem] DROP CONSTRAINT [OrderItem_Fk_Item];
ALTER TABLE [OrderItem] DROP CONSTRAINT [OrderItem_Fk_Order];

ALTER TABLE [Order] DROP CONSTRAINT [Order_Fk_Dlvy];
ALTER TABLE [Order] DROP CONSTRAINT [Order_Fk_Store];

DROP TABLE [Station];
DROP TABLE [Dlvy];
DROP TABLE [Store];
DROP TABLE [OrderItem];
DROP TABLE [Order];
DROP TABLE [Item];
