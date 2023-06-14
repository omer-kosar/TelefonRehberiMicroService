using Entities.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TelefonRehberi.APIGateway.Enums;
using TelefonRehberi.APIGateway.HttpClientServices;
using TelefonRehberi.APIGateway.HttpClientServices.Interfaces;
using TelefonRehberi.APIGateway.Models.ErrorModels;
using TelefonRehberi.APIGateway.Models.Exceptions;
using TelefonRehberi.APIGateway.Models.Iletisim;
using TelefonRehberi.APIGateway.Models.Kisi;
using TelefonRehberi.APIGateway.Models.Rapor;
using TelefonRehberi.APIGateway.Models.Responses;
using TelefonRehberi.APIGateway.RabbitMQ;

namespace TelefonRehberi.APIGateway.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    public class ProxyController : ControllerBase
    {
        private readonly IKisiService _kisiService;
        private readonly IIletisimService _iletisimService;
        private readonly IRaporService _raporService;
        private readonly IMessageProducer _messageProducer;

        public ProxyController(IKisiService kisiService, IIletisimService iletisimService, IRaporService raporService, IMessageProducer messageProducer)
        {
            _kisiService = kisiService;
            _iletisimService = iletisimService;
            _raporService = raporService;
            _messageProducer = messageProducer;
        }

        [HttpGet]
        [Route("api/v{version:apiVersion}/telefon-rehberi-gateway/kisi")]
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
        [HttpPost]
        [Route("api/v{version:apiVersion}/telefon-rehberi-gateway/kisi")]
        public async Task<IActionResult> CreateKisi(Kisi kisi)
        {

            var baseResult = await _kisiService.KisiKaydet(kisi);
            if (!baseResult.Success)
            {
                return ProcessError(baseResult);
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("api/v{version:apiVersion}/telefon-rehberi-gateway/kisi/{id}")]
        public async Task<IActionResult> DeleteKisi(Guid id)
        {
            var baseResult = await _kisiService.KisiSil(id);
            if (!baseResult.Success)
            {
                return ProcessError(baseResult);
            }
            return NoContent();
        }


        [HttpPost]
        [Route("api/v{version:apiVersion}/telefon-rehberi-gateway/iletisim")]

        public async Task<IActionResult> Iletisim(IletisimBilgileri iletisim)
        {

            var baseResult = await _iletisimService.IletisimKaydet(iletisim);
            if (!baseResult.Success)
            {
                return ProcessError(baseResult);
            }
            return Ok(baseResult.GetResult<Guid>());
        }

        [HttpDelete]
        [Route("api/v{version:apiVersion}/telefon-rehberi-gateway/iletisim/{id}")]

        public async Task<IActionResult> IletisimSil(Guid id)
        {
            var baseResult = await _iletisimService.IletisimSil(id);
            if (!baseResult.Success)
            {
                return ProcessError(baseResult);
            }
            return NoContent();
        }

        [HttpGet]
        [Route("api/v{version:apiVersion}/telefon-rehberi-gateway/iletisim/{id}")]
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

        [HttpPost]
        [Route("api/v{version:apiVersion}/telefon-rehberi-gateway/rapor")]
        public async Task<IActionResult> RaporKaydet(string konum)
        {
            var yeniRapor = new Models.Rapor.Rapor { Durum = (int)RaporDurum.Hazirlaniyor, TalepEdildigiTarih = DateTimeOffset.UtcNow };
            var baseResult = await _raporService.RaporKaydet(yeniRapor);
            if (!baseResult.Success)
            {
                return ProcessError(baseResult);
            }
            var raporId = baseResult.GetResult<Guid>();
            _messageProducer.SendRaporOlusturMesaj(new RaporTalepModel { RaporId = raporId, Konum = konum });
            return Accepted(new RaporResponseModel { RaporId = raporId, Durum = "Hazırlanıyor", TalepEdildigiTarihi = yeniRapor.TalepEdildigiTarih });
        }

        [HttpGet]
        [Route("api/v{version:apiVersion}/telefon-rehberi-gateway/rapor")]

        public async Task<IActionResult> GetirRaporListesi()
        {
            var baseResult = await _raporService.GetirRaporListesi();
            if (!baseResult.Success)
            {
                return ProcessError(baseResult);
            }
            var result = baseResult.GetResult<IEnumerable<Rapor>>();
            return Ok(result);
        }
        [HttpGet]
        [Route("api/v{version:apiVersion}/telefon-rehberi-gateway/rapor/{raporId:guid}")]
        public async Task<IActionResult> GetRaporRaporDetayBilgileri(Guid raporId)
        {
            var baseResult = await _raporService.GetirRaporDetayBilgileri(raporId);
            if (!baseResult.Success)
            {
                return ProcessError(baseResult);
            }
            var result = baseResult.GetResult<IEnumerable<RaporDetayBilgileri>>();
            return Ok(result);
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
