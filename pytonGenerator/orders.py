import csv
import random
from datetime import datetime, timedelta

# Ayarlar
TOTAL_RECORDS = 500000
FILENAME = r"C:\Temp\orders.csv" # C:\Temp klasörünün var olduğundan emin olun

# Veri Havuzları
payment_methods = ["Kredi Kartı", "Banka Kartı", "Kapıda Ödeme", "Nakit", "PayPal", "Apple Pay"]
order_statuses = ["Tamamlandı", "Beklemede", "İptal Edildi", "Hazırlanıyor"]
notes_list = [
    "Hediye paketi olsun", 
    "Lütfen zili çalmayın", 
    "Arka bahçeye bırakın", 
    "Hızlı teslimat rica ederim", 
    "Mesai saatleri içinde teslim", 
    "Hafta sonu teslim edilmesin", 
    "", "", "", "" # Boş not olasılığını artırmak için
]

# Tarih Aralığı 
end_date = datetime.now()
start_date = end_date - timedelta(days=1030)

def random_date(start, end):
    """İki tarih arasında rastgele bir zaman döndürür"""
    delta = end - start
    int_delta = (delta.days * 24 * 60 * 60) + delta.seconds
    random_second = random.randrange(int_delta)
    return start + timedelta(seconds=random_second)

print(f"{TOTAL_RECORDS} adet sipariş verisi oluşturuluyor... Lütfen bekleyin.")

with open(FILENAME, mode='w', newline='', encoding='utf-8-sig') as file:
    writer = csv.writer(file, delimiter=',')
    
    # Header (Identity Insert yapmayacağımız için OrderId'yi CSV'ye koymuyoruz, SQL otomatik artıracak)
    writer.writerow(['OrdersId', 'ProductId', 'CustomerId', 'Quantity', 'PaymentMethod', 'OrderStatus', 'OrderDate', 'OrderNotes'])
    
    for i in range(TOTAL_RECORDS):
        # Constraints
        # DİKKAT: Veritabanında gerçekten 2000 müşteri olduğundan emin ol. 
        # Eğer sadece 1000 müşteri eklediysen burayı random.randint(1, 1000) yapmalısın.
        orders_id = i + 1
        product_id = random.randint(1, 18000)
        customer_id = random.randint(1, 2000) 
        
        quantity = random.randint(1, 50)
        payment = random.choice(payment_methods)
        status = random.choice(order_statuses)
        
        # Tarih formatı: YYYY-MM-DD HH:MM:SS
        r_date = random_date(start_date, end_date).strftime("%Y-%m-%d %H:%M:%S")
        
        note = random.choice(notes_list)
        
        writer.writerow([orders_id, product_id, customer_id, quantity, payment, status, r_date, note])

        if (i + 1) % 50000 == 0:
            print(f"{i + 1} kayıt oluşturuldu...")

print(f"İşlem tamam! Dosya konumu: {FILENAME}")