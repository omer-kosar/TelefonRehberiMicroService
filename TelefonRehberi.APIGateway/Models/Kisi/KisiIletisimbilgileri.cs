using TelefonRehberi.APIGateway.Models.Iletisim;

namespace TelefonRehberi.APIGateway.Models.Kisi
{
    public class KisiIletisimbilgileri
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }
        public List<IletisimBilgileri> IletisimBilgileri { get; set; }
    }
}
