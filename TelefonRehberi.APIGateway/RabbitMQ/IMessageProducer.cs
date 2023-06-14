namespace TelefonRehberi.APIGateway.RabbitMQ
{
    public interface IMessageProducer
    {
        void SendRaporOlusturMesaj<T>(T message);
    }
}
