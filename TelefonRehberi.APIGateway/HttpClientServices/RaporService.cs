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

        public async Task<ApiBaseResponse> GetirRaporListesi()
        {
            var response = await _client.GetAsync("api/v1/reports");

            response.EnsureSuccessStatusCode();
            var result = await response.ReadContentAs<IEnumerable<Rapor>>();
            return new ApiOkResponse<IEnumerable<Rapor>>(result);
        }

        public async Task<ApiBaseResponse> RaporKaydet(Rapor rapor)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(rapor), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/v1/reports", stringContent);

            if (response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
            {
                var errorMessage = await response.ReadContentAs();
                return new ApiUnprocessableResponse(errorMessage);
            }

            response.EnsureSuccessStatusCode();
            var result = await response.ReadContentAs<Guid>();
            return new ApiOkResponse<Guid>(result);
        }
        public async Task<ApiBaseResponse> GetirRaporDetayBilgileri(Guid raporId)
        {
            var response = await _client.GetAsync($"api/v1/reports/{raporId}");

            response.EnsureSuccessStatusCode();
            var result = await response.ReadContentAs<IEnumerable<RaporDetayBilgileri>>();
            return new ApiOkResponse<IEnumerable<RaporDetayBilgileri>>(result);
        }
    }
}
