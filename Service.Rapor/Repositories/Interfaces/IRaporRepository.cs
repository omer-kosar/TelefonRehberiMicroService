namespace Service.Rapor.Repositories.Interfaces
{
    public interface IRaporRepository
    {
        Task<Entities.Rapor> GetirRaporById(Guid id);
    }
}
