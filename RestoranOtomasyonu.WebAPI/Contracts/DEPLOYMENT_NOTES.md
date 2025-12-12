# RestaurantPayment.sol - Deployment Notları

## 📋 Ganache Local Blockchain'e Deploy Etme

### 1. Ganache Kurulumu ve Başlatma

1. **Ganache'ı indir ve kur:**
   - https://trufflesuite.com/ganache/ adresinden indirin
   - Windows için `.exe` dosyasını çalıştırın

2. **Ganache'ı başlat:**
   - "Quickstart" ile hızlı başlatma yapın
   - Veya "New Workspace" ile özel ayarlar yapın
   - **Port:** 7545 (varsayılan)
   - **Chain ID:** 1337 veya 5777 (Ganache versiyonuna göre değişir)

3. **Test hesapları:**
   - Ganache otomatik olarak 10 test hesabı oluşturur
   - Her hesapta 100 ETH test bakiyesi vardır
   - İlk hesabı (Account 1) Owner olarak kullanacağız

### 2. Remix IDE ile Deploy Etme (Önerilen - Kolay Yöntem)

#### Adım 1: Remix'i Aç
- Tarayıcıda https://remix.ethereum.org adresine gidin

#### Adım 2: Kontratı Yükle
1. Sol panelde "File Explorer" sekmesine tıklayın
2. "contracts" klasörüne sağ tıklayın → "New File"
3. Dosya adı: `RestaurantPayment.sol`
4. `RestaurantPayment.sol` dosyasının içeriğini yapıştırın

#### Adım 3: Compile Et
1. Sol panelde "Solidity Compiler" sekmesine tıklayın
2. Compiler Version: `0.8.0` veya üzeri seçin
3. "Compile RestaurantPayment.sol" butonuna tıklayın
4. Yeşil tik görünürse başarılı!

#### Adım 4: Deploy Et
1. Sol panelde "Deploy & Run Transactions" sekmesine tıklayın
2. **Environment:** "Injected Provider - MetaMask" seçin
3. MetaMask'ı Ganache'a bağlayın:
   - MetaMask → Settings → Networks → Add Network
   - Network Name: `Ganache Local`
   - RPC URL: `http://127.0.0.1:7545`
   - Chain ID: `1337` (veya Ganache'ta gösterilen ID)
   - Currency Symbol: `ETH`
4. MetaMask'ta Ganache hesabını import edin:
   - Ganache'ta Account 1'in "key" ikonuna tıklayın
   - Private Key'i kopyalayın
   - MetaMask → Import Account → Private Key yapıştırın
5. Remix'te "Deploy" butonuna tıklayın
6. MetaMask'ta işlemi onaylayın

#### Adım 5: Contract Address ve ABI Al
1. Remix'in alt panelinde "Deployed Contracts" bölümünde kontratınızı göreceksiniz
2. **Contract Address:** Kontratın yanındaki kopyala ikonuna tıklayın
   - Örnek: `0x1234567890123456789012345678901234567890`
3. **ABI:** 
   - Sol panelde "Solidity Compiler" sekmesine gidin
   - "ABI" butonuna tıklayın
   - Tüm JSON'u kopyalayın

### 3. Truffle ile Deploy Etme (Gelişmiş Yöntem)

#### Adım 1: Truffle Kurulumu
```bash
npm install -g truffle
```

#### Adım 2: Proje Oluştur
```bash
mkdir restaurant-payment-contract
cd restaurant-payment-contract
truffle init
```

#### Adım 3: Kontratı Kopyala
- `contracts/RestaurantPayment.sol` dosyasına kontratı yapıştırın

#### Adım 4: Migration Dosyası Oluştur
`migrations/2_deploy_restaurant_payment.js`:
```javascript
const RestaurantPayment = artifacts.require("RestaurantPayment");

module.exports = function (deployer) {
  deployer.deploy(RestaurantPayment);
};
```

#### Adım 5: Truffle Config
`truffle-config.js`:
```javascript
module.exports = {
  networks: {
    ganache: {
      host: "127.0.0.1",
      port: 7545,
      network_id: "*" // Match any network id
    }
  },
  compilers: {
    solc: {
      version: "0.8.0"
    }
  }
};
```

#### Adım 6: Deploy Et
```bash
truffle migrate --network ganache
```

#### Adım 7: Contract Address ve ABI Al
- Contract Address: Migration çıktısında görünecek
- ABI: `build/contracts/RestaurantPayment.json` dosyasındaki `abi` alanı

### 4. Front-End'e Contract Address Ekleme

`RestoranOtomasyonu.WebAPI/wwwroot/js/app.js` dosyasında:

```javascript
const CONTRACT_ADDRESS = "0xYOUR_CONTRACT_ADDRESS"; // Buraya deploy ettiğiniz adresi yapıştırın
```

### 5. ABI Güncelleme (Opsiyonel)

Eğer tam ABI kullanmak isterseniz, `CONTRACT_ABI` değişkenini Remix'ten aldığınız ABI JSON'u ile değiştirin:

```javascript
const CONTRACT_ABI = [
    // Remix'ten aldığınız tam ABI JSON'u buraya yapıştırın
];
```

## 🔍 Test Etme

### 1. MetaMask'ı Ganache'a Bağlayın
- MetaMask → Networks → Ganache Local seçin

### 2. Test ETH Alın
- Ganache'ta Account 1'de zaten 100 ETH var
- MetaMask'ta import ettiğiniz hesapta da görünecek

### 3. Web Uygulamasında Test Edin
- "Kendi Payımı Öde" butonuna tıklayın
- MetaMask'ta işlemi onaylayın
- Transaction hash'i kontrol edin

### 4. Ganache'ta Transaction'ı Görün
- Ganache arayüzünde "Transactions" sekmesinde işleminizi görebilirsiniz

## 📝 Önemli Notlar

- **Gas Limit:** Ganache'ta gas limiti yoktur, istediğiniz kadar gönderebilirsiniz
- **Gas Price:** Ganache'ta gas price 0 olabilir (test için)
- **Chain ID:** Ganache versiyonuna göre 1337 veya 5777 olabilir
- **RPC URL:** Ganache varsayılan portu 7545'tir

## 🐛 Sorun Giderme

### Problem: MetaMask "Unrecognized chain" hatası veriyor
**Çözüm:** Ganache Chain ID'sini doğru eklediğinizden emin olun (1337 veya 5777)

### Problem: Transaction başarısız oluyor
**Çözüm:** 
- Ganache'ın çalıştığından emin olun
- MetaMask'ta yeterli ETH bakiyesi olduğunu kontrol edin
- Contract Address'in doğru olduğunu kontrol edin

### Problem: Contract fonksiyonu çalışmıyor
**Çözüm:**
- ABI'nin doğru olduğundan emin olun
- Contract Address'in doğru olduğundan emin olun
- Browser console'da hata mesajlarını kontrol edin

