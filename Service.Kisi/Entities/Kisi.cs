namespace Service.Kisi.Entities
{
    public class Kisi
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }
    }
}
