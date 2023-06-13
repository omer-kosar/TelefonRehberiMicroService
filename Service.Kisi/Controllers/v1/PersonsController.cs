using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Kisi.Dto;
using Service.Kisi.Repositories.Interfaces;
using System.Net;

namespace Service.Kisi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/persons")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IKisiRepository _kisiRepository;

        public PersonsController(IKisiRepository kisiRepository)
        {
            _kisiRepository = kisiRepository ?? throw new ArgumentNullException(nameof(kisiRepository));
        }
        [HttpGet("{id:guid}", Name = "getirKisiById")]
        [ProducesResponseType(typeof(Entities.Kisi), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getirKisiById(Guid id)
        {
            var kisi = await _kisiRepository.GetirKisiById(id);
            var kisiDto = kisi.Adapt<KisiDto>();
            if (kisi is null)
                return NotFound();
            return Ok(kisiDto);
        }
    }
}
