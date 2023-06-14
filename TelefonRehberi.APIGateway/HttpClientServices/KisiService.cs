using Entities.Responses;
using Newtonsoft.Json;
using System.Text;
using TelefonRehberi.APIGateway.Extensions;
using TelefonRehberi.APIGateway.HttpClientServices.Interfaces;
using TelefonRehberi.APIGateway.Models.Kisi;
using TelefonRehberi.APIGateway.Models.Responses;

namespace TelefonRehberi.APIGateway.HttpClientServices
{
    public class KisiService : IKisiService
    {
        private readonly HttpClient _client;
        public KisiService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<ApiBaseResponse> GetirKisiById(Guid id)
        {
            var response = await _client.GetAsync($"api/v1/persons/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                var errorMessage = await response.ReadContentAs();
                return new ApiNotFoundResponse(errorMessage);
            }

            response.EnsureSuccessStatusCode();
            var result = await response.ReadContentAs<Kisi>();
            return new ApiOkResponse<Kisi>(result);
        }
        public async Task<ApiBaseResponse> GetirKisiListesi()
        {
            var response = await _client.GetAsync("api/v1/persons");

            response.EnsureSuccessStatusCode();
            var result = await response.ReadContentAs<IEnumerable<Kisi>>();
            return new ApiOkResponse<IEnumerable<Kisi>>(result);
        }
        public async Task<ApiBaseResponse> KisiKaydet(Kisi kisi)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(kisi), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/v1/persons", stringContent);

            if (response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
            {
                var errorMessage = await response.ReadContentAs();
                return new ApiUnprocessableResponse(errorMessage);
            }

            response.EnsureSuccessStatusCode();
            var result = await response.ReadContentAs<string>();
            return new ApiOkResponse<string>(result);
        }
        public async Task<ApiBaseResponse> KisiSil(Guid id)
        {
            var response = await _client.DeleteAsync($"api/v1/persons/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                var errorMessage = await response.ReadContentAs();
                return new ApiNotFoundResponse(errorMessage);
            }

            response.EnsureSuccessStatusCode();
            var result = await response.ReadContentAs<string>();
            return new ApiOkResponse<string>(result);
        }
    }
}
