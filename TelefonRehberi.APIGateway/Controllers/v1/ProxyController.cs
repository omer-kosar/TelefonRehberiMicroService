using Entities.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TelefonRehberi.APIGateway.Models.ErrorModels;
using TelefonRehberi.APIGateway.Models.Responses;

namespace TelefonRehberi.APIGateway.Controllers.v1
{
    [Route("api/v{version:apiVersion}/telefon-rehberi-gateway")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ProxyController : ControllerBase
    {


        private IActionResult ProcessError(ApiBaseResponse baseResponse)
        {
            return baseResponse switch
            {
                ApiNotFoundResponse => NotFound(new ErrorDetail
                {
                    Message = ((ApiNotFoundResponse)baseResponse).Message,
                    StatusCode = StatusCodes.Status404NotFound
                })
            };
        }
    }
}
