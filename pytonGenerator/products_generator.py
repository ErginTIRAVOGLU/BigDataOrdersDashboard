import csv
import random

# 18.000 Satır
TOTAL_RECORDS = 18000
FILENAME = "products.csv"

# Sabit Veriler
countries = ["Türkiye", "Spain", "France", "Germany", "Netherlands", "Italy"]
adjectives = ["Premium", "Yüksek Performanslı", "Ekonomik", "Profesyonel", "Yeni Sezon", "Ultra", "Dayanıklı", "Akıllı", "Kompakt", "Ergonomik", "Lüks", "Orijinal"]

# Kategori Bazlı Ürün Havuzu ve Fiyat Aralığı (Min, Max)
category_data = {
    1: (["4K Smart TV", "Bluetooth Hoparlör", "Dijital Kamera", "Projeksiyon Cihazı", "Ses Sistemi", "Akıllı Saat"], 2000, 40000), # Electronics
    2: (["Oyun Laptopu", "Mekanik Klavye", "Kablosuz Mouse", "27 inç Monitör", "SSD Disk", "Harici Harddisk", "USB Bellek"], 500, 60000), # Computers
    3: (["Akıllı Telefon", "Tablet Bilgisayar", "Hızlı Şarj Aleti", "Powerbank", "Telefon Kılıfı", "Ekran Koruyucu"], 300, 50000), # Smartphones
    4: (["Kahve Makinesi", "Blender", "Robot Süpürge", "Ütü", "Tost Makinesi", "Çay Makinesi", "Hava Temizleyici"], 500, 15000), # Home & Kitchen
    5: (["Köşe Koltuk", "Çalışma Masası", "Kitaplık", "Lambader", "Yemek Masası", "Ofis Sandalyesi", "Gardırop"], 1000, 25000), # Furniture
    6: (["Pamuklu T-Shirt", "Kot Pantolon", "Kışlık Mont", "Sweatshirt", "Gömlek", "Ceket", "Elbise"], 100, 3000), # Clothing
    7: (["Spor Ayakkabı", "Deri Bot", "Günlük Sneaker", "Topuklu Ayakkabı", "Terlik", "Sandalet"], 200, 5000), # Shoes
    8: (["Yüz Kremi", "Parfüm", "Şampuan", "Saç Serumu", "Makyaj Seti", "Güneş Kremi", "Tıraş Makinesi"], 50, 2000), # Beauty
    9: (["Yoga Matı", "Dambıl Seti", "Koşu Bandı", "Futbol Topu", "Kamp Çadırı", "Tenis Raketi", "Bisiklet"], 100, 10000), # Sports
    10: (["Yapboz (Puzzle)", "Uzaktan Kumandalı Araba", "Peluş Oyuncak", "Eğitici Oyun Seti", "Akülü Araba", "Lego Seti"], 100, 5000), # Toys
    11: (["Roman Kitabı", "Defter", "Dolma Kalem", "Bilim Kitabı", "Boyama Seti", "Akademik Ajanda"], 20, 500), # Books
    12: (["Motor Yağı", "Silecek Takımı", "Jant Kapağı", "Oto Paspas", "Araç İçi Kamera", "Lastik Parlatıcı"], 100, 4000), # Automotive
    13: (["Kedi Maması", "Köpek Tasması", "Akvaryum Motoru", "Kedi Kumu", "Kuş Kafesi", "Tırmalama Tahtası"], 50, 2000), # Pet Supplies
    14: (["Gümüş Kolye", "Akıllı Bileklik", "Deri Saat", "Altın Küpe", "Güneş Gözlüğü", "Tektaş Yüzük"], 500, 50000), # Jewelry
    15: (["Akustik Gitar", "Keman", "Elektro Gitar", "Davul Bageti", "Piyano", "Ukulele"], 300, 30000)  # Music
}

print(f"{TOTAL_RECORDS} adet ürün oluşturuluyor...")

with open(FILENAME, mode='w', newline='', encoding='utf-8-sig') as file:
    writer = csv.writer(file, delimiter=',') # SQL Import için virgül ayırıcı
    
    # Header (Başlıklar)
    writer.writerow(['ProductId', 'ProductName', 'ProductDescription', 'UnitPrice', 'StockQuantity', 'CategoryId', 'CountryOfOrigin', 'ProductImageUrl'])
    
    for i in range(1, TOTAL_RECORDS + 1):
        # Rastgele Kategori Seç (1-15)
        cat_id = random.randint(1, 15)
        
        # Kategoriye uygun isim ve fiyat aralığı al
        base_names, min_p, max_p = category_data[cat_id]
        
        # Rastgelelik türet
        base_name = random.choice(base_names)
        adjective = random.choice(adjectives)
        
        product_name = f"{adjective} {base_name}"
        
        # Açıklama oluştur
        description = f"{base_name} kategorisinde {adjective.lower()} kalite sunan, {random.choice(countries)} menşeli harika bir ürün."
        
        # Fiyat (Küsüratlı olması için)
        price = round(random.uniform(min_p, max_p), 2)
        
        # Stok
        stock = random.randint(0, 1000)
        
        # Menşei
        origin = random.choice(countries)
        
        # Resim URL (Seed kullanarak her ID için sabit ama unique resim)
        image_url = f"https://picsum.photos/seed/{i}/200"
        
        # Satırı yaz
        writer.writerow([i, product_name, description, price, stock, cat_id, origin, image_url])

print(f"İşlem tamam! '{FILENAME}' dosyası oluşturuldu.")