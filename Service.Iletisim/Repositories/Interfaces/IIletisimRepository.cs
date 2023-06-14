namespace Service.Iletisim.Repositories.Interfaces
{
    public interface IIletisimRepository
    {
        Task IletisimKaydet(Entities.Iletisim iletisim);
        Task<Entities.Iletisim> GetIletisimById(Guid id);
    }
}
