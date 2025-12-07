# RestoranOtomasyonu.WebAPI

ASP.NET Core 8.0 Web API projesi - Mobil entegrasyon ve SignalR desteği ile.

## 🚀 Özellikler

- **Authentication:** Müşteri kaydı ve giriş
- **Menu API:** Ürün ve menü listesi
- **Order API:** Sipariş oluşturma, listeleme, ödeme
- **SignalR Hub:** Gerçek zamanlı sipariş bildirimleri

## 📡 API Endpoints

### Authentication
- `POST /api/auth/register` - Müşteri kaydı
- `POST /api/auth/login` - Giriş yap

### Menu
- `GET /api/menu` - Tüm ürün ve menüleri getir

### Orders
- `POST /api/order/create` - Yeni sipariş oluştur
- `GET /api/order/table/{masaId}` - Masadaki tüm siparişler
- `GET /api/order/my/{kullaniciId}` - Kullanıcının siparişleri
- `POST /api/order/pay/{siparisId}` - Sipariş ödemesi

## 🔌 SignalR Hub

- **Endpoint:** `/siparisHub`
- **Events:**
  - `ReceiveOrder` - Yeni sipariş geldiğinde
  - `OrderPaid` - Sipariş ödendiğinde

## ⚙️ Yapılandırma

Connection string `appsettings.json` dosyasında tanımlıdır:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data source=(localdb)\\MSSQLLocalDB;Initial Catalog=Restoran;Integrated Security=true"
  }
}
```

## 🏃 Çalıştırma

```bash
dotnet run
```

API: `https://localhost:5001` veya `http://localhost:5000`
Swagger UI: `https://localhost:5001/swagger`

