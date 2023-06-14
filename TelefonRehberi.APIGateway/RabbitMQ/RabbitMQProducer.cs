using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace TelefonRehberi.APIGateway.RabbitMQ
{
    public class RabbitMQProducer : IMessageProducer
    {
        public void SendRaporOlusturMesaj<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("rapor-talepleri", durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "rapor-talepleri", body: body);
        }
    }
}
