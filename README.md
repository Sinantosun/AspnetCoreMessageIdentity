Merhaba,

<h1>Asp net core 8.0 Identity Mesajlaşma Uygulaması</h1>

Bu projede, kullanıcılar kendi aralarında mesaj gönderebilir, gönderilen mesajlar yanıtlanabilir ve mesajları başka kullanıcılara iletebilir,
mesajları taslak olarak kaydedebilir, mesajlarını çöp kutusuna taşıyabilir, gönderdikleri mesajları ve kendisine gelen önemli mesajları görüntüleyebilir.

<h1>Identity Kütüphanesi</h1>

![image](https://github.com/Sinantosun/AspnetCoreMessageIdentity/assets/145317724/4f2899c9-7d66-404c-b44c-f30b3cfde419)
📌 Identity kütüphanesi içinde hazır olarak bir çok tablo ve bu tablolara ait propertyler barındırır.
    <ul>
        <li>AspNetUsers : bu tablo kayıtlı kullanıcıların tutulacağı otomatik olarak gelen tablodur içinde email telefon numarası şifre kullanıcı adı gibi veriler yer alır bu tabloya ayrıca kendi istediğimiz verilerin eklenmesi de mümkündür.</li><br>
        <li>AspNetRoles : bu tablo kullanıcılara rol atama işlemlerinde rol adlarının belirlenmesinde kullanılmaktadır.</li><br>
        <li>AspNetUserRoles : Son olarak bu tablo  ise yukarda bahsettiğim iki tablo ile ilişkilidir ve hangi kullanıcının hangi rolu olduğu buradan anlaşılır</li><br>
    </ul>
📌 Projenin güvenlik tarafında kullanılmıştır.<br>br>
📌 Kullanıcılar 5 defa hatalı giriş yaptığında sistem tarafından otomatik olarak 5 dakika boyunca engellenir.<br>br>

📌 AspNetCore da AppUser ve AppRole olarak iki tablo oluşturulursa bu oluşturulan iki tablonunun da id değerlerinin aynı olması gereklidir yani aslında AppUser tablosunun id özelliği int ise AppRole tablosunun da int olmalıdır bu işlem tablolar arasında ilişki sağlanabilmesi için önemlidir.

<h1>SignalR Kütühanesi</h1>
<h3>SignalR Nedir ?</h3>
📌 SignalR, gerçek zamanlı uygulamalar geliştirmek için yazılmış açık kaynak kodlu bir .NET kütüphanedir. Normal HTTP bağlantılarında client-server iletişimi her istekte yenilenirken, SignalR ile client ve server arasında sürekli bir bağlantı sağlanır.<br><br>
📌 Aşağıdaki Görsel de bir kullanıcı giriş yaptıktan sonra aktif olan bütün kullanıcılara görseldeki gibi bilidirm gitmektedir.<br><br>
📌 Bu olay mesaj gönderme işleminde de kullanılmıştır, kullanıcı mesaj gönderdiğinde sadece mesajın gideceği kişinin tarayıcısında "Yeni Mesajınız Var" şeklinde bildirim gidecektir.<br><br>

![image](https://github.com/Sinantosun/AspnetCoreMessageIdentity/assets/145317724/8a306cd0-6b21-4959-978c-6d8dadcc3f95)

<h1>AutoMapper Kütüphanesi</h1>
<h3>AutoMapper Nedir ?</h3>

📌 AutoMapper; farklı veri nesnelerinin otomatik olarak eşleştirilerek dönüştürülmesini ve kopyalanmasını kolaylaştıran bir kütüphanedir. <br><br>
    📌 (örneğin mail adında bir entitymizin olduğunu ve bu entitynin birden cok propertysi olduğunu düşünelim CreateMailViewModel adında bir sınıf oluşturduğumuzda bu sınıfa bütün propertyleri tek tek atamamız gereklidir. AutoMapper işte tam burada kullanılır
        birden cok nesneyi tek tek atamak yerine otomatik olarak verdiğimiz sınıflari birbiyle eşler.<br><br>
        Dikkat ! : verilerin eşitlenmesi için entity sınıfı ve oluşturulan ViewModel/Dto sınıflarının propertylerinin birebir aynı olması gereklidir aksi halde eşitleme işlemi başarıylı olamaz.<br><br>
📌 mesaj gönderilirken, yanıtlanırken ve iletilirken automaper kullanılmıştır.

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









