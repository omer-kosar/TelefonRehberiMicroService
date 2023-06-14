using TelefonRehberi.APIGateway.Models.Rapor;
using TelefonRehberi.APIGateway.Models.Responses;

namespace TelefonRehberi.APIGateway.HttpClientServices.Interfaces
{
    public interface IRaporService
    {
        Task<ApiBaseResponse> RaporKaydet(Rapor rapor);
        Task<ApiBaseResponse> GetirRaporListesi();
    }
}
