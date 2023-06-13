using TelefonRehberi.APIGateway.Models.Responses;

namespace TelefonRehberi.APIGateway.HttpClientServices.Interfaces
{
    public interface IKisiService
    {
        Task<ApiBaseResponse> GetirKisiById(Guid id);
        Task<ApiBaseResponse> GetirKisiListesi();
    }
}
