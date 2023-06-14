namespace Service.Rapor.Entities
{
    public class Rapor
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTimeOffset TalepEdildigiTarih { get; set; }
        public int Durum { get; set; }
        public ICollection<RaporBilgi> RaporBilgileri { get; set; }
    }
}
