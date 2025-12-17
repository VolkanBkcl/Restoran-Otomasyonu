// Global Variables
let currentUser = null;
let currentMasaId = null;
let cart = [];
let API_BASE_URL = window.location.origin; // API base URL

// Blockchain Configuration (Ganache için)
const CONTRACT_ADDRESS = "0xYOUR_CONTRACT_ADDRESS"; // Ganache'tan deploy ettikten sonra buraya yapıştır
const CONTRACT_ABI = [
    "function payBill(uint256 orderId) payable",
    "event PaymentReceived(address indexed from, uint256 amount, uint256 indexed orderId, uint256 timestamp)"
];
const GANACHE_CHAIN_ID = 1337; // Ganache varsayılan Chain ID (veya 5777)
const ETH_TO_TL_RATE = 100000; // 1 ETH = 100.000 TL (test için sabit kur)

// Sayfa yüklendiğinde
document.addEventListener('DOMContentLoaded', function() {
    // URL'den masa ID'sini al (her sayfa yüklemesinde kontrol et)
    const urlParams = new URLSearchParams(window.location.search);
    const urlMasaId = urlParams.get('id') || getMasaIdFromPath();
    
    if (!urlMasaId) {
        showToast('Masa ID bulunamadı! Lütfen QR kodunu tekrar tarayın.', 'error');
        return;
    }

    // Masa ID'sini güncelle (URL'den gelen değer her zaman öncelikli)
    const previousMasaId = currentMasaId;
    currentMasaId = urlMasaId;
    
    // Eğer masa ID değiştiyse kullanıcıyı bilgilendir
    if (previousMasaId && previousMasaId !== currentMasaId) {
        console.log(`Masa ID değişti: ${previousMasaId} → ${currentMasaId}`);
        // Sepeti temizle (farklı masa için)
        cart = [];
        updateCart();
    }

    // LocalStorage'dan kullanıcı bilgisini kontrol et
    const savedUser = localStorage.getItem('currentUser');
    if (savedUser) {
        try {
            currentUser = JSON.parse(savedUser);
            showOrderScreen();
            loadMenu();
            loadMyOrders();
            loadTableOrders();
        } catch (error) {
            console.error('Kullanıcı bilgisi parse edilemedi:', error);
            localStorage.removeItem('currentUser');
            currentUser = null;
        }
    }
    
    console.log('Masa ID:', currentMasaId);
});

// Masa ID'sini path'ten al (örn: /masa/5)
function getMasaIdFromPath() {
    const path = window.location.pathname;
    const match = path.match(/\/masa\/(\d+)/);
    return match ? match[1] : null;
}

// ========== AUTH FUNCTIONS ==========

function showTab(tabName) {
    // Tab butonlarını güncelle (sadece login ve register için)
    if (tabName === 'login' || tabName === 'register') {
        document.querySelectorAll('.auth-tabs .tab-btn').forEach(btn => btn.classList.remove('active'));
        if (event && event.target) {
            event.target.classList.add('active');
        }
    }

    // Form'ları göster/gizle
    document.getElementById('loginTab').classList.remove('active');
    document.getElementById('registerTab').classList.remove('active');
    const forgotPasswordTab = document.getElementById('forgotPasswordTab');
    if (forgotPasswordTab) {
        forgotPasswordTab.classList.remove('active');
    }
    
    const targetTab = document.getElementById(tabName + 'Tab');
    if (targetTab) {
        targetTab.classList.add('active');
    }
    
    // Şifremi unuttum için tab butonlarını gizle
    if (tabName === 'forgotPassword') {
        document.getElementById('authTabsContainer').style.display = 'none';
    } else {
        document.getElementById('authTabsContainer').style.display = 'flex';
    }
}

async function handleLogin(event) {
    event.preventDefault();
    showLoading(true);

    const kullaniciAdi = document.getElementById('loginKullaniciAdi').value;
    const parola = document.getElementById('loginParola').value;

    try {
        const response = await fetch(`${API_BASE_URL}/api/auth/login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ kullaniciAdi, parola })
        });

        const data = await response.json();

        if (data.success) {
            currentUser = data.user;
            localStorage.setItem('currentUser', JSON.stringify(currentUser));
            
            // Masa ID'sini URL'den tekrar kontrol et (giriş sonrası)
            const urlParams = new URLSearchParams(window.location.search);
            const urlMasaId = urlParams.get('id') || getMasaIdFromPath();
            if (urlMasaId && urlMasaId !== currentMasaId) {
                currentMasaId = urlMasaId;
                console.log('Giriş sonrası masa ID güncellendi:', currentMasaId);
            }
            
            showToast('Giriş başarılı!', 'success');
            showOrderScreen();
            loadMenu();
            loadMyOrders();
            loadTableOrders();
        } else {
            showToast(data.message || 'Giriş başarısız!', 'error');
        }
    } catch (error) {
        showToast('Bir hata oluştu: ' + error.message, 'error');
    } finally {
        showLoading(false);
    }
}

async function handleRegister(event) {
    event.preventDefault();
    showLoading(true);

    const registerData = {
        adSoyad: document.getElementById('registerAdSoyad').value,
        telefon: document.getElementById('registerTelefon').value,
        email: document.getElementById('registerEmail').value,
        kullaniciAdi: document.getElementById('registerKullaniciAdi').value,
        parola: document.getElementById('registerParola').value,
        hatirlatmaSorusu: document.getElementById('registerHatirlatmaSorusu').value,
        cevap: document.getElementById('registerCevap').value
    };

    try {
        const response = await fetch(`${API_BASE_URL}/api/auth/register`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(registerData)
        });

        const data = await response.json();

        if (data.success) {
            showToast('Kayıt başarılı! Giriş yapabilirsiniz.', 'success');
            showTab('login');
            // Form'u temizle
            document.getElementById('registerForm').reset();
        } else {
            showToast(data.message || 'Kayıt başarısız!', 'error');
        }
    } catch (error) {
        showToast('Bir hata oluştu: ' + error.message, 'error');
    } finally {
        showLoading(false);
    }
}

function logout() {
    currentUser = null;
    localStorage.removeItem('currentUser');
    cart = [];
    // Masa ID'sini temizleme - URL'den tekrar alınacak
    // currentMasaId = null; // Bunu yapmıyoruz çünkü URL'den tekrar alınacak
    document.getElementById('authScreen').style.display = 'flex';
    document.getElementById('orderScreen').style.display = 'none';
    document.getElementById('loginForm').reset();
    updateCart(); // Sepeti temizle
}

// ========== ORDER SCREEN FUNCTIONS ==========

function showOrderScreen() {
    document.getElementById('authScreen').style.display = 'none';
    document.getElementById('orderScreen').style.display = 'block';
    document.getElementById('userName').textContent = currentUser.adSoyad || currentUser.kullaniciAdi;
    
    // Kullanıcı profil bilgilerini göster
    if (currentUser) {
        const initials = (currentUser.adSoyad || currentUser.kullaniciAdi || 'U')
            .split(' ')
            .map(n => n[0])
            .join('')
            .toUpperCase()
            .substring(0, 2);
        document.getElementById('userInitials').textContent = initials;
        document.getElementById('userFullName').textContent = currentUser.adSoyad || currentUser.kullaniciAdi;
        document.getElementById('userEmail').textContent = currentUser.email || 'Email bilgisi yok';
    }
    
    // Masa numarasını göster
    if (currentMasaId) {
        document.getElementById('tableNumber').textContent = currentMasaId;
    }
}

function showOrderTab(tabName) {
    // Tab butonlarını güncelle
    document.querySelectorAll('.order-tabs .tab-btn').forEach(btn => btn.classList.remove('active'));
    event.target.classList.add('active');

    // Tab içeriklerini göster/gizle
    document.querySelectorAll('.order-tab-content').forEach(tab => tab.classList.remove('active'));
    document.getElementById(tabName + 'Tab').classList.add('active');

    // İlgili verileri yükle
    if (tabName === 'myAccount') {
        stopTableSummaryUpdates();
        loadMyOrders();
    } else if (tabName === 'tableSummary') {
        loadTableOrders();
        startTableSummaryUpdates(); // Periyodik güncelleme başlat
    } else {
        stopTableSummaryUpdates();
    }
}

// ========== MENU FUNCTIONS ==========

let categories = [];
let currentCategoryId = null;
let allMenuItems = [];

async function loadMenu() {
    showLoading(true);
    try {
        // Önce kategorileri yükle
        const categoriesResponse = await fetch(`${API_BASE_URL}/api/menu/categories`);
        const categoriesData = await categoriesResponse.json();

        if (categoriesData.success) {
            categories = categoriesData.data;
            displayCategories(categories);
            
            // İlk kategoriyi seç ve ürünlerini yükle
            if (categories.length > 0) {
                currentCategoryId = categories[0].id;
                await loadProductsByCategory(currentCategoryId);
            }
        } else {
            showToast('Kategoriler yüklenemedi!', 'error');
        }
    } catch (error) {
        showToast('Bir hata oluştu: ' + error.message, 'error');
    } finally {
        showLoading(false);
    }
}

function displayCategories(cats) {
    const categoryTabs = document.getElementById('categoryTabs');
    categoryTabs.innerHTML = '';
    
    if (cats.length === 0) {
        return;
    }

    cats.forEach((category, index) => {
        const categoryBtn = document.createElement('button');
        categoryBtn.className = 'category-btn' + (index === 0 ? ' active' : '');
        categoryBtn.textContent = `${category.menuAdi} (${category.urunSayisi})`;
        categoryBtn.onclick = () => selectCategory(category.id);
        categoryTabs.appendChild(categoryBtn);
    });
}

async function selectCategory(categoryId) {
    currentCategoryId = categoryId;
    
    // Kategori butonlarını güncelle
    document.querySelectorAll('.category-btn').forEach(btn => btn.classList.remove('active'));
    event.target.classList.add('active');
    
    // Ürünleri yükle
    await loadProductsByCategory(categoryId);
}

async function loadProductsByCategory(categoryId) {
    showLoading(true);
    try {
        const response = await fetch(`${API_BASE_URL}/api/menu/products/${categoryId}`);
        const data = await response.json();

        if (data.success) {
            allMenuItems = data.data; // Filtreleme için sakla
            displayMenu(data.data);
        } else {
            showToast('Ürünler yüklenemedi!', 'error');
        }
    } catch (error) {
        showToast('Bir hata oluştu: ' + error.message, 'error');
    } finally {
        showLoading(false);
    }
}

function displayMenu(items) {
    const menuGrid = document.getElementById('menuGrid');
    menuGrid.innerHTML = '';

    if (items.length === 0) {
        menuGrid.innerHTML = '<p class="empty-cart">Bu kategoride ürün bulunamadı.</p>';
        return;
    }

    items.forEach(item => {
        const menuItem = document.createElement('div');
        menuItem.className = 'menu-item';
        const itemName = (item.urunAdi || item.menuAdi || 'Ürün').replace(/'/g, "\\'").replace(/"/g, '&quot;');
        const price = parseFloat(item.birimFiyati || 0);
        
        // Resim yolu kontrolü - eğer dosya yolu varsa API base URL'i ekle
        let imageSrc = item.resim || '';
        if (imageSrc && !imageSrc.startsWith('http') && !imageSrc.startsWith('data:')) {
            // Dosya yolu ise, API base URL'ini ekle
            imageSrc = `${API_BASE_URL}/${imageSrc}`;
        }
        if (!imageSrc || imageSrc === '') {
            imageSrc = 'data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMjAwIiBoZWlnaHQ9IjIwMCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48cmVjdCB3aWR0aD0iMjAwIiBoZWlnaHQ9IjIwMCIgZmlsbD0iI2Y4ZjlmYSIvPjx0ZXh0IHg9IjUwJSIgeT0iNTAlIiBmb250LWZhbWlseT0iQXJpYWwiIGZvbnQtc2l6ZT0iMTgiIGZpbGw9IiM5OTkiIHRleHQtYW5jaG9yPSJtaWRkbGUiIGR5PSIuM2VtIj5SZXNpbSBZb2s8L3RleHQ+PC9zdmc+';
        }
        
        menuItem.innerHTML = `
            <img src="${imageSrc}" 
                 alt="${itemName}" 
                 onerror="this.src='data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMjAwIiBoZWlnaHQ9IjIwMCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48cmVjdCB3aWR0aD0iMjAwIiBoZWlnaHQ9IjIwMCIgZmlsbD0iI2Y4ZjlmYSIvPjx0ZXh0IHg9IjUwJSIgeT0iNTAlIiBmb250LWZhbWlseT0iQXJpYWwiIGZvbnQtc2l6ZT0iMTgiIGZpbGw9IiM5OTkiIHRleHQtYW5jaG9yPSJtaWRkbGUiIGR5PSIuM2VtIj5SZXNpbSBZb2s8L3RleHQ+PC9zdmc+'">
            <h3>${itemName}</h3>
            ${item.aciklama ? `<p class="menu-description">${item.aciklama}</p>` : ''}
            <div class="price">${price.toFixed(2)} ₺</div>
            <button class="btn btn-primary" onclick="addToCart(${item.id}, ${item.id}, null, '${itemName}', ${price})">
                🛒 Sepete Ekle
            </button>
        `;
        menuGrid.appendChild(menuItem);
    });
}

function addToCart(id, urunId, menuId, name, price) {
    // urunId varsa urunId kullan, yoksa id'yi urunId olarak kullan
    const actualUrunId = urunId || id;
    
    const existingItem = cart.find(item => item.urunId === actualUrunId);

    if (existingItem) {
        existingItem.miktari++;
    } else {
        cart.push({
            id: id,
            urunId: actualUrunId,
            menuId: menuId || 0,
            name: name,
            price: parseFloat(price),
            miktari: 1
        });
    }

    updateCart();
    showToast(`${name} sepete eklendi!`, 'success');
}

function removeFromCart(index) {
    cart.splice(index, 1);
    updateCart();
}

function updateCart() {
    const cartItems = document.getElementById('cartItems');
    cartItems.innerHTML = '';

    if (cart.length === 0) {
        cartItems.innerHTML = '<p class="empty-cart">Sepetiniz boş</p>';
        document.getElementById('cartTotal').textContent = '0.00';
        document.getElementById('cartSubtotal').textContent = '0.00';
        document.getElementById('cartTax').textContent = '0.00';
        document.getElementById('cartCount').textContent = '0';
        document.getElementById('submitOrderBtn').disabled = true;
        return;
    }

    let subtotal = 0;
    const taxRate = 0.20; // %20 KDV (değiştirilebilir)

    cart.forEach((item, index) => {
        const itemTotal = item.price * item.miktari;
        subtotal += itemTotal;

        const cartItem = document.createElement('div');
        cartItem.className = 'cart-item';
        cartItem.innerHTML = `
            <div>
                <strong>${item.name}</strong>
                <div style="font-size: 0.9rem; color: #666;">${item.price.toFixed(2)} ₺ x ${item.miktari}</div>
            </div>
            <div style="text-align: right;">
                <strong>${itemTotal.toFixed(2)} ₺</strong>
                <button onclick="removeFromCart(${index})" class="btn btn-secondary" style="margin-left: 10px; padding: 5px 10px; font-size: 0.9rem;">✕</button>
            </div>
        `;
        cartItems.appendChild(cartItem);
    });

    const tax = subtotal * taxRate;
    const total = subtotal + tax;

    document.getElementById('cartSubtotal').textContent = subtotal.toFixed(2);
    document.getElementById('cartTax').textContent = tax.toFixed(2);
    document.getElementById('cartTotal').textContent = total.toFixed(2);
    document.getElementById('cartCount').textContent = cart.length;
    document.getElementById('submitOrderBtn').disabled = false;
}

async function submitOrder() {
    if (cart.length === 0) {
        showToast('Sepetiniz boş!', 'error');
        return;
    }

    if (!currentUser) {
        showToast('Lütfen giriş yapın!', 'error');
        return;
    }

    showLoading(true);

    const orderItems = cart.map(item => ({
        urunId: item.urunId || 0,
        menuId: item.menuId || 0,
        miktari: item.miktari,
        aciklama: ''
    }));

    const orderData = {
        masaId: parseInt(currentMasaId),
        kullaniciId: currentUser.id,
        items: orderItems
    };

    try {
        console.log('Sipariş gönderiliyor:', orderData);
        
        const response = await fetch(`${API_BASE_URL}/api/order/create`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(orderData)
        });

        // Response durumunu kontrol et
        if (!response.ok) {
            // HTTP hatası (400, 500 vb.)
            let errorMessage = `HTTP Hatası: ${response.status} ${response.statusText}`;
            
            try {
                // Sunucudan dönen hata mesajını okumaya çalış
                const errorText = await response.text();
                console.error('API Hatası (Text):', errorText);
                
                // JSON formatında mı kontrol et
                try {
                    const errorJson = JSON.parse(errorText);
                    errorMessage = errorJson.message || errorJson.error || errorText;
                    console.error('API Hatası (JSON):', errorJson);
                } catch {
                    // JSON değilse text olarak kullan
                    errorMessage = errorText || errorMessage;
                }
            } catch (parseError) {
                console.error('Hata mesajı okunamadı:', parseError);
            }
            
            showToast('Sipariş Hatası: ' + errorMessage, 'error');
            alert('Sipariş Hatası:\n' + errorMessage);
            return;
        }

        // Başarılı response - JSON parse et
        const data = await response.json();
        console.log('API Yanıtı:', data);

        if (data.success) {
            // Yeni akış: ödeme sayfasına yönlendir
            const siparisId = data.siparisId || data.orderId;
            if (siparisId) {
                showToast('Sipariş başarıyla oluşturuldu! Ödeme sayfasına yönlendiriliyorsunuz.', 'success');
                cart = [];
                updateCart();
                window.location.href = `${API_BASE_URL}/masa/odeme.html?orderId=${siparisId}&masaId=${currentMasaId}`;
            } else {
                console.error('Sipariş ID dönen veride bulunamadı', data);
                showToast('Sipariş oluşturuldu ancak ID alınamadı!', 'error');
                alert('Sipariş oluşturuldu ancak ID alınamadı. Lütfen destekle iletişime geçin.\n\nDönen veri: ' + JSON.stringify(data, null, 2));
            }
        } else {
            // API başarısız yanıt döndü
            const errorMsg = data.message || data.error || 'Sipariş oluşturulamadı!';
            console.error('Sipariş oluşturma hatası:', data);
            showToast(errorMsg, 'error');
            alert('Sipariş Hatası:\n' + errorMsg);
        }
    } catch (error) {
        // Network hatası veya beklenmeyen hata
        console.error('Sipariş oluşturma istisnası:', error);
        const errorMessage = error.message || 'Bilinmeyen bir hata oluştu';
        showToast('Bir hata oluştu: ' + errorMessage, 'error');
        alert('Bir hata oluştu:\n' + errorMessage + '\n\nDetay: ' + error.stack);
    } finally {
        showLoading(false);
    }
}

// ========== ORDERS FUNCTIONS ==========

async function loadMyOrders() {
    if (!currentUser) return;

    showLoading(true);
    try {
        // Sadece bu masadaki siparişleri getir
        const response = await fetch(`${API_BASE_URL}/api/order/my/${currentUser.id}?masaId=${currentMasaId}`);
        const data = await response.json();

        if (data.success) {
            allMyOrders = data.data; // Tüm siparişleri sakla
            displayFilteredMyOrders(); // Filtrelenmiş siparişleri göster
        } else {
            showToast('Siparişler yüklenemedi!', 'error');
        }
    } catch (error) {
        showToast('Bir hata oluştu: ' + error.message, 'error');
    } finally {
        showLoading(false);
    }
}

function displayMyOrders(orders) {
    const ordersList = document.getElementById('myOrdersList');
    ordersList.innerHTML = '';

    if (orders.length === 0) {
        const emptyMessage = myOrdersFilter === 'all' ? 'Henüz siparişiniz yok.' : 
                            myOrdersFilter === 'pending' ? 'Bekleyen siparişiniz yok.' : 
                            'Ödenen siparişiniz yok.';
        ordersList.innerHTML = `<p>${emptyMessage}</p>`;
        return;
    }

    orders.forEach(order => {
        const orderCard = document.createElement('div');
        orderCard.className = 'order-card';
        
        const odemeDurumu = getOdemeDurumuText(order.odemeDurumu);
        const odemeDurumuClass = getOdemeDurumuClass(order.odemeDurumu);
        const siparisDurumu = getSiparisDurumuText(order.siparisDurumu);
        const canPay = order.odemeDurumu === 0; // Odenmedi
        const canCancel = order.siparisDurumu === 0 || order.siparisDurumu === 1; // OnayBekliyor veya Hazirlaniyor

        orderCard.innerHTML = `
            <h4>Sipariş #${order.satisKodu}</h4>
            <div class="order-info">
                <span>Masa: ${order.masaAdi || order.masaId}</span>
                <span class="order-status ${odemeDurumuClass}">${odemeDurumu}</span>
            </div>
            <div class="order-info">
                <span>Durum: ${siparisDurumu}</span>
            </div>
            <div class="order-info">
                <span>Tutar: ${order.tutar.toFixed(2)} ₺</span>
                <span>Net Tutar: ${order.netTutar.toFixed(2)} ₺</span>
            </div>
            <div class="order-info">
                <span>Tarih: ${new Date(order.tarih).toLocaleString('tr-TR')}</span>
            </div>
            <div class="order-actions">
                ${canPay ? `<button onclick="payOrder(${order.id}, '${order.satisKodu}')" class="btn btn-success">Kendi Payımı Öde (${order.netTutar.toFixed(2)} ₺)</button>` : ''}
                ${canCancel ? `<button onclick="cancelOrder(${order.id}, '${order.satisKodu}')" class="btn btn-danger">Sipariş İptal Et</button>` : ''}
            </div>
        `;
        ordersList.appendChild(orderCard);
    });
}

function getSiparisDurumuText(durum) {
    switch (durum) {
        case 0: return 'Onay Bekliyor';
        case 1: return 'Hazırlanıyor';
        case 2: return 'Hazır';
        case 3: return 'Teslim Edildi';
        case 4: return 'İptal Edildi';
        default: return 'Bilinmiyor';
    }
}

async function cancelOrder(siparisId, satisKodu) {
    if (!confirm(`Sipariş #${satisKodu} iptal edilecek. Emin misiniz?`)) {
        return;
    }

    showLoading(true);
    try {
        const response = await fetch(`${API_BASE_URL}/api/order/cancel/${siparisId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ kullaniciId: currentUser.id })
        });

        const data = await response.json();

        if (data.success) {
            showToast('Sipariş iptal edildi!', 'success');
            loadMyOrders();
            loadTableOrders();
        } else {
            showToast(data.message || 'Sipariş iptal edilemedi!', 'error');
        }
    } catch (error) {
        showToast('Bir hata oluştu: ' + error.message, 'error');
    } finally {
        showLoading(false);
    }
}

let tableOrdersInterval = null;

async function loadTableOrders() {
    if (!currentMasaId) return;

    showLoading(true);
    try {
        // Masa siparişlerini getir
        const response = await fetch(`${API_BASE_URL}/api/order/table/${currentMasaId}`);
        const data = await response.json();

        if (data.success) {
            displayTableOrders(data.data);
        } else {
            showToast('Masa siparişleri yüklenemedi!', 'error');
        }

        // Masa özetini getir
        const summaryResponse = await fetch(`${API_BASE_URL}/api/order/table/${currentMasaId}/summary`);
        const summaryData = await summaryResponse.json();

        if (summaryData.success) {
            updateTableSummary(summaryData.data);
        }
    } catch (error) {
        showToast('Bir hata oluştu: ' + error.message, 'error');
    } finally {
        showLoading(false);
    }
}

function updateTableSummary(summary) {
    // Masa özeti istatistiklerini güncelle
    document.getElementById('personCount').textContent = summary.kullaniciSayisi || 0;
    document.getElementById('orderCount').textContent = summary.siparisSayisi || 0;
    
    // Toplam tutarı hesapla (KDV dahil)
    const taxRate = 0.20;
    const total = (summary.toplamTutar || 0) * (1 + taxRate);
    document.getElementById('tableTotalPreview').textContent = total.toFixed(2) + ' ₺';
}

// Masa özeti sekmesi açıldığında periyodik güncelleme başlat
function startTableSummaryUpdates() {
    if (tableOrdersInterval) {
        clearInterval(tableOrdersInterval);
    }
    
    // Her 5 saniyede bir güncelle
    tableOrdersInterval = setInterval(() => {
        if (document.getElementById('tableSummaryTab').classList.contains('active')) {
            loadTableOrders();
        }
    }, 5000);
}

// Masa özeti sekmesi kapatıldığında güncellemeyi durdur
function stopTableSummaryUpdates() {
    if (tableOrdersInterval) {
        clearInterval(tableOrdersInterval);
        tableOrdersInterval = null;
    }
}

function displayTableOrders(orders) {
    const ordersList = document.getElementById('tableOrdersList');
    ordersList.innerHTML = '';

    if (orders.length === 0) {
        ordersList.innerHTML = '<p class="empty-cart">Bu masada henüz sipariş yok.</p>';
        document.getElementById('tableTotal').textContent = '0.00';
        document.getElementById('tableSubtotal').textContent = '0.00';
        document.getElementById('tableTax').textContent = '0.00';
        document.getElementById('tableTotalPreview').textContent = '0.00 ₺';
        document.getElementById('orderCount').textContent = '0';
        document.getElementById('personCount').textContent = '0';
        return;
    }

    let subtotal = 0;
    const taxRate = 0.20; // %20 KDV
    const uniqueUsers = new Set();

    // Sadece onaylanmış siparişleri (OnayBekliyor, Hazirlaniyor, Hazır) say
    const confirmedOrders = orders.filter(order => 
        order.siparisDurumu === 0 || order.siparisDurumu === 1 || order.siparisDurumu === 2
    );

    confirmedOrders.forEach(order => {
        subtotal += order.netTutar;
        uniqueUsers.add(order.kullaniciId || order.kullaniciAdi);

        const orderCard = document.createElement('div');
        orderCard.className = 'order-card';
        
        const odemeDurumu = getOdemeDurumuText(order.odemeDurumu);
        const odemeDurumuClass = getOdemeDurumuClass(order.odemeDurumu);
        const siparisDurumu = getSiparisDurumuText(order.siparisDurumu);

        orderCard.innerHTML = `
            <h4>${order.adSoyad || order.kullaniciAdi || 'Kullanıcı'}</h4>
            <div class="order-info">
                <span>Sipariş: #${order.satisKodu}</span>
                <span class="order-status ${odemeDurumuClass}">${odemeDurumu}</span>
            </div>
            <div class="order-info">
                <span>Durum: ${siparisDurumu}</span>
            </div>
            <div class="order-info">
                <span>Tutar: ${order.tutar.toFixed(2)} ₺</span>
                <span>Net Tutar: ${order.netTutar.toFixed(2)} ₺</span>
            </div>
            <div class="order-info">
                <span>Tarih: ${new Date(order.tarih).toLocaleString('tr-TR')}</span>
            </div>
        `;
        ordersList.appendChild(orderCard);
    });

    const tax = subtotal * taxRate;
    const total = subtotal + tax;

    document.getElementById('tableSubtotal').textContent = subtotal.toFixed(2);
    document.getElementById('tableTax').textContent = tax.toFixed(2);
    document.getElementById('tableTotal').textContent = total.toFixed(2);
    document.getElementById('tableTotalPreview').textContent = total.toFixed(2) + ' ₺';
    document.getElementById('orderCount').textContent = confirmedOrders.length;
    document.getElementById('personCount').textContent = uniqueUsers.size;
}

/**
 * Ödeme sayfasına yönlendir
 */
async function payOrder(siparisId, satisKodu) {
    if (!currentUser) {
        showToast('Lütfen giriş yapın!', 'error');
        return;
    }

    // Ödeme sayfasına yönlendir
    window.location.href = `${API_BASE_URL}/masa/odeme.html?orderId=${siparisId}&masaId=${currentMasaId}`;
}

// ========== HELPER FUNCTIONS ==========

function getOdemeDurumuText(durum) {
    switch (durum) {
        case 0: return 'Ödenmedi';
        case 1: return 'Kendi Ödendi';
        case 2: return 'Tamamı Ödendi';
        default: return 'Bilinmiyor';
    }
}

function getOdemeDurumuClass(durum) {
    switch (durum) {
        case 0: return 'unpaid';
        case 1: return 'paid';
        case 2: return 'paid';
        default: return 'pending';
    }
}

function showLoading(show) {
    document.getElementById('loadingOverlay').style.display = show ? 'flex' : 'none';
}

// ========== FORGOT PASSWORD ==========

async function handleForgotPassword(event) {
    event.preventDefault();
    showLoading(true);

    const kullaniciAdi = document.getElementById('forgotKullaniciAdi').value;
    const securityQuestionGroup = document.getElementById('securityQuestionGroup');
    const newPasswordGroup = document.getElementById('newPasswordGroup');
    const forgotPasswordBtn = document.getElementById('forgotPasswordBtn');
    const securityQuestion = document.getElementById('securityQuestion');
    const cevap = document.getElementById('forgotCevap').value;
    const newPassword = document.getElementById('newPassword').value;

    try {
        // İlk adım: Kullanıcı adını kontrol et ve güvenlik sorusunu getir
        if (!securityQuestionGroup.style.display || securityQuestionGroup.style.display === 'none') {
            const response = await fetch(`${API_BASE_URL}/api/auth/forgot-password/check`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ kullaniciAdi })
            });

            const data = await response.json();
            if (data.success) {
                securityQuestion.textContent = data.securityQuestion || 'Güvenlik sorusu bulunamadı';
                securityQuestionGroup.style.display = 'block';
                forgotPasswordBtn.textContent = 'Cevabı Doğrula';
            } else {
                showToast(data.message || 'Kullanıcı bulunamadı!', 'error');
            }
        }
        // İkinci adım: Cevabı doğrula
        else if (!newPasswordGroup.style.display || newPasswordGroup.style.display === 'none') {
            const response = await fetch(`${API_BASE_URL}/api/auth/forgot-password/verify`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ kullaniciAdi, cevap })
            });

            const data = await response.json();
            if (data.success) {
                newPasswordGroup.style.display = 'block';
                forgotPasswordBtn.textContent = 'Şifreyi Güncelle';
            } else {
                showToast(data.message || 'Cevap yanlış!', 'error');
            }
        }
        // Üçüncü adım: Yeni şifreyi kaydet
        else {
            const response = await fetch(`${API_BASE_URL}/api/auth/forgot-password/reset`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ kullaniciAdi, newPassword })
            });

            const data = await response.json();
            if (data.success) {
                showToast('Şifreniz başarıyla güncellendi!', 'success');
                setTimeout(() => {
                    showTab('login');
                    document.getElementById('forgotPasswordForm').reset();
                    securityQuestionGroup.style.display = 'none';
                    newPasswordGroup.style.display = 'none';
                    forgotPasswordBtn.textContent = 'Devam Et';
                }, 2000);
            } else {
                showToast(data.message || 'Şifre güncellenemedi!', 'error');
            }
        }
    } catch (error) {
        showToast('Bir hata oluştu: ' + error.message, 'error');
    } finally {
        showLoading(false);
    }
}

// ========== MENU FILTER ==========
// allMenuItems yukarıda (loadMenu içinde) global olarak tanımlandı.
// Burada tekrar tanımlamıyoruz, sadece mevcut diziyi filtrelemede kullanıyoruz.
function filterMenu() {
    const searchTerm = document.getElementById('menuSearch').value.toLowerCase();

    const filtered = allMenuItems.filter(item => {
        const matchesSearch = (item.urunAdi || item.menuAdi || '').toLowerCase().includes(searchTerm);
        return matchesSearch;
    });

    displayMenu(filtered);
}

// ========== ORDER FILTER ==========

let myOrdersFilter = 'all';
let allMyOrders = [];

function filterMyOrders(filter) {
    myOrdersFilter = filter;
    const filterButtons = document.querySelectorAll('.filter-btn');
    filterButtons.forEach(btn => btn.classList.remove('active'));
    event.target.classList.add('active');

    // Filtrelenmiş siparişleri göster
    displayFilteredMyOrders();
}

function displayFilteredMyOrders() {
    let filtered = allMyOrders;

    if (myOrdersFilter === 'pending') {
        // Bekleyen: OnayBekliyor (0) veya Hazirlaniyor (1)
        filtered = allMyOrders.filter(order => 
            order.siparisDurumu === 0 || order.siparisDurumu === 1
        );
    } else if (myOrdersFilter === 'paid') {
        // Ödenen: OdemeDurumu == Odendi (1 veya 2)
        filtered = allMyOrders.filter(order => 
            order.odemeDurumu === 1 || order.odemeDurumu === 2
        );
    }
    // 'all' için tüm siparişler

    displayMyOrders(filtered);
}

// ========== HELPER FUNCTIONS ==========

function showToast(message, type = 'info') {
    const toast = document.getElementById('toast');
    toast.textContent = message;
    toast.className = `toast show ${type}`;
    
    setTimeout(() => {
        toast.classList.remove('show');
    }, 3000);
}

