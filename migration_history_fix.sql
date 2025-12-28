USE Restoran;
GO

-- Tabloyu oluştur (eğer yoksa)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = '__MigrationHistory')
BEGIN
    CREATE TABLE [dbo].[__MigrationHistory] (
        [MigrationId] [nvarchar](150) NOT NULL,
        [ContextKey] [nvarchar](300) NOT NULL,
        [Model] [varbinary](max) NOT NULL,
        [ProductVersion] [nvarchar](32) NOT NULL,
        CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY ([MigrationId], [ContextKey])
    );
END
GO

-- Önce mevcut migration'ları kontrol et
PRINT 'Mevcut migration kayıtları:';
SELECT MigrationId, ContextKey, ProductVersion 
FROM [dbo].[__MigrationHistory] 
WHERE ContextKey = 'RestoranOtomasyonu.Entities.Models.RestoranContext'
ORDER BY MigrationId;
GO

-- Migration'ları ekle (sadece eksik olanlar)
DECLARE @ContextKey NVARCHAR(300) = 'RestoranOtomasyonu.Entities.Models.RestoranContext';
DECLARE @ProductVersion NVARCHAR(32) = '6.5.1';
DECLARE @Count INT = 0;

IF NOT EXISTS (SELECT 1 FROM [dbo].[__MigrationHistory] WHERE MigrationId = '202511011139060_cls' AND ContextKey = @ContextKey)
BEGIN
    INSERT INTO [dbo].[__MigrationHistory] (MigrationId, ContextKey, Model, ProductVersion)
    VALUES ('202511011139060_cls', @ContextKey, 0x, @ProductVersion);
    SET @Count = @Count + 1;
    PRINT '202511011139060_cls eklendi';
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[__MigrationHistory] WHERE MigrationId = '202511021139494_TabloIliskilendirme' AND ContextKey = @ContextKey)
BEGIN
    INSERT INTO [dbo].[__MigrationHistory] (MigrationId, ContextKey, Model, ProductVersion)
    VALUES ('202511021139494_TabloIliskilendirme', @ContextKey, 0x, @ProductVersion);
    SET @Count = @Count + 1;
    PRINT '202511021139494_TabloIliskilendirme eklendi';
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[__MigrationHistory] WHERE MigrationId = '202511051901097_Validations2' AND ContextKey = @ContextKey)
BEGIN
    INSERT INTO [dbo].[__MigrationHistory] (MigrationId, ContextKey, Model, ProductVersion)
    VALUES ('202511051901097_Validations2', @ContextKey, 0x, @ProductVersion);
    SET @Count = @Count + 1;
    PRINT '202511051901097_Validations2 eklendi';
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[__MigrationHistory] WHERE MigrationId = '202511061210173_MasaHareketlerindeDeğişiklik' AND ContextKey = @ContextKey)
BEGIN
    INSERT INTO [dbo].[__MigrationHistory] (MigrationId, ContextKey, Model, ProductVersion)
    VALUES ('202511061210173_MasaHareketlerindeDeğişiklik', @ContextKey, 0x, @ProductVersion);
    SET @Count = @Count + 1;
    PRINT '202511061210173_MasaHareketlerindeDeğişiklik eklendi';
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[__MigrationHistory] WHERE MigrationId = '202511061335111_Resim' AND ContextKey = @ContextKey)
BEGIN
    INSERT INTO [dbo].[__MigrationHistory] (MigrationId, ContextKey, Model, ProductVersion)
    VALUES ('202511061335111_Resim', @ContextKey, 0x, @ProductVersion);
    SET @Count = @Count + 1;
    PRINT '202511061335111_Resim eklendi';
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[__MigrationHistory] WHERE MigrationId = '202511061351083_UrunResim' AND ContextKey = @ContextKey)
BEGIN
    INSERT INTO [dbo].[__MigrationHistory] (MigrationId, ContextKey, Model, ProductVersion)
    VALUES ('202511061351083_UrunResim', @ContextKey, 0x, @ProductVersion);
    SET @Count = @Count + 1;
    PRINT '202511061351083_UrunResim eklendi';
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[__MigrationHistory] WHERE MigrationId = '202511081152210_BirimFiyatGüncellemesi' AND ContextKey = @ContextKey)
BEGIN
    INSERT INTO [dbo].[__MigrationHistory] (MigrationId, ContextKey, Model, ProductVersion)
    VALUES ('202511081152210_BirimFiyatGüncellemesi', @ContextKey, 0x, @ProductVersion);
    SET @Count = @Count + 1;
    PRINT '202511081152210_BirimFiyatGüncellemesi eklendi';
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[__MigrationHistory] WHERE MigrationId = '202511230735104_MasaAdiSilindi' AND ContextKey = @ContextKey)
BEGIN
    INSERT INTO [dbo].[__MigrationHistory] (MigrationId, ContextKey, Model, ProductVersion)
    VALUES ('202511230735104_MasaAdiSilindi', @ContextKey, 0x, @ProductVersion);
    SET @Count = @Count + 1;
    PRINT '202511230735104_MasaAdiSilindi eklendi';
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[__MigrationHistory] WHERE MigrationId = '202512051345167_MenuVeUrunHareketleriTablolari' AND ContextKey = @ContextKey)
BEGIN
    INSERT INTO [dbo].[__MigrationHistory] (MigrationId, ContextKey, Model, ProductVersion)
    VALUES ('202512051345167_MenuVeUrunHareketleriTablolari', @ContextKey, 0x, @ProductVersion);
    SET @Count = @Count + 1;
    PRINT '202512051345167_MenuVeUrunHareketleriTablolari eklendi';
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[__MigrationHistory] WHERE MigrationId = '202512071245029_API' AND ContextKey = @ContextKey)
BEGIN
    INSERT INTO [dbo].[__MigrationHistory] (MigrationId, ContextKey, Model, ProductVersion)
    VALUES ('202512071245029_API', @ContextKey, 0x, @ProductVersion);
    SET @Count = @Count + 1;
    PRINT '202512071245029_API eklendi';
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[__MigrationHistory] WHERE MigrationId = '202512071302271_DosyaHatasıDuzeltme' AND ContextKey = @ContextKey)
BEGIN
    INSERT INTO [dbo].[__MigrationHistory] (MigrationId, ContextKey, Model, ProductVersion)
    VALUES ('202512071302271_DosyaHatasıDuzeltme', @ContextKey, 0x, @ProductVersion);
    SET @Count = @Count + 1;
    PRINT '202512071302271_DosyaHatasıDuzeltme eklendi';
END

PRINT '';
PRINT 'Toplam ' + CAST(@Count AS VARCHAR(10)) + ' yeni migration eklendi.';
GO

-- Kontrol
SELECT MigrationId, ContextKey, ProductVersion 
FROM [dbo].[__MigrationHistory] 
WHERE ContextKey = 'RestoranOtomasyonu.Entities.Models.RestoranContext'
ORDER BY MigrationId;
GO
