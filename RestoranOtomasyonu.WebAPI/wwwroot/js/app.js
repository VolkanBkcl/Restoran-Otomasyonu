// Global Variables
let currentUser = null;
let currentMasaId = null;
let cart = [];
let API_BASE_URL = window.location.origin; // API base URL

// Sayfa yüklendiğinde
document.addEventListener('DOMContentLoaded', function() {
    // URL'den masa ID'sini al
    const urlParams = new URLSearchParams(window.location.search);
    currentMasaId = urlParams.get('id') || getMasaIdFromPath();
    
    if (!currentMasaId) {
        showToast('Masa ID bulunamadı!', 'error');
        return;
    }

    // LocalStorage'dan kullanıcı bilgisini kontrol et
    const savedUser = localStorage.getItem('currentUser');
    if (savedUser) {
        currentUser = JSON.parse(savedUser);
        showOrderScreen();
        loadMenu();
        loadMyOrders();
        loadTableOrders();
    }
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
    document.getElementById('authScreen').style.display = 'flex';
    document.getElementById('orderScreen').style.display = 'none';
    document.getElementById('loginForm').reset();
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
        loadMyOrders();
    } else if (tabName === 'tableSummary') {
        loadTableOrders();
    }
}

// ========== MENU FUNCTIONS ==========

async function loadMenu() {
    showLoading(true);
    try {
        const response = await fetch(`${API_BASE_URL}/api/menu`);
        const data = await response.json();

        if (data.success) {
            displayMenu(data.data);
            // Kategorileri filtre dropdown'una ekle (eğer varsa)
            // Bu kısım API'den kategori listesi gelirse eklenebilir
        } else {
            showToast('Menü yüklenemedi!', 'error');
        }
    } catch (error) {
        showToast('Bir hata oluştu: ' + error.message, 'error');
    } finally {
        showLoading(false);
    }
}

function displayMenu(items) {
    allMenuItems = items; // Filtreleme için sakla
    const menuGrid = document.getElementById('menuGrid');
    menuGrid.innerHTML = '';

    if (items.length === 0) {
        menuGrid.innerHTML = '<p class="empty-cart">Ürün bulunamadı.</p>';
        return;
    }

    items.forEach(item => {
        const menuItem = document.createElement('div');
        menuItem.className = 'menu-item';
        const itemName = (item.urunAdi || item.menuAdi || 'Ürün').replace(/'/g, "\\'");
        menuItem.innerHTML = `
            <img src="${item.resim || 'data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMjAwIiBoZWlnaHQ9IjIwMCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48cmVjdCB3aWR0aD0iMjAwIiBoZWlnaHQ9IjIwMCIgZmlsbD0iI2Y4ZjlmYSIvPjx0ZXh0IHg9IjUwJSIgeT0iNTAlIiBmb250LWZhbWlseT0iQXJpYWwiIGZvbnQtc2l6ZT0iMTgiIGZpbGw9IiM5OTkiIHRleHQtYW5jaG9yPSJtaWRkbGUiIGR5PSIuM2VtIj5SZXNpbSBZb2s8L3RleHQ+PC9zdmc+'}" 
                 alt="${itemName}" 
                 onerror="this.src='data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMjAwIiBoZWlnaHQ9IjIwMCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48cmVjdCB3aWR0aD0iMjAwIiBoZWlnaHQ9IjIwMCIgZmlsbD0iI2Y4ZjlmYSIvPjx0ZXh0IHg9IjUwJSIgeT0iNTAlIiBmb250LWZhbWlseT0iQXJpYWwiIGZvbnQtc2l6ZT0iMTgiIGZpbGw9IiM5OTkiIHRleHQtYW5jaG9yPSJtaWRkbGUiIGR5PSIuM2VtIj5SZXNpbSBZb2s8L3RleHQ+PC9zdmc+'};">
            <h3>${itemName}</h3>
            <div class="price">${item.birimFiyati.toFixed(2)} ₺</div>
            <button class="btn btn-primary" onclick="addToCart(${item.id}, ${item.isMenu ? 'null' : item.id}, ${item.isMenu ? item.id : 'null'}, '${itemName}', ${item.birimFiyati})">
                🛒 Sepete Ekle
            </button>
        `;
        menuGrid.appendChild(menuItem);
    });
}

function addToCart(id, urunId, menuId, name, price) {
    const existingItem = cart.find(item => 
        (item.urunId && item.urunId === urunId) || 
        (item.menuId && item.menuId === menuId)
    );

    if (existingItem) {
        existingItem.miktari++;
    } else {
        cart.push({
            id: id,
            urunId: urunId || 0,
            menuId: menuId || 0,
            name: name,
            price: price,
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
        const response = await fetch(`${API_BASE_URL}/api/order/create`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(orderData)
        });

        const data = await response.json();

        if (data.success) {
            showToast('Sipariş başarıyla oluşturuldu!', 'success');
            cart = [];
            updateCart();
            loadMyOrders();
            loadTableOrders();
        } else {
            showToast(data.message || 'Sipariş oluşturulamadı!', 'error');
        }
    } catch (error) {
        showToast('Bir hata oluştu: ' + error.message, 'error');
    } finally {
        showLoading(false);
    }
}

// ========== ORDERS FUNCTIONS ==========

async function loadMyOrders() {
    if (!currentUser) return;

    showLoading(true);
    try {
        const response = await fetch(`${API_BASE_URL}/api/order/my/${currentUser.id}`);
        const data = await response.json();

        if (data.success) {
            displayMyOrders(data.data);
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
        ordersList.innerHTML = '<p>Henüz siparişiniz yok.</p>';
        return;
    }

    orders.forEach(order => {
        const orderCard = document.createElement('div');
        orderCard.className = 'order-card';
        
        const odemeDurumu = getOdemeDurumuText(order.odemeDurumu);
        const odemeDurumuClass = getOdemeDurumuClass(order.odemeDurumu);
        const canPay = order.odemeDurumu === 0; // Odenmedi

        orderCard.innerHTML = `
            <h4>Sipariş #${order.satisKodu}</h4>
            <div class="order-info">
                <span>Masa: ${order.masaAdi || order.masaId}</span>
                <span class="order-status ${odemeDurumuClass}">${odemeDurumu}</span>
            </div>
            <div class="order-info">
                <span>Tutar: ${order.tutar.toFixed(2)} ₺</span>
                <span>Net Tutar: ${order.netTutar.toFixed(2)} ₺</span>
            </div>
            <div class="order-info">
                <span>Tarih: ${new Date(order.tarih).toLocaleString('tr-TR')}</span>
            </div>
            ${canPay ? `<button onclick="payOrder(${order.id}, '${order.satisKodu}')" class="btn btn-success" style="margin-top: 10px; width: 100%;">Kendi Payımı Öde (${order.netTutar.toFixed(2)} ₺)</button>` : ''}
        `;
        ordersList.appendChild(orderCard);
    });
}

async function loadTableOrders() {
    if (!currentMasaId) return;

    showLoading(true);
    try {
        const response = await fetch(`${API_BASE_URL}/api/order/table/${currentMasaId}`);
        const data = await response.json();

        if (data.success) {
            displayTableOrders(data.data);
        } else {
            showToast('Masa siparişleri yüklenemedi!', 'error');
        }
    } catch (error) {
        showToast('Bir hata oluştu: ' + error.message, 'error');
    } finally {
        showLoading(false);
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

    orders.forEach(order => {
        subtotal += order.netTutar;
        uniqueUsers.add(order.kullaniciId || order.kullaniciAdi);

        const orderCard = document.createElement('div');
        orderCard.className = 'order-card';
        
        const odemeDurumu = getOdemeDurumuText(order.odemeDurumu);
        const odemeDurumuClass = getOdemeDurumuClass(order.odemeDurumu);

        orderCard.innerHTML = `
            <h4>${order.adSoyad || order.kullaniciAdi}</h4>
            <div class="order-info">
                <span>Sipariş: #${order.satisKodu}</span>
                <span class="order-status ${odemeDurumuClass}">${odemeDurumu}</span>
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
    document.getElementById('orderCount').textContent = orders.length;
    document.getElementById('personCount').textContent = uniqueUsers.size;
}

async function payOrder(siparisId, satisKodu) {
    if (!currentUser) {
        showToast('Lütfen giriş yapın!', 'error');
        return;
    }

    if (!confirm('Kendi payınızı ödemek istediğinize emin misiniz?')) {
        return;
    }

    showLoading(true);

    const payData = {
        kullaniciId: currentUser.id,
        satisKodu: satisKodu,
        odemeTuru: 'Nakit'
    };

    try {
        const response = await fetch(`${API_BASE_URL}/api/order/pay/${siparisId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(payData)
        });

        const data = await response.json();

        if (data.success) {
            showToast('Ödeme başarıyla tamamlandı!', 'success');
            loadMyOrders();
            loadTableOrders();
        } else {
            showToast(data.message || 'Ödeme yapılamadı!', 'error');
        }
    } catch (error) {
        showToast('Bir hata oluştu: ' + error.message, 'error');
    } finally {
        showLoading(false);
    }
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

let allMenuItems = [];

function filterMenu() {
    const searchTerm = document.getElementById('menuSearch').value.toLowerCase();
    const categoryFilter = document.getElementById('menuCategoryFilter').value;

    const filtered = allMenuItems.filter(item => {
        const matchesSearch = (item.urunAdi || item.menuAdi || '').toLowerCase().includes(searchTerm);
        const matchesCategory = !categoryFilter || item.kategoriId == categoryFilter;
        return matchesSearch && matchesCategory;
    });

    displayMenu(filtered);
}

// ========== ORDER FILTER ==========

function filterMyOrders(filter) {
    const filterButtons = document.querySelectorAll('.filter-btn');
    filterButtons.forEach(btn => btn.classList.remove('active'));
    event.target.classList.add('active');

    // Bu fonksiyon loadMyOrders'dan sonra çağrılacak
    // Şimdilik sadece UI güncellemesi yapıyoruz
    loadMyOrders();
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

