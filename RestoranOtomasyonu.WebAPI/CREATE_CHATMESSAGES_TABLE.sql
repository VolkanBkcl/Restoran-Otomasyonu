-- ChatMessages tablosunu oluştur
-- Bu script, chatbot özelliği için gerekli ChatMessages tablosunu oluşturur

USE [Restoran];
GO

-- ChatMessages tablosu yoksa oluştur
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChatMessages]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[ChatMessages] (
        [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [Sender] NVARCHAR(50) NOT NULL,
        [Message] NVARCHAR(MAX) NOT NULL,
        [Timestamp] DATETIME2 NOT NULL,
        [IsLiveSupportRequest] BIT NOT NULL DEFAULT 0,
        [SessionId] NVARCHAR(100) NULL,
        [UserName] NVARCHAR(100) NULL
    );

    -- Index'ler ekle (performans için)
    CREATE INDEX [IX_ChatMessages_SessionId] ON [dbo].[ChatMessages] ([SessionId]);
    CREATE INDEX [IX_ChatMessages_Timestamp] ON [dbo].[ChatMessages] ([Timestamp]);
    CREATE INDEX [IX_ChatMessages_IsLiveSupportRequest] ON [dbo].[ChatMessages] ([IsLiveSupportRequest]);

    PRINT 'ChatMessages tablosu başarıyla oluşturuldu.';
END
ELSE
BEGIN
    PRINT 'ChatMessages tablosu zaten mevcut.';
END
GO
