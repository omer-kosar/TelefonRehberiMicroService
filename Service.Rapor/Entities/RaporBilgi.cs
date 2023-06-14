namespace Service.Rapor.Entities
{
    public class RaporBilgi
    {
        public Guid Id { get; set; }
        public Guid RaporId { get; set; }
        public string KonumBilgisi { get; set; }
        public int KisiSayisi { get; set; }
        public int TelefonNumarasiSayisi { get; set; }
        public Rapor Rapor { get; set; }
    }
}
