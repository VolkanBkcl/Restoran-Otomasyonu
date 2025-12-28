-- Migration Geçmişindeki Bozuk Model Alanlarını Düzeltme Scripti
-- Bu script, __MigrationHistory tablosundaki boş veya bozuk Model alanlarını düzeltir

USE Restoran;
GO

-- Önce mevcut durumu kontrol et
SELECT 
    MigrationId,
    ContextKey,
    Model,
    ProductVersion
FROM __MigrationHistory
WHERE Model IS NULL OR Model = '' OR LEN(Model) < 10;
GO

-- Boş veya bozuk Model alanlarını minimal geçerli bir XML ile doldur
-- Not: Bu, Entity Framework'ün migration geçmişini okumasını sağlar
UPDATE __MigrationHistory
SET Model = '<?xml version="1.0" encoding="utf-8"?><edmx:Edmx Version="3.0" xmlns:edmx="http://schemas.microsoft.com/ado/2009/11/edmx"><edmx:Runtime><edmx:StorageModels><Schema Namespace="RestoranOtomasyonu.Entities.Models.Store" Alias="Self" Provider="System.Data.SqlClient" ProviderManifestToken="2012" xmlns:store="http://schemas.microsoft.com/ado/2007/12/edm/EntityStoreSchemaGenerator" xmlns="http://schemas.microsoft.com/ado/2009/11/edm/ssdl"><EntityContainer Name="RestoranOtomasyonu.Models.StoreContainer"><EntitySet Name="Menu" EntityType="Self.Menu" store:Type="Tables" Schema="dbo" /><EntitySet Name="Urun" EntityType="Self.Urun" store:Type="Tables" Schema="dbo" /><EntitySet Name="Kullanicilar" EntityType="Self.Kullanicilar" store:Type="Tables" Schema="dbo" /><EntitySet Name="Masalar" EntityType="Self.Masalar" store:Type="Tables" Schema="dbo" /></EntityContainer></Schema></edmx:StorageModels></edmx:Runtime></edmx:Edmx>'
WHERE Model IS NULL OR Model = '' OR LEN(Model) < 10;
GO

-- Alternatif: Eğer yukarıdaki çözüm işe yaramazsa, migration geçmişini temizle
-- DİKKAT: Bu, tüm migration geçmişini siler. Sadece son çare olarak kullanın!
-- DELETE FROM __MigrationHistory;
-- GO

PRINT 'Migration geçmişi düzeltildi.';
GO
