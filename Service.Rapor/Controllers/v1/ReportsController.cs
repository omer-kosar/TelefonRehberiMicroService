using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Service.Rapor.Controllers.v1
{
    [Route("api/v{version:apiVersion}/reports")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
    }
}
