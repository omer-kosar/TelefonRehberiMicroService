using Service.Rapor.Dto;
using System.Text;

namespace Service.Rapor.HttpClientServices.Interfaces
{
    public interface IILetisimService
    {
        Task<RaporKonumDto> GetirRaporByKonum(string konum);
    }
}
