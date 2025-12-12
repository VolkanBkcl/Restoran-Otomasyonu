// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

/**
 * @title RestaurantPayment
 * @dev Basit restoran ödeme akıllı kontratı
 * Ganache Local Blockchain için tasarlandı
 */
contract RestaurantPayment {
    address public owner;
    
    // Event: Ödeme alındığında yayınlanır
    event PaymentReceived(
        address indexed from,
        uint256 amount,
        uint256 indexed orderId,
        uint256 timestamp
    );
    
    // Event: Owner ETH çektiğinde yayınlanır
    event FundsWithdrawn(
        address indexed to,
        uint256 amount,
        uint256 timestamp
    );
    
    // Modifier: Sadece owner işlem yapabilir
    modifier onlyOwner() {
        require(msg.sender == owner, "Sadece owner işlem yapabilir");
        _;
    }
    
    // Constructor: Kontrat deploy edildiğinde owner'ı ayarla
    constructor() {
        owner = msg.sender;
    }
    
    /**
     * @dev Sipariş ödemesi yapılır
     * @param orderId Sipariş ID'si
     */
    function payBill(uint256 orderId) external payable {
        require(msg.value > 0, "Ödeme tutarı 0'dan büyük olmalı");
        require(orderId > 0, "Geçerli bir sipariş ID'si gerekli");
        
        // Event yayınla
        emit PaymentReceived(
            msg.sender,
            msg.value,
            orderId,
            block.timestamp
        );
    }
    
    /**
     * @dev Owner kontrat bakiyesini çekebilir
     */
    function withdraw() external onlyOwner {
        uint256 balance = address(this).balance;
        require(balance > 0, "Çekilecek bakiye yok");
        
        (bool success, ) = payable(owner).call{value: balance}("");
        require(success, "Para çekme işlemi başarısız");
        
        emit FundsWithdrawn(owner, balance, block.timestamp);
    }
    
    /**
     * @dev Kontrat bakiyesini görüntüle
     */
    function getBalance() external view returns (uint256) {
        return address(this).balance;
    }
    
    /**
     * @dev Owner adresini görüntüle
     */
    function getOwner() external view returns (address) {
        return owner;
    }
    
    // Fallback fonksiyonu: Direkt ETH gönderilirse reddet
    receive() external payable {
        revert("Lutfen payBill fonksiyonunu kullanin");
    }
}

