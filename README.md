# Regula Falsi (Kirişler) Yöntemi Uygulaması

Matematik Mühendisliği 2. sınıf Nümerik Analiz dersi kapsamında, Regula Falsi (False Position) kök bulma yöntemini pekiştirmek ve görselleştirmek amacıyla geliştirdiğim C# WPF projesi.


## Projenin Amacı ve İşleyişi

Program, kullanıcıdan alınan bir matematiksel fonksiyonun, belirlenen kapalı aralıktaki kökünü iterasyon yöntemiyle bulmaktadır.

* **Matematiksel Fonksiyon Girişi:** Kullanıcı `sin(x) - x^2` veya `x*e^x - cos(x)` gibi ifadeleri metin kutusuna girebilir. Arka planda `mXparser` kütüphanesi bu string ifadeyi matematiksel fonksiyona çevirir.
* **Grafiksel Gösterim:** `ScottPlot` kütüphanesi kullanılarak fonksiyonun grafiği çizilir. Algoritmanın bulduğu her $c$ noktası (tahmini kök) grafik üzerinde kırmızı nokta ile işaretlenir. Bu sayede algoritmanın köke nasıl adım adım yaklaştığı görsel olarak takip edilebilir.
* **Adım Adım Hesaplama:** Program sadece son kök değerini vermez. Her iterasyon adımında $a$, $b$ ve $c$ noktalarının değerlerini, fonksiyonun bu noktalardaki karşılıklarını gösterir.
* **Hata Payı Kontrolü:** Kullanıcı, hesaplamanın ne kadar hassas olacağını ($10^{-2}$ ile $10^{-9}$ arasında) seçebilir.

## Teknik Detaylar

Proje .NET platformunda C# dili ve WPF arayüzü ile geliştirilmiştir.

* **Dil:** C#
* **Arayüz:** WPF
* **Kullanılan Kütüphaneler:**
    * MathParser.org-mXparser (Fonksiyon ayrıştırma işlemleri için)
    * ScottPlot (Veri görselleştirme ve grafik çizimi için)

## Kazanımlar

Bu proje, C# ve WPF üzerindeki yetkinliğimi pekiştirmemi sağladı. Proje geliştirme sürecini 10 günlük planlı bir takvim çerçevesinde tamamladım. Özellikle daha önce deneyimlemediğim ScottPlot gibi harici kütüphaneleri entegre etme tecrübesi kazandım. 


## 📸 Ekran Görüntüleri

<img width="979" height="551" alt="Screenshot 2026-01-31 184659" src="https://github.com/user-attachments/assets/5f13a3a0-1802-4af1-af89-5c6df59232a3" />

<img width="975" height="551" alt="Screenshot 2026-01-31 184728" src="https://github.com/user-attachments/assets/192d599d-3e22-4855-be2c-3d138355b20a" />

<img width="979" height="547" alt="Screenshot 2026-01-31 184736" src="https://github.com/user-attachments/assets/43b2e44b-0f16-42f9-aac3-caa133915be2" />



---
