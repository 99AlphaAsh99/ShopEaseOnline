-- Categories (3 categories)
-- Only insert if the category doesn't already exist
IF NOT EXISTS (SELECT 1 FROM dbo.Categories WHERE Name = 'Clothing')
BEGIN
    INSERT INTO dbo.Categories (Name) VALUES ('Clothing');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Categories WHERE Name = 'Electronics')
BEGIN
    INSERT INTO dbo.Categories (Name) VALUES ('Electronics');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Categories WHERE Name = 'HomeKitchen')
BEGIN
    INSERT INTO dbo.Categories (Name) VALUES ('HomeKitchen');
END

-- Variables to store CategoryIDs
DECLARE @ClothingCategoryID INT;
DECLARE @ElectronicsCategoryID INT;
DECLARE @HomeKitchenCategoryID INT;

-- Get the CategoryIDs (whether they were just inserted or already existed)
SELECT @ClothingCategoryID = CategoryID FROM dbo.Categories WHERE Name = 'Clothing';
SELECT @ElectronicsCategoryID = CategoryID FROM dbo.Categories WHERE Name = 'Electronics';
SELECT @HomeKitchenCategoryID = CategoryID FROM dbo.Categories WHERE Name = 'HomeKitchen';

-- Products (2 products per category)
-- Clothing Products
IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Title = 'T-Shirt' AND CategoryID = @ClothingCategoryID)
BEGIN
    INSERT INTO dbo.Products (Title, Description, Price, StockQuantity, ImageURL, CategoryID)
    VALUES ('T-Shirt', 'Cotton t-shirt in various colors', 19.99, 100, 
            'https://images.unsplash.com/photo-1579109015395-eb2bf0fa095e?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 
            @ClothingCategoryID);
END

IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Title = 'Jeans' AND CategoryID = @ClothingCategoryID)
BEGIN
    INSERT INTO dbo.Products (Title, Description, Price, StockQuantity, ImageURL, CategoryID)
    VALUES ('Jeans', 'Classic blue denim jeans', 49.99, 50, 
            'https://images.unsplash.com/photo-1542272604-787c3835535d?q=80&w=1926&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 
            @ClothingCategoryID);
END

-- Electronics Products
IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Title = 'Smartphone' AND CategoryID = @ElectronicsCategoryID)
BEGIN
    INSERT INTO dbo.Products (Title, Description, Price, StockQuantity, ImageURL, CategoryID)
    VALUES ('Smartphone', 'Latest model smartphone with high-resolution camera', 699.99, 30, 
            'https://images.unsplash.com/photo-1610945265064-0e34e5519bbf?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 
            @ElectronicsCategoryID);
END

IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Title = 'Headphones' AND CategoryID = @ElectronicsCategoryID)
BEGIN
    INSERT INTO dbo.Products (Title, Description, Price, StockQuantity, ImageURL, CategoryID)
    VALUES ('Headphones', 'Wireless noise-cancelling headphones', 149.99, 45, 
            'https://images.unsplash.com/photo-1609081219090-a6d81d3085bf?q=80&w=1926&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 
            @ElectronicsCategoryID);
END

-- HomeKitchen Products
IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Title = 'Coffee Maker' AND CategoryID = @HomeKitchenCategoryID)
BEGIN
    INSERT INTO dbo.Products (Title, Description, Price, StockQuantity, ImageURL, CategoryID)
    VALUES ('Coffee Maker', 'Programmable coffee maker with timer', 79.99, 25, 
            'https://images.unsplash.com/photo-1545936055-22b27770efca?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 
            @HomeKitchenCategoryID);
END

IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Title = 'Knife Set' AND CategoryID = @HomeKitchenCategoryID)
BEGIN
    INSERT INTO dbo.Products (Title, Description, Price, StockQuantity, ImageURL, CategoryID)
    VALUES ('Knife Set', 'Professional 8-piece kitchen knife set', 129.99, 15, 
            'https://images.unsplash.com/photo-1507648154934-f230d5bd6942?q=80&w=1995&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 
            @HomeKitchenCategoryID);
END