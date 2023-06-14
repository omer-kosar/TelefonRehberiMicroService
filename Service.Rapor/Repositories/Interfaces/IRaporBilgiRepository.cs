namespace Service.Rapor.Repositories.Interfaces
{
    public interface IRaporBilgiRepository
    {
        Task<Entities.RaporBilgi> GetirRaporBilgiById(Guid id);

    }
}
