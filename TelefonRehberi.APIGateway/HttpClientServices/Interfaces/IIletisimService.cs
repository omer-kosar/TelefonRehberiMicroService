using TelefonRehberi.APIGateway.Models.Iletisim;
using TelefonRehberi.APIGateway.Models.Responses;

namespace TelefonRehberi.APIGateway.HttpClientServices.Interfaces
{
    public interface IIletisimService
    {
        //kişi ile iletişim getir
        Task<ApiBaseResponse> IletisimKaydet(IletisimBilgileri iletisim);
        Task<ApiBaseResponse> IletisimSil(Guid id);
        Task<ApiBaseResponse> GetIletisimBilgileriByKisiId(Guid kisiId);
    }
}
