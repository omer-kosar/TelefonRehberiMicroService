using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;
using Service.Rapor.Dto;
using Service.Rapor.Entities;
using Service.Rapor.Repositories.Interfaces;
using Service.Rapor.HttpClientServices.Interfaces;
using Mapster;
using Service.Rapor.Enums;

namespace Service.Rapor.BackgroundServices
{
    public class RaporServiceConsumer : IHostedService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceScopeFactory _scopeFactory;

        public RaporServiceConsumer(IServiceScopeFactory scopeFactory)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", DispatchConsumersAsync = true };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _scopeFactory = scopeFactory;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _channel.QueueDeclare(queue: "rapor-talepleri",
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var raporTalepModel = JsonConvert.DeserializeObject<RaporTalepDto>(message);

                using (var scope = _scopeFactory.CreateScope())
                {
                    Thread.Sleep(10000);
                    var raporBilgiRepository = scope.ServiceProvider.GetRequiredService<IRaporBilgiRepository>();
                    var raporRepository = scope.ServiceProvider.GetRequiredService<IRaporRepository>();
                    var iletisimService = scope.ServiceProvider.GetRequiredService<IILetisimService>();
                    var konumRaporu = await iletisimService.GetirRaporByKonum(raporTalepModel?.Konum);
                    var raporBilgiEntity = konumRaporu.Adapt<RaporBilgi>();
                    raporBilgiEntity.RaporId = raporTalepModel.RaporId;
                    await raporBilgiRepository.RaporBilgiKaydet(raporBilgiEntity);
                    var hazirlananRapor = await raporRepository.GetirRaporById(raporTalepModel.RaporId);
                    hazirlananRapor.Durum = (int)RaporDurum.Tamamlandi;
                    await raporRepository.RaporGuncelle(hazirlananRapor);
                }
                _channel.BasicAck(ea.DeliveryTag, false);

            };
            _channel.BasicConsume(queue: "rapor-talepleri",
                               autoAck: false,
                               consumer: consumer);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.Close();
            _connection.Close();

            return Task.CompletedTask;
        }
    }
}
