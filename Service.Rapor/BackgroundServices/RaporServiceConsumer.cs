using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading.Channels;

namespace Service.Rapor.BackgroundServices
{
    public class RaporServiceConsumer : IHostedService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        public RaporServiceConsumer()
        {
            var factory = new ConnectionFactory() { HostName = "localhost", DispatchConsumersAsync = true };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
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
