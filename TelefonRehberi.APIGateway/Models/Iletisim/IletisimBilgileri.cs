namespace TelefonRehberi.APIGateway.Models.Iletisim
{
    public class IletisimBilgileri
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid KisiId { get; set; }
        public int IletisimType { get; set; }
        public string Icerik { get; set; }
    }
}
