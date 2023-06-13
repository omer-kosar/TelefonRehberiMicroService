using Newtonsoft.Json;
using System.Text.Json;

namespace TelefonRehberi.APIGateway.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
           return JsonConvert.DeserializeObject<T>(dataAsString);
        }
        public static async Task<string> ReadContentAs(this HttpResponseMessage response)
        {
            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return dataAsString;
        }
    }
}
