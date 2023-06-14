using Entities.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TelefonRehberi.APIGateway.HttpClientServices.Interfaces;
using TelefonRehberi.APIGateway.Models.ErrorModels;
using TelefonRehberi.APIGateway.Models.Exceptions;
using TelefonRehberi.APIGateway.Models.Iletisim;
using TelefonRehberi.APIGateway.Models.Kisi;
using TelefonRehberi.APIGateway.Models.Responses;

namespace TelefonRehberi.APIGateway.Controllers.v1
{
    [Route("api/v{version:apiVersion}/telefon-rehberi-gateway")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ProxyController : ControllerBase
    {
        //kisiid ile iletişim bilgileri getir
        private readonly IKisiService _kisiService;
        private readonly IIletisimService _iletisimService;

        public ProxyController(IKisiService kisiService, IIletisimService iletisimService)
        {
            _kisiService = kisiService;
            _iletisimService = iletisimService;
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

        [HttpDelete("/kisi/{id}")]
        public async Task<IActionResult> DeleteKisi(Guid id)
        {
            var baseResult = await _kisiService.KisiSil(id);
            if (!baseResult.Success)
            {
                return ProcessError(baseResult);
            }
            return NoContent();
        }


        [HttpPost("/iletisim")]
        public async Task<IActionResult> Iletisim(IletisimBilgileri iletisim)
        {

            var baseResult = await _iletisimService.IletisimKaydet(iletisim);
            if (!baseResult.Success)
            {
                return ProcessError(baseResult);
            }
            return Ok(baseResult.GetResult<Guid>());
        }
        [HttpDelete("/iletisim/{id}")]
        public async Task<IActionResult> IletisimSil(Guid id)
        {
            var baseResult = await _iletisimService.IletisimSil(id);
            if (!baseResult.Success)
            {
                return ProcessError(baseResult);
            }
            return NoContent();
        }
        [HttpGet("/iletisim/{id}")]
        public async Task<IActionResult> GetKisiIletisimBilgileri(Guid id)
        {
            var kisiResult = await _kisiService.GetirKisiById(id);
            if (!kisiResult.Success)
            {
                return ProcessError(kisiResult);
            }
            var kisi = kisiResult.GetResult<Kisi>();
            var iletisimResult = await _iletisimService.GetIletisimBilgileriByKisiId(id);
            if (!iletisimResult.Success)
            {
                return ProcessError(iletisimResult);
            }
            var iletisimBilgileri = iletisimResult.GetResult<List<IletisimBilgileri>>();
            var kisiIletisimBilgileri = new KisiIletisimbilgileri { Ad = kisi.Ad, Soyad = kisi.Soyad, Firma = kisi.Firma, IletisimBilgileri = iletisimBilgileri };
            return Ok(kisiIletisimBilgileri);
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
