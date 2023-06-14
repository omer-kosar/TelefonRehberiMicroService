using Newtonsoft.Json;
using Service.Rapor.Dto;
using Service.Rapor.HttpClientServices.Interfaces;

namespace Service.Rapor.HttpClientServices
{
    public class IletisimService : IILetisimService
    {
        private readonly HttpClient _client;

        public IletisimService(HttpClient client)
        {
            _client = client;
        }

        public async Task<RaporKonumDto> GetirRaporByKonum(string konum)
        {
            var response = await _client.GetAsync($"api/v1/contacts?konum={konum}");
            response.EnsureSuccessStatusCode();
            var dataAsString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RaporKonumDto>(dataAsString);
        }
    }
}
