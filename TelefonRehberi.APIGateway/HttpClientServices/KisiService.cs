using Entities.Responses;
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

            if (response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
            {
                var errorMessage = await response.ReadContentAs();
                return new PersonNotFoundResponse(errorMessage);
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
    }
}
