namespace TelefonRehberi.APIGateway.Models.Rapor
{
    public class Rapor
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTimeOffset TalepEdildigiTarih { get; set; }
        public int Durum { get; set; }
    }
}
