namespace Service.Iletisim.Dto
{
    public class IletisimDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid KisiId { get; set; }
        public int IletisimType { get; set; }
        public string Icerik { get; set; }
    }
}
