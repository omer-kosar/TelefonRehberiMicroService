using TelefonRehberi.APIGateway.Models.Kisi;
using TelefonRehberi.APIGateway.Models.Responses;

namespace TelefonRehberi.APIGateway.HttpClientServices.Interfaces
{
    public interface IKisiService
    {
        //Task<ApiBaseResponse> GetirKisiById(Guid id);
        Task<ApiBaseResponse> GetirKisiListesi();
        Task<ApiBaseResponse> KisiKaydet(Kisi kisi);
        Task<ApiBaseResponse> KisiSil(Guid id);
    }
}
