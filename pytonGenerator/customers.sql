SET NOCOUNT ON;

-- 1. TABLOYU SIFIRLA
IF OBJECT_ID('dbo.Customers', 'U') IS NOT NULL
DROP TABLE dbo.Customers;
GO

CREATE TABLE Customers (
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName NVARCHAR(100) NOT NULL,
    CustomerSurname NVARCHAR(100) NOT NULL,
    CustomerEmail NVARCHAR(150) NOT NULL UNIQUE,
    CustomerPhone NVARCHAR(20) NULL,
    CustomerImageUrl NVARCHAR(255) NULL,
    CustomerCountry NVARCHAR(100) NULL,
    CustomerCity NVARCHAR(100) NULL,
    CustomerDistrict NVARCHAR(100) NULL,
    CustomerAddress NVARCHAR(255) NULL
);
GO

-- 2. VERİ ÜRETİMİ
DECLARE @i INT = 1;
DECLARE @TotalRows INT = 2000;

-- Değişkenler
DECLARE @CountryIndex INT;
DECLARE @Rnd INT; -- Rastgele sayıyı sabitlemek için kritik değişken
DECLARE @SelectedCountry NVARCHAR(100);
DECLARE @SelectedCity NVARCHAR(100);
DECLARE @SelectedDistrict NVARCHAR(100);
DECLARE @FirstName NVARCHAR(100);
DECLARE @LastName NVARCHAR(100);
DECLARE @Email NVARCHAR(150);
DECLARE @Phone NVARCHAR(20);
DECLARE @PhoneCode NVARCHAR(5);
DECLARE @Address NVARCHAR(255);
DECLARE @ImageUrl NVARCHAR(255);

-- Türkçe karakter dönüşümü için değişkenler
DECLARE @CleanName NVARCHAR(100);
DECLARE @CleanSurname NVARCHAR(100);

BEGIN TRY
    BEGIN TRANSACTION;

    WHILE @i <= @TotalRows
    BEGIN
        -- Değişkenleri Temizle
        SET @SelectedCountry = NULL; 
        SET @SelectedCity = NULL;
        SET @SelectedDistrict = NULL;
        SET @FirstName = NULL;
        SET @LastName = NULL;

        -- Ülke Seçimi (1-6)
        SET @CountryIndex = FLOOR(RAND() * 6) + 1;

        -------------------------------------------------------
        -- 1. ALMANYA
        -------------------------------------------------------
        IF @CountryIndex = 1 
        BEGIN
            SET @SelectedCountry = 'Germany';
            SET @PhoneCode = '+49';
            
            SET @Rnd = FLOOR(RAND() * 5) + 1;
            SET @SelectedCity = CHOOSE(@Rnd, 'Berlin', 'Munich', 'Hamburg', 'Frankfurt', 'Cologne');
            
            SET @Rnd = FLOOR(RAND() * 5) + 1;
            SET @SelectedDistrict = CHOOSE(@Rnd, 'Mitte', 'Kreuzberg', 'Altona', 'Schwabing', 'Nippes');
            
            SET @Rnd = FLOOR(RAND() * 10) + 1;
            SET @FirstName = CHOOSE(@Rnd, 'Hans', 'Klaus', 'Julia', 'Stefan', 'Claudia', 'Thomas', 'Sabine', 'Michael', 'Andreas', 'Petra');
            
            SET @Rnd = FLOOR(RAND() * 10) + 1;
            SET @LastName = CHOOSE(@Rnd, 'Mueller', 'Schmidt', 'Schneider', 'Fischer', 'Weber', 'Meyer', 'Wagner', 'Becker', 'Schulz', 'Hoffmann');
        END
        -------------------------------------------------------
        -- 2. FRANSA
        -------------------------------------------------------
        ELSE IF @CountryIndex = 2 
        BEGIN
            SET @SelectedCountry = 'France';
            SET @PhoneCode = '+33';
            
            SET @Rnd = FLOOR(RAND() * 5) + 1;
            SET @SelectedCity = CHOOSE(@Rnd, 'Paris', 'Lyon', 'Marseille', 'Toulouse', 'Nice');
            
            SET @Rnd = FLOOR(RAND() * 5) + 1;
            SET @SelectedDistrict = CHOOSE(@Rnd, 'Montmartre', 'Le Marais', 'Presqu''île', 'Capitole', 'Vieux Nice');
            
            SET @Rnd = FLOOR(RAND() * 10) + 1;
            SET @FirstName = CHOOSE(@Rnd, 'Jean', 'Marie', 'Pierre', 'Sophie', 'Michel', 'Camille', 'Lucas', 'Lea', 'Antoine', 'Chloe');
            
            SET @Rnd = FLOOR(RAND() * 10) + 1;
            SET @LastName = CHOOSE(@Rnd, 'Martin', 'Bernard', 'Dubois', 'Thomas', 'Robert', 'Richard', 'Petit', 'Durand', 'Leroy', 'Moreau');
        END
        -------------------------------------------------------
        -- 3. İSPANYA
        -------------------------------------------------------
        ELSE IF @CountryIndex = 3 
        BEGIN
            SET @SelectedCountry = 'Spain';
            SET @PhoneCode = '+34';
            
            SET @Rnd = FLOOR(RAND() * 5) + 1;
            SET @SelectedCity = CHOOSE(@Rnd, 'Madrid', 'Barcelona', 'Valencia', 'Seville', 'Bilbao');
            
            SET @Rnd = FLOOR(RAND() * 5) + 1;
            SET @SelectedDistrict = CHOOSE(@Rnd, 'Centro', 'Gracia', 'Ciutat Vella', 'Triana', 'Abando');
            
            SET @Rnd = FLOOR(RAND() * 10) + 1;
            SET @FirstName = CHOOSE(@Rnd, 'Antonio', 'Carmen', 'Jose', 'Maria', 'Manuel', 'Ana', 'David', 'Laura', 'Javier', 'Isabel');
            
            SET @Rnd = FLOOR(RAND() * 10) + 1;
            SET @LastName = CHOOSE(@Rnd, 'Garcia', 'Rodriguez', 'Gonzalez', 'Fernandez', 'Lopez', 'Martinez', 'Sanchez', 'Perez', 'Gomez', 'Martin');
        END
        -------------------------------------------------------
        -- 4. İTALYA
        -------------------------------------------------------
        ELSE IF @CountryIndex = 4 
        BEGIN
            SET @SelectedCountry = 'Italy';
            SET @PhoneCode = '+39';
            
            SET @Rnd = FLOOR(RAND() * 5) + 1;
            SET @SelectedCity = CHOOSE(@Rnd, 'Rome', 'Milan', 'Naples', 'Turin', 'Florence');
            
            SET @Rnd = FLOOR(RAND() * 5) + 1;
            SET @SelectedDistrict = CHOOSE(@Rnd, 'Centro Storico', 'Navigli', 'Vomero', 'San Salvario', 'Santa Croce');
            
            SET @Rnd = FLOOR(RAND() * 10) + 1;
            SET @FirstName = CHOOSE(@Rnd, 'Marco', 'Sofia', 'Alessandro', 'Giulia', 'Giuseppe', 'Francesca', 'Luca', 'Anna', 'Matteo', 'Chiara');
            
            SET @Rnd = FLOOR(RAND() * 10) + 1;
            SET @LastName = CHOOSE(@Rnd, 'Rossi', 'Russo', 'Ferrari', 'Esposito', 'Bianchi', 'Romano', 'Colombo', 'Ricci', 'Marino', 'Greco');
        END
        -------------------------------------------------------
        -- 5. HOLLANDA
        -------------------------------------------------------
        ELSE IF @CountryIndex = 5 
        BEGIN
            SET @SelectedCountry = 'Netherlands';
            SET @PhoneCode = '+31';
            
            SET @Rnd = FLOOR(RAND() * 5) + 1;
            SET @SelectedCity = CHOOSE(@Rnd, 'Amsterdam', 'Rotterdam', 'The Hague', 'Utrecht', 'Eindhoven');
            
            SET @Rnd = FLOOR(RAND() * 5) + 1;
            SET @SelectedDistrict = CHOOSE(@Rnd, 'Centrum', 'Zuid', 'Noord', 'West', 'Oost');
            
            SET @Rnd = FLOOR(RAND() * 10) + 1;
            SET @FirstName = CHOOSE(@Rnd, 'Jan', 'Johanna', 'Willem', 'Sanne', 'Johannes', 'Emma', 'Pieter', 'Eva', 'Dennis', 'Lisa');
            
            SET @Rnd = FLOOR(RAND() * 10) + 1;
            SET @LastName = CHOOSE(@Rnd, 'De Jong', 'Jansen', 'De Vries', 'Van den Berg', 'Van Dijk', 'Bakker', 'Janssen', 'Visser', 'Smit', 'Meijer');
        END
        -------------------------------------------------------
        -- 6. TÜRKİYE
        -------------------------------------------------------
        ELSE 
        BEGIN
            SET @SelectedCountry = 'Turkey';
            SET @PhoneCode = '+90';
            
            SET @Rnd = FLOOR(RAND() * 5) + 1;
            SET @SelectedCity = CHOOSE(@Rnd, N'İstanbul', N'Ankara', N'İzmir', N'Bursa', N'Antalya');
            
            SET @Rnd = FLOOR(RAND() * 5) + 1;
            SET @SelectedDistrict = CHOOSE(@Rnd, N'Kadıköy', N'Çankaya', N'Karşıyaka', N'Nilüfer', N'Muratpaşa');
            
            SET @Rnd = FLOOR(RAND() * 10) + 1;
            SET @FirstName = CHOOSE(@Rnd, N'Ahmet', N'Ayşe', N'Mehmet', N'Fatma', N'Mustafa', N'Zeynep', N'Can', N'Elif', N'Emre', N'Selin');
            
            SET @Rnd = FLOOR(RAND() * 10) + 1;
            SET @LastName = CHOOSE(@Rnd, N'Yılmaz', N'Kaya', N'Demir', N'Çelik', N'Şahin', N'Yıldız', N'Öztürk', N'Aydın', N'Özdemir', N'Arslan');
        END

        -------------------------------------------------------
        -- VERİ TEMİZLİK VE FORMATLAMA
        -------------------------------------------------------
        
        -- Email için Türkçe karakterleri İngilizceye çevir
        SET @CleanName = LOWER(@FirstName);
        SET @CleanName = REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(@CleanName, N'ğ', 'g'), N'ü', 'u'), N'ş', 's'), N'ı', 'i'), N'ö', 'o'), N'ç', 'c');
        
        SET @CleanSurname = LOWER(@LastName);
        SET @CleanSurname = REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(@CleanSurname, N'ğ', 'g'), N'ü', 'u'), N'ş', 's'), N'ı', 'i'), N'ö', 'o'), N'ç', 'c');
        -- Soyisimdeki boşlukları sil
        SET @CleanSurname = REPLACE(@CleanSurname, ' ', '');

        -- Email Oluştur (Unique)
        SET @Email = @CleanName + '.' + @CleanSurname + CAST(@i AS NVARCHAR(10)) + '@mail.eu';

        -- Telefon
        SET @Phone = @PhoneCode + ' ' + CAST(FLOOR(RAND() * (599-500)+500) AS NVARCHAR(10)) + ' ' + CAST(FLOOR(RAND() * (999999-100000)+100000) AS NVARCHAR(10));

        -- Adres
        SET @Address = @SelectedDistrict + ', ' + CAST(FLOOR(RAND() * 200) + 1 AS NVARCHAR(10)) + ' ' + @SelectedCity + ' St., No: ' + CAST(FLOOR(RAND() * 50) + 1 AS NVARCHAR(10));

        -- Resim URL
        SET @ImageUrl = 'https://picsum.photos/seed/' + CAST(@i AS NVARCHAR(10)) + '/200';

        INSERT INTO Customers (
            CustomerName, 
            CustomerSurname, 
            CustomerEmail, 
            CustomerPhone, 
            CustomerImageUrl, 
            CustomerCountry, 
            CustomerCity, 
            CustomerDistrict, 
            CustomerAddress
        )
        VALUES (
            @FirstName,
            @LastName,
            @Email,
            @Phone,
            @ImageUrl,
            @SelectedCountry,
            @SelectedCity,
            @SelectedDistrict,
            @Address
        );

        SET @i = @i + 1;
    END

    COMMIT TRANSACTION;
    PRINT 'İşlem Başarılı: 1000 Kayıt (Hatasız ve Türkçe Karakter Destekli) Eklendi.';

END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
    
    PRINT 'HATA OLUŞTU!';
    PRINT ERROR_MESSAGE();
END CATCH;

SET NOCOUNT OFF;