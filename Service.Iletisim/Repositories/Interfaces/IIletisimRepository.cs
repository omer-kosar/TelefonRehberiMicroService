using Service.Iletisim.Dto;

namespace Service.Iletisim.Repositories.Interfaces
{
    public interface IIletisimRepository
    {
        Task IletisimKaydet(Entities.Iletisim iletisim);
        Task<Entities.Iletisim> GetirIletisimById(Guid id);
        Task<IEnumerable<Entities.Iletisim>> GetirIletisimListesi();
        Task IletisimSil(Guid id);
        Task<IEnumerable<Entities.Iletisim>> GetirIletisimBilgileriByKisiId(Guid kisiId);
        Task<KonumRaporuDto> GetirKonumRaporByKonum(string konum);
    }
}
