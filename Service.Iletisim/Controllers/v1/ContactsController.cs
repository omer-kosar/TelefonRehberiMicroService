﻿using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Iletisim.Dto;
using Service.Iletisim.Entities;
using Service.Iletisim.Repositories.Interfaces;
using System.Net;

namespace Service.Iletisim.Controllers.v1
{
    [Route("api/v{version:apiVersion}/contacts")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        //iletişim sil
        //konum raporu getir
        //kişi id ile iletişim bilgileri getir
        private readonly IIletisimRepository _iletisimRepository;
        public ContactsController(IIletisimRepository iletisimRepository)
        {
            _iletisimRepository = iletisimRepository ?? throw new ArgumentNullException(nameof(iletisimRepository)); ;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> KaydetIletisim([FromBody] IletisimDto iletisim)
        {
            //todo:validation eklenecek
            var iletisimEntity = iletisim.Adapt<Entities.Iletisim>();
            await _iletisimRepository.IletisimKaydet(iletisimEntity);
            return Ok(iletisimEntity.Id);
        }
        [HttpDelete("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> IletisimSilById(Guid id)
        {
            var deletedIletisim = await _iletisimRepository.GetirIletisimById(id);
            if (deletedIletisim is null)
                return NotFound($"Contact was not able to found with id:{id}");
            await _iletisimRepository.IletisimSil(id);
            return NoContent();
        }
    }
}
