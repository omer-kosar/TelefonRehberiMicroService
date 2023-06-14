using Entities.Responses;
using Newtonsoft.Json;
using System.Text;
using TelefonRehberi.APIGateway.Extensions;
using TelefonRehberi.APIGateway.HttpClientServices.Interfaces;
using TelefonRehberi.APIGateway.Models.Rapor;
using TelefonRehberi.APIGateway.Models.Responses;

namespace TelefonRehberi.APIGateway.HttpClientServices
{

    public class RaporService : IRaporService
    {
        private readonly HttpClient _client;

        public RaporService(HttpClient client)
        {
            _client = client;
        }
        public async Task<ApiBaseResponse> CreateReport(Rapor rapor)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(rapor), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/v1/reports", stringContent);

            if (response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
            {
                var errorMessage = await response.ReadContentAs();
                return new ReportUnprocessableResponse(errorMessage);
            }

            response.EnsureSuccessStatusCode();
            var result = await response.ReadContentAs<Guid>();
            return new ApiOkResponse<Guid>(result);
        }
    }
}
