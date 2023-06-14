using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Rapor.Dto;
using Service.Rapor.Filters;
using Service.Rapor.Repositories;
using Service.Rapor.Repositories.Interfaces;
using System.Net;

namespace Service.Rapor.Controllers.v1
{
    [Route("api/v{version:apiVersion}/reports")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IRaporRepository _raporRepository;
        private readonly IRaporBilgiRepository _raporBilgiRepository;

        public ReportsController(IRaporRepository raporRepository, IRaporBilgiRepository raporBilgiRepository)
        {
            _raporRepository = raporRepository;
            _raporBilgiRepository = raporBilgiRepository;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilter))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> RaporKaydet([FromBody] RaporDto rapor)
        {
            var raporEntity = rapor.Adapt<Entities.Rapor>();
            await _raporRepository.RaporKaydet(raporEntity);
            return Ok(raporEntity.Id);
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RaporListDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetirRaporListesi()
        {
            var raporlar = await _raporRepository.GetirRaporListesi();
            var raporListDto = raporlar.Adapt<IEnumerable<RaporListDto>>();
            return Ok(raporListDto);
        }
        [HttpGet("{raporId:guid}")]
        [ProducesResponseType(typeof(IEnumerable<RaporBilgiListDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetirRaporDetayBilgileri(Guid raporId)
        {
            var raporBilgileri = await _raporBilgiRepository.GetirRaporDetayBilgiList(raporId);
            var raporBilgiListDto = raporBilgileri.Adapt<IEnumerable<RaporBilgiListDto>>();
            return Ok(raporBilgiListDto);
        }
    }
}
