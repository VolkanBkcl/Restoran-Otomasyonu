-- Migration Geçmişini Temizleme Scripti
-- Bu script, __MigrationHistory tablosundaki bozuk kayıtları temizler

USE Restoran;
GO

-- Önce mevcut durumu kontrol et
PRINT 'Mevcut migration kayıtları:';
SELECT 
    MigrationId,
    ContextKey,
    LEN(Model) AS ModelLength,
    ProductVersion
FROM __MigrationHistory
ORDER BY MigrationId;
GO

-- Bozuk Model alanlarını kontrol et (NULL, boş veya çok kısa olanlar)
PRINT '';
PRINT 'Bozuk migration kayıtları:';
SELECT 
    MigrationId,
    ContextKey,
    LEN(Model) AS ModelLength,
    ProductVersion
FROM __MigrationHistory
WHERE Model IS NULL OR Model = 0x OR LEN(Model) < 10;
GO

-- ÇÖZÜM 1: Bozuk kayıtları sil (ÖNERİLEN)
PRINT '';
PRINT 'Bozuk migration kayıtları siliniyor...';
DELETE FROM __MigrationHistory
WHERE Model IS NULL OR Model = 0x OR LEN(Model) < 10;
GO

PRINT 'Bozuk kayıtlar silindi.';
GO

-- ÇÖZÜM 2: Tüm migration geçmişini temizle (SON ÇARE - DİKKATLİ KULLANIN!)
-- Eğer yukarıdaki çözüm işe yaramazsa, tüm migration geçmişini temizleyebilirsiniz
-- Ancak bu, migration'ları yeniden uygulamanızı gerektirebilir
-- UNCOMMENT EDİN SADECE GEREKİRSE:
-- DELETE FROM __MigrationHistory;
-- GO
-- PRINT 'Tüm migration geçmişi temizlendi.';
-- GO

-- Son durumu kontrol et
PRINT '';
PRINT 'Kalan migration kayıtları:';
SELECT 
    MigrationId,
    ContextKey,
    LEN(Model) AS ModelLength,
    ProductVersion
FROM __MigrationHistory
ORDER BY MigrationId;
GO

PRINT '';
PRINT 'Migration geçmişi temizlendi. Uygulamayı yeniden başlatın.';
GO
