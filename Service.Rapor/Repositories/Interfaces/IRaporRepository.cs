namespace Service.Rapor.Repositories.Interfaces
{
    public interface IRaporRepository
    {

        //rapor listesi getir
        //rapor güncelle
        Task<Entities.Rapor> GetirRaporById(Guid id);
        Task RaporKaydet(Entities.Rapor rapor);
        Task<IEnumerable<Entities.Rapor>> GetirRaporListesi();
    }
}
