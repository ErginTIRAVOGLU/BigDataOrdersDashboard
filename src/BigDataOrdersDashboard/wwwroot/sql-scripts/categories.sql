-- 1. Eğer tablo daha önce varsa hata vermemesi için kontrol et (İsteğe bağlı)
IF OBJECT_ID('dbo.Categories', 'U') IS NOT NULL
DROP TABLE dbo.Categories;
GO

-- 2. Tabloyu Oluştur
CREATE TABLE Categories (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL
);
GO

-- 3. 15 Adet Kategori Verisini Ekle
SET NOCOUNT ON;

INSERT INTO Categories (CategoryName)
VALUES 
('Electronics'),
('Computers & Accessories'),
('Smartphones & Tablets'),
('Home & Kitchen'),
('Furniture & Decor'),
('Clothing & Fashion'),
('Shoes & Footwear'),
('Beauty & Personal Care'),
('Sports & Outdoors'),
('Toys & Games'),
('Books & Stationery'),
('Automotive Parts'),
('Pet Supplies'),
('Jewelry & Watches'),
('Music & Instruments');

PRINT 'Categories tablosu oluşturuldu ve 15 kategori eklendi.';
SET NOCOUNT OFF;