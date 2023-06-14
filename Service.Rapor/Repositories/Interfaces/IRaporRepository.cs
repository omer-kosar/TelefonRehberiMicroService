namespace Service.Rapor.Repositories.Interfaces
{
    public interface IRaporRepository
    {

        //rapor kaydet
        //rapor listesi getir
        //rapor güncelle
        Task<Entities.Rapor> GetirRaporById(Guid id);
        Task RaporKaydet(Entities.Rapor rapor);
    }
}
