using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Service.Kisi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/persons")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
    }
}
