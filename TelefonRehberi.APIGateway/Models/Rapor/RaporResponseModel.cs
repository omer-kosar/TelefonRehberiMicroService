namespace TelefonRehberi.APIGateway.Models.Rapor
{
    public class RaporResponseModel
    {
        public Guid RaporId { get; set; }
        public DateTimeOffset TalepEdildigiTarihi { get; set; }
        public string Durum { get; set; }
    }
}
