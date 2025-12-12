# Blockchain Ödeme Sistemi - Hızlı Başlangıç

## ✅ Tamamlanan İşlemler

1. ✅ Solidity Akıllı Kontrat (`RestaurantPayment.sol`) oluşturuldu
2. ✅ Front-end'e `ethers.js` eklendi
3. ✅ `payOrder` fonksiyonu blockchain'e çevrildi
4. ✅ Backend'e `CompletePayment` endpoint'i eklendi
5. ✅ Entegrasyon tamamlandı

## 🚀 Hızlı Kurulum

### Adım 1: Ganache'ı Başlat
1. Ganache'ı açın (https://trufflesuite.com/ganache/)
2. "Quickstart" ile başlatın
3. Port: **7545** (varsayılan)
4. Chain ID: **1337** veya **5777** (Ganache versiyonuna göre)

### Adım 2: Kontratı Deploy Edin
**Remix IDE ile (Önerilen):**
1. https://remix.ethereum.org adresine gidin
2. `RestaurantPayment.sol` dosyasını yükleyin
3. Compile edin (Solidity 0.8.0+)
4. MetaMask'ı Ganache'a bağlayın
5. Deploy edin
6. **Contract Address**'i kopyalayın

**Detaylı adımlar için:** `DEPLOYMENT_NOTES.md` dosyasına bakın.

### Adım 3: Front-End'i Güncelleyin
`RestoranOtomasyonu.WebAPI/wwwroot/js/app.js` dosyasında:

```javascript
const CONTRACT_ADDRESS = "0xYOUR_CONTRACT_ADDRESS"; // Deploy ettiğiniz adresi buraya yapıştırın
```

### Adım 4: MetaMask'ı Ayarlayın
1. MetaMask'ı açın
2. Settings → Networks → Add Network
3. **Network Name:** Ganache Local
4. **RPC URL:** http://127.0.0.1:7545
5. **Chain ID:** 1337 (veya Ganache'ta gösterilen)
6. **Currency Symbol:** ETH

### Adım 5: Ganache Hesabını MetaMask'a Import Edin
1. Ganache'ta Account 1'in "key" ikonuna tıklayın
2. Private Key'i kopyalayın
3. MetaMask → Import Account → Private Key yapıştırın

### Adım 6: Test Edin
1. Web uygulamasını açın
2. Bir sipariş oluşturun
3. "Kendi Payımı Öde" butonuna tıklayın
4. MetaMask'ta işlemi onaylayın
5. Transaction hash'i kontrol edin

## 📁 Dosya Yapısı

```
RestoranOtomasyonu.WebAPI/
├── Contracts/
│   ├── RestaurantPayment.sol      # Solidity akıllı kontrat
│   ├── DEPLOYMENT_NOTES.md       # Detaylı deployment rehberi
│   └── README.md                 # Bu dosya
├── Controllers/
│   └── OrderController.cs        # CompletePayment endpoint'i eklendi
└── wwwroot/
    ├── masa/
    │   └── index.html            # ethers.js eklendi
    └── js/
        └── app.js                # payOrder fonksiyonu blockchain'e çevrildi
```

## 🔧 Yapılandırma

### Front-End Ayarları (`app.js`)
```javascript
const CONTRACT_ADDRESS = "0xYOUR_CONTRACT_ADDRESS"; // Değiştirin!
const GANACHE_CHAIN_ID = 1337; // Ganache Chain ID'niz
const ETH_TO_TL_RATE = 100000; // 1 ETH = 100.000 TL (test için)
```

### Backend Endpoint
```
POST /api/order/completePayment
Body: {
    "orderId": 123,
    "transactionHash": "0x...",
    "amount": 250.00
}
```

## 🎯 Özellikler

- ✅ MetaMask entegrasyonu
- ✅ Ganache Local Blockchain desteği
- ✅ Otomatik ağ kontrolü
- ✅ Transaction hash doğrulama
- ✅ Veritabanına blockchain ödeme kaydı
- ✅ SignalR bildirimleri
- ✅ Detaylı hata yönetimi

## 🐛 Sorun Giderme

**Problem:** "MetaMask bulunamadı"
- Çözüm: MetaMask eklentisini yükleyin

**Problem:** "Yanlış ağ" hatası
- Çözüm: MetaMask'ta Ganache Local ağını seçin

**Problem:** "Contract Address ayarlanmamış"
- Çözüm: `app.js` dosyasında `CONTRACT_ADDRESS` değişkenini güncelleyin

**Problem:** Transaction başarısız
- Çözüm: Ganache'ın çalıştığından ve MetaMask'ta yeterli ETH olduğundan emin olun

## 📚 Daha Fazla Bilgi

- Detaylı deployment: `DEPLOYMENT_NOTES.md`
- Solidity kontrat: `RestaurantPayment.sol`
- Front-end kod: `wwwroot/js/app.js`
- Backend kod: `Controllers/OrderController.cs`

