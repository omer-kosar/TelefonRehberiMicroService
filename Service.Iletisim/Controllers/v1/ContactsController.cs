using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Service.Iletisim.Controllers.v1
{
    [Route("api/v{version:apiVersion}/contacts")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
    }
}
