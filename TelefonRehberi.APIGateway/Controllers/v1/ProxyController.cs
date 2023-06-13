using Entities.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TelefonRehberi.APIGateway.HttpClientServices.Interfaces;
using TelefonRehberi.APIGateway.Models.ErrorModels;
using TelefonRehberi.APIGateway.Models.Exceptions;
using TelefonRehberi.APIGateway.Models.Kisi;
using TelefonRehberi.APIGateway.Models.Responses;

namespace TelefonRehberi.APIGateway.Controllers.v1
{
    [Route("api/v{version:apiVersion}/telefon-rehberi-gateway")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ProxyController : ControllerBase
    {

        private readonly IKisiService _kisiService;

        public ProxyController(IKisiService kisiService)
        {
            _kisiService = kisiService;
        }

        [HttpGet("/kisi")]
        public async Task<IActionResult> GetirKisiListesi()
        {
            var result = await _kisiService.GetirKisiListesi();
            if (!result.Success)
            {
                return ProcessError(result);
            }
            var kisiListesi = result.GetResult<IEnumerable<Kisi>>();
            return Ok(kisiListesi);
        }
        [HttpPost("/kisi")]
        public async Task<IActionResult> CreateKisi(Kisi kisi)
        {

            var baseResult = await _kisiService.KisiKaydet(kisi);
            if (!baseResult.Success)
            {
                return ProcessError(baseResult);
            }
            return NoContent();
        }
        private IActionResult ProcessError(ApiBaseResponse baseResponse)
        {
            return baseResponse switch
            {
                ApiNotFoundResponse => NotFound(new ErrorDetail
                {
                    Message = ((ApiNotFoundResponse)baseResponse).Message,
                    StatusCode = StatusCodes.Status404NotFound
                }),
                ApiUnprocessableResponse => UnprocessableEntity(new ErrorDetail
                {
                    Message = ((ApiUnprocessableResponse)baseResponse).Message,
                    StatusCode = StatusCodes.Status400BadRequest
                })
            };
        }
    }
}
