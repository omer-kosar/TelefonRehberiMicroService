namespace Service.Rapor.Dto
{
    public class RaporListDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset TalepEdildigiTarih { get; set; }
        public int Durum { get; set; }
    }
}
