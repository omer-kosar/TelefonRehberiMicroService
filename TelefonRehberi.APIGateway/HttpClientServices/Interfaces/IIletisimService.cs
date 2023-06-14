using TelefonRehberi.APIGateway.Models.Iletisim;
using TelefonRehberi.APIGateway.Models.Responses;

namespace TelefonRehberi.APIGateway.HttpClientServices.Interfaces
{
    public interface IIletisimService
    {
        //iletişim sil
        //kişi ile iletişim getir
        Task<ApiBaseResponse> IletisimKaydet(IletisimBilgileri iletisim);
    }
}
