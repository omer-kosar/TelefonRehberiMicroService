using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Rapor.Dto;
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

        public ReportsController(IRaporRepository raporRepository)
        {
            _raporRepository = raporRepository;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> RaporKaydet([FromBody] RaporDto rapor)
        {
            var raporEntity = rapor.Adapt<Entities.Rapor>();
            await _raporRepository.RaporKaydet(raporEntity);
            return Ok(raporEntity.Id);
        }
    }
}
