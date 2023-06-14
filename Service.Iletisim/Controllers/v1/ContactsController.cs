using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Iletisim.Repositories.Interfaces;

namespace Service.Iletisim.Controllers.v1
{
    [Route("api/v{version:apiVersion}/contacts")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        //iletişim kaydet
        //iletişim sil
        //konum raporu getir
        //kişi id ile iletişim bilgileri getir
        private readonly IIletisimRepository _iletisimRepository;
        public ContactsController(IIletisimRepository iletisimRepository)
        {
            _iletisimRepository = iletisimRepository ?? throw new ArgumentNullException(nameof(iletisimRepository)); ;
        }
        
    }
}
