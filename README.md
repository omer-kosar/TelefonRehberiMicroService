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
- Her servis için ayrı bir veri tabanı kullanılmıştır. Servislerin veri kendi veri tabanlarına bağlanabilmesi için PostgreSQL veri tabanında kişi servisi için "person_db_user", iletişim servisi için "iletişim_db_user", rapor servisi için "rapor_db_user" kullanıcıları tanımlanmalı ve login ve veri tabanı oluşturma yetkileri verilmelidir.
- Uygulama çalışırken migration değişikliklerini otomatik olarak her servis kendisi migration manager sınıfları ile veri tabanına yansıtacaktır.
- Uygulama çalıştırıldıktan sonra Gateway servisine https://localhost:7210/ adresi üzerinden ulaşılabilir. 
- API dökümantasyonu için Swagger kullanılmıştır.