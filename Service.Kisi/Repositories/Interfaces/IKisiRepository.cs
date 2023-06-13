namespace Service.Kisi.Repositories.Interfaces
{
    public interface IKisiRepository
    {
        Task KisiKaydet(Entities.Kisi kisi);
        Task<Entities.Kisi> GetirKisiById(Guid id);
        Task<IEnumerable<Entities.Kisi>> GetirKisiListesi();
        Task KisiSil(Guid id);
    }
}
