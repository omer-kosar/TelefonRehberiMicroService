using TelefonRehberi.APIGateway.Models.Rapor;
using TelefonRehberi.APIGateway.Models.Responses;

namespace TelefonRehberi.APIGateway.HttpClientServices.Interfaces
{
    public interface IRaporService
    {
        Task<ApiBaseResponse> CreateReport(Rapor rapor);
    }
}
