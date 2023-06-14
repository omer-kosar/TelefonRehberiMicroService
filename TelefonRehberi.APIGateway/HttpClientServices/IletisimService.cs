using Entities.Responses;
using Newtonsoft.Json;
using System.Text;
using TelefonRehberi.APIGateway.Extensions;
using TelefonRehberi.APIGateway.HttpClientServices.Interfaces;
using TelefonRehberi.APIGateway.Models.Iletisim;
using TelefonRehberi.APIGateway.Models.Responses;

namespace TelefonRehberi.APIGateway.HttpClientServices
{
    public class IletisimService : IIletisimService
    {
        private readonly HttpClient _client;
        public IletisimService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<ApiBaseResponse> GetIletisimBilgileriByKisiId(Guid kisiId)
        {

            var response = await _client.GetAsync($"api/v1/contacts/{kisiId}");

            response.EnsureSuccessStatusCode();
            var result = await response.ReadContentAs<List<IletisimBilgileri>>();
            return new ApiOkResponse<List<IletisimBilgileri>>(result);
        }

        public async Task<ApiBaseResponse> IletisimKaydet(IletisimBilgileri iletisim)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(iletisim), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/v1/contacts", stringContent);

            if (response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
            {
                var errorMessage = await response.ReadContentAs();
                return new PersonUnprocessableResponse(errorMessage);
            }

            response.EnsureSuccessStatusCode();
            var result = await response.ReadContentAs<Guid>();
            return new ApiOkResponse<Guid>(result);
        }

        public async Task<ApiBaseResponse> IletisimSil(Guid id)
        {
            var response = await _client.DeleteAsync($"api/v1/contacts/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                var errorMessage = await response.ReadContentAs();
                return new PersonNotFoundResponse(errorMessage);
            }

            response.EnsureSuccessStatusCode();
            var result = await response.ReadContentAs<string>();
            return new ApiOkResponse<string>(result);
        }
    }
}
