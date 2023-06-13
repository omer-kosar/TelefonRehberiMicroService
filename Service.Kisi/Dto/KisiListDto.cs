namespace Service.Kisi.Dto
{
    public class KisiListDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }
    }
}
