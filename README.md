Merhaba,

<h1>Asp net core 8.0 Identity Mesajlaşma Uygulaması</h1>

Bu projede, kullanıcılar kendi aralarında mesaj gönderebilir, gönderilen mesajlar yanıtlanabilir ve mesajları başka kullanıcılara iletebilir,
mesajları taslak olarak kaydedebilir, mesajlarını çöp kutusuna taşıyabilir, gönderdikleri mesajları ve kendisine gelen önemli mesajları görüntüleyebilir.

<h1>Dashboard Alanı</h1>

![image](https://github.com/Sinantosun/AspnetCoreMessageIdentity/assets/145317724/7f6b27a2-7d46-4f28-a4a0-5b7d695428f2)

📌 Her kullanıcının Sistem akışını izlediği mesajlarını alandır.
📌 Giriş yapan kullanıcının gönderdiği son 5 mesajın durumu sağ alanda yer alır mesaj durumu kullanıcının mesaja tıkladığında bu alanda iletildi yerine görüldü yazmaktadır.

<h1>Mesaj Gönderme Alanı </h1>

![image](https://github.com/Sinantosun/AspnetCoreMessageIdentity/assets/145317724/29933925-29b5-4eec-9c1c-7bcd654de9b7)

📌 Mesaj gönderme işlemi kayıtlı kullanıcıların email adreslerine göre işlemektedir.
📌 Mesaj içeriğini süslemek ve daha modern bir görünüme kavuşturmak için summerNote ile textarea kullanılmıştır.

<h1>Mesajlarım Alanı</h1>

![image](https://github.com/Sinantosun/AspnetCoreMessageIdentity/assets/145317724/89641432-90de-47af-afb8-741cfcc3c452)

📌 Mesajın kimden geldiği, mesajın başlığı, kategorisi önemli olarak işaretlendiği zaman önemli seviye mesaj yazılması, tarihi ekli dosya paylaşıldğında ataç sembolunun cıkması mesaj yanıtlandığında yanıtlandı olarak iletildiğinde iletildi olarak gösterilmesi burada sağlanır.
📌 Sol tarafda bulunan klasörler sayesinde kullanıcı mesajları okundu olarak işaretliyebilir, çöp kutusuna taşıyabilir gönderdiği mesajları görüntüleyebilir taslak mesajlarını görüntüleyebilir.

<h1>Gönderilenler Alanı</h1>

![image](https://github.com/Sinantosun/AspnetCoreMessageIdentity/assets/145317724/701de735-407e-4553-aa56-20b5eded2693)

📌 Kullanıcının göndermiş olduğu mesajlar burada görünür.

<h1>Önemli Mesajlar Alanı </h1>

![image](https://github.com/Sinantosun/AspnetCoreMessageIdentity/assets/145317724/49314e2d-766d-486a-b7dc-66050967ce58)

📌 Gönderilen mesajların önemli olarak işaretlenmesi durumunda mesaj bu klasöre ve ana klasore düşmektedir.

<h1>Taslak Alanı </h1>

![image](https://github.com/Sinantosun/AspnetCoreMessageIdentity/assets/145317724/e75a7a61-9378-4fc9-a5c7-813d8e43138c)

📌 Kullanıcı mesaj göndermekten vazgeçmesi durumunda mesajını taslak olarak kayıt edebilir.

<h1>Çöp Kutusu Alanı</h1>

![image](https://github.com/Sinantosun/AspnetCoreMessageIdentity/assets/145317724/df5bad34-a5d0-489e-8c28-5a2b2b58ce5c)

📌 Kullanıcı sildiği mesajları buradan görüntüler.

İlk kısım bitti :)

Projede kalan eksikleri tamamlayıp, SignalR Kütüphanesini dahil edeceğim.

SignalR ile oturum açılma işlemleri, mesaj gittikten sonra anlık bildirim gibi özellikleri ekliyeceğim.









