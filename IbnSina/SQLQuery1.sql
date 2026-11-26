-- Ibn Sina pharma app: Products and Categories database (training / development data).
-- The names and prices below are made-up examples. Replace them with the real catalog later.
-- WARNING: running this script again deletes the two tables and rebuilds them.

IF DB_ID(N'IbnSinaPharma') IS NULL
    CREATE DATABASE IbnSinaPharma;
GO

USE IbnSinaPharma;
GO

-- Products first, because it points to Categories
IF OBJECT_ID(N'dbo.Products', N'U') IS NOT NULL DROP TABLE dbo.Products;
IF OBJECT_ID(N'dbo.Categories', N'U') IS NOT NULL DROP TABLE dbo.Categories;
GO

CREATE TABLE dbo.Categories
(
    Id          INT IDENTITY(1,1) PRIMARY KEY,   -- IDENTITY = the database numbers it for you
    Name        NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(255) NULL
);

CREATE TABLE dbo.Products
(
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    CategoryId  INT NOT NULL REFERENCES dbo.Categories(Id),
    Name        NVARCHAR(150) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Price       DECIMAL(10,2) NOT NULL,
    Stock       INT NOT NULL DEFAULT 0,
    IsActive    BIT NOT NULL DEFAULT 1
);
GO

-- Categories (they get Ids 1 to 7 in this order)
INSERT INTO dbo.Categories (Name, Description) VALUES
    (N'Pain Relief',             N'Analgesics and anti-inflammatory products'),
    (N'Antibiotics',             N'Antibacterial medicines'),
    (N'Vitamins and Supplements',N'Vitamins, minerals and nutritional supplements'),
    (N'Medical Supplies',        N'Devices and consumables used in pharmacies and clinics'),
    (N'Skin Care',               N'Creams, lotions and sun protection'),
    (N'Hair Care',               N'Shampoos and conditioners'),
    (N'Baby Care',               N'Products for babies and infants');

-- Products. Products 1 to 18 are active. Product 19 is inactive, to test the "active only" rule.
INSERT INTO dbo.Products (CategoryId, Name, Description, Price, Stock, IsActive) VALUES
    (1, N'Paracetamol 500 mg Tablets',       N'Active ingredient: Paracetamol. Pack of 20 tablets.',            25.50, 300, 1),
    (1, N'Ibuprofen 400 mg Tablets',         N'Active ingredient: Ibuprofen. Pack of 20 tablets.',              38.00, 220, 1),
    (1, N'Diclofenac Gel 50 g',              N'Active ingredient: Diclofenac. Topical gel, 50 g tube.',          55.00, 140, 1),
    (2, N'Amoxicillin 500 mg Capsules',      N'Active ingredient: Amoxicillin. Pack of 16 capsules.',            62.00, 180, 1),
    (2, N'Azithromycin 500 mg Tablets',      N'Active ingredient: Azithromycin. Pack of 3 tablets.',             85.00, 120, 1),
    (3, N'Vitamin C 1000 mg Effervescent',   N'Active ingredient: Ascorbic acid. Tube of 20 effervescent tablets.', 70.00, 250, 1),
    (3, N'Multivitamin Tablets (30)',        N'Daily multivitamin and mineral tablets. Pack of 30.',            120.00, 160, 1),
    (3, N'Omega-3 Capsules (60)',            N'Fish oil supplement. Bottle of 60 capsules.',                    210.00,  90, 1),
    (4, N'Digital Thermometer',              N'Digital thermometer with flexible tip.',                         150.00,  75, 1),
    (4, N'Surgical Gloves (Box of 100)',     N'Disposable latex-free gloves, box of 100.',                      180.00, 200, 1),
    (4, N'Elastic Bandage 6 cm',             N'Elastic compression bandage, 6 cm wide.',                         22.00, 400, 1),
    (4, N'Blood Pressure Monitor',           N'Automatic upper-arm blood pressure monitor.',                    750.00,  30, 1),
    (5, N'Moisturizing Cream 100 ml',        N'Daily moisturizing cream for dry skin.',                          95.00, 130, 1),
    (5, N'Sunscreen SPF 50 (120 ml)',        N'High protection sunscreen lotion.',                              185.00,  85, 1),
    (6, N'Anti-Dandruff Shampoo 400 ml',     N'Shampoo for dandruff-prone scalp.',                              110.00, 150, 1),
    (6, N'Hair Conditioner 250 ml',          N'Conditioner for dry and damaged hair.',                           90.00, 110, 1),
    (7, N'Baby Shampoo 200 ml',              N'Gentle tear-free shampoo for babies.',                            65.00, 170, 1),
    (7, N'Baby Diapers Size 3 (Pack of 44)', N'Disposable diapers, size 3, pack of 44.',                        240.00,  95, 1),
    (6, N'Old Formula Shampoo 400 ml',       N'Discontinued, must stay hidden.',                                 80.00,   0, 0);
GO

-- Quick check: you should see 18 active products and 7 categories
SELECT COUNT(*) AS ActiveProducts FROM dbo.Products WHERE IsActive = 1;
SELECT COUNT(*) AS TotalCategories FROM dbo.Categories;
GO