# Telefon Rehberi

 

Bu projede mikroservis mimarisi kullanılarak demo bir telefon rehberi uygulaması geliştirilmiştir.

 

 

## Kullanılan Teknolojiler

 

- .Net Core 6.0

- EntityFramework Core

- Postgresql

- Fluentvalidation

- RabbitMQ

- Swagger

- Mapster

- Git

## Mimari Tasarım

&nbsp;&nbsp;&nbsp;&nbsp;Uygulama oluşturulurken TelefonRehberi.APIGateway servis projesi istekli karşılayıp diğer servislere gönderecek şekilde gateway olarak konumlandırıldı. Servislerin haberleşmesi  HTTP üzerinden REST ile .Net' in HttpClient sınıfları ile istek atarak sağlandı.

 

&nbsp;&nbsp;&nbsp;&nbsp;Gateway servisine hizmet edecek üç ayrı servis projesi oluşturuldu.

 

- Kişi bilgilerini işleyene kişi servisi

- İletişim bilgilerini işleyen iletişim servisi

- Rapor taleplerini işleyen rapor servisi

## Çalışma Akışı

&nbsp;&nbsp;&nbsp;&nbsp;TelefonRehberi.APIGateway servisinden gelen istekler mikroservisler tarafından ele alınmaktadır. Veriler EntityFrameworkCore ile PostgreSQL veri tabanına kaydedilmektedir. Gateway servisi ve diğer servisler HttpClient ile rest üzerinden haberleşmektedir.

&nbsp;&nbsp;&nbsp;&nbsp;Rapor taleplerinin servisleri tıkamaması için gelen talepler gateway servisi tarafından RabbitMQ sunucusuna gönderilmekte ve kuyrukta olan talepler rapor servisi tarafından tüketilerek raporlar oluşturulup raporbilgi tablosuna yazılmaktadır.

## Kurulum ve Başlatma
- Uygulama çalıştırılmadan önce RabbitMQ sunucusu kurulmalı ya da docker üzerinden ayağa kaldırılmalıdır. Docker kullanmak için "docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.12-management" komutu kullanılabilir. Fiziksel olarka kurmak için https://www.rabbitmq.com/download.html adresini ziyaret ediniz.
- Her servis için ayrı bir veri tabanı kullanılmıştır. Servislerin veri kendi veri tabanlarına bağlanabilmesi için PostgreSQL veri tabanında kişi servisi için "kisi_db_user", iletişim servisi için "iletisim_db_user", rapor servisi için "rapor_db_user" kullanıcıları tanımlanmalı ve login ve veri tabanı oluşturma yetkileri verilmelidir.
- Uygulama çalışırken migration değişikliklerini otomatik olarak her servis kendisi migration manager sınıfları ile veri tabanına yansıtacaktır.
- Uygulama çalıştırıldıktan sonra Gateway servisine https://localhost:7210/ adresi üzerinden ulaşılabilir. 


## API Dökümantasyonu

- API dökümantasyonu için Swagger kullanılmıştır.

- api/v1/telefon-rehberi-gateway/kisi end point i http get isteği yapıldığında kişi listesini getirir.
- api/v1/telefon-rehberi-gateway/kisi end point i http post isteği ile kişi model gönderildiğinde kişi yeni bir kişi kaydeder.
- api/v1/telefon-rehberi-gateway/kisi/{id} end point i http delete isteği yapıldığında ilgili kişi id ile kişiyi siler
- api/v1/telefon-rehberi-gateway/iletisim end point i htt post isteği ile iletişim model gönderildiğinde iletişim bilgisi kaydeder.
- api/v1/telefon-rehberi-gateway/iletisim/{id} end point i http get isteiği ile kişi id verildiğinde kişinin iletişim bilgilerini getirir.
- api/v1/telefon-rehberi-gateway/rapor end point i http post isteği ile aldığı konum bilgisi ile rapor kaydeder ve rapor id ile rapor talebini rabbitmq sunucusuna gönderir.
- api/v1/telefon-rehberi-gateway/rapor http get istepi ile raporların listesini getirir.
- api/v1/telefon-rehberi-gateway/rapor/{raporId:guid} http get isteği ile rapor id verildiğinde rapor talep edilirken oluşturulan raporun detay bilgilerini getirir.
- Rapor oluşturma işlemleri rapor servisinde background servis olarak çalışan RaporServiceConsumer sınıfı tarafından yapılmaktadır.

## Yapılabilecek İyileştirmeler
- Gateway olarak GraphQL veya Ocelot gibi daha gelişmiş yöntemler kullanılabilir.
- Mikroservisler arasında transaction yönetimi
- Hata yönetimi ve loglama