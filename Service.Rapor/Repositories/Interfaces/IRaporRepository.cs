namespace Service.Rapor.Repositories.Interfaces
{
    public interface IRaporRepository
    {

        Task<Entities.Rapor> GetirRaporById(Guid id);
        Task RaporKaydet(Entities.Rapor rapor);
        Task<IEnumerable<Entities.Rapor>> GetirRaporListesi();
        Task RaporGuncelle(Entities.Rapor rapor);
    }
}
