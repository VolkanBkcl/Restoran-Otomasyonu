# 📊 VERİTABANI MİGRATİON REHBERİ

## ✅ Oluşturulan Migration

**Dosya:** `RestoranOtomasyonu.Entities/Migrations/20250101000000_SiparislerTablosu.cs`

Bu migration, `Siparisler` tablosunu oluşturur.

## 🗄️ Oluşturulacak Tablo: `Siparisler`

```sql
CREATE TABLE [dbo].[Siparisler] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [MasaId] INT NOT NULL,
    [KullaniciId] INT NOT NULL,
    [SatisKodu] VARCHAR(15) NULL,
    [Tutar] DECIMAL(18,2) NOT NULL,
    [IndirimOrani] DECIMAL(5,2) NOT NULL,
    [NetTutar] DECIMAL(18,2) NOT NULL,
    [OdemeDurumu] INT NOT NULL,              -- Enum: 0=Odenmedi, 1=KendiOdedi, 2=TumuOdendi
    [SiparisDurumu] INT NOT NULL,            -- Enum: 0=Beklemede, 1=Hazirlaniyor, 2=Hazir, 3=TeslimEdildi, 4=Iptal
    [Aciklama] VARCHAR(300) NULL,
    [Tarih] DATETIME NOT NULL,
    
    FOREIGN KEY ([MasaId]) REFERENCES [dbo].[Masalar]([Id]),
    FOREIGN KEY ([KullaniciId]) REFERENCES [dbo].[Kullanicilar]([Id])
);

CREATE INDEX [IX_Siparisler_MasaId] ON [dbo].[Siparisler]([MasaId]);
CREATE INDEX [IX_Siparisler_KullaniciId] ON [dbo].[Siparisler]([KullaniciId]);
```

## 🚀 Migration Çalıştırma

### Yöntem 1: Package Manager Console (Visual Studio)

1. Visual Studio'da **Tools → NuGet Package Manager → Package Manager Console** açın
2. **Default project** olarak `RestoranOtomasyonu.Entities` seçin
3. Şu komutu çalıştırın:

```powershell
Update-Database
```

### Yöntem 2: Migrate.exe (Komut Satırı)

```powershell
cd "C:\Users\Volkan\OneDrive\Desktop\Restoran Otomasyonu\RestoranOtomasyonu.Entities"
migrate.exe RestoranOtomasyonu.Entities.dll /startupConfigurationFile="App.config" /startupDirectory="bin\Debug"
```

### Yöntem 3: Kod ile (Geçici Test)

```csharp
using RestoranOtomasyonu.Entities.Models;
using System.Data.Entity;

var context = new RestoranContext();
Database.SetInitializer(new MigrateDatabaseToLatestVersion<RestoranContext, RestoranOtomasyonu.Entities.Migrations.Configuration>());
context.Database.Initialize(true);
```

## ✅ Migration Kontrolü

Migration'ın başarılı olup olmadığını kontrol etmek için:

```sql
-- SQL Server Management Studio'da çalıştırın
USE Restoran;
GO

-- Tablo var mı?
SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Siparisler';

-- Sütunlar doğru mu?
SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Siparisler'
ORDER BY ORDINAL_POSITION;
```

## ⚠️ ÖNEMLİ NOTLAR

1. **Veri Kaybı:** Bu migration yalnızca yeni tablo oluşturur, mevcut verileri etkilemez.
2. **Foreign Key:** `MasaId` ve `KullaniciId` foreign key'ler olarak tanımlıdır.
3. **Enum Değerleri:** `OdemeDurumu` ve `SiparisDurumu` INT olarak saklanır (0, 1, 2, vb.)

## 🔄 Migration Geri Alma (Rollback)

Eğer migration'ı geri almak isterseniz:

```powershell
Update-Database -TargetMigration:202512051345167_MenuVeUrunHareketleriTablolari
```

Bu, `Siparisler` tablosunu siler.

---

**Migration başarılı olduktan sonra projeyi çalıştırabilirsiniz!** 🎉

