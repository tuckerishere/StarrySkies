using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StarrySkies.Services.DTOs.VocationSpellDtos;
using StarrySkies.Services.Services.VocationSpells;

namespace StarrySkies.API.Controllers
{
    [ApiController]
    [Route("/api/vocationSpells")]
    public class VocationSpellController : ControllerBase
    {
        private readonly IVocationSpellService _vocationSpellService;
        public VocationSpellController(IVocationSpellService vocationSpellService)
        {
            _vocationSpellService = vocationSpellService;
        }

        [HttpGet("{vocationId}/vocation/{spellId}/spell")]
        [ProducesResponseType(200, Type=typeof(VocationSpellResponseDto))]
        [ProducesResponseType(400)]
        public ActionResult<VocationSpellResponseDto> GetVocationSpell(int vocationId, int spellId)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VocationSpellResponseDto vsToReturn = _vocationSpellService.GetVocationSpell(vocationId, spellId);

            if(vsToReturn == null || vsToReturn?.VocationId == 0 || vsToReturn?.SpellId == 0)
            {
                return NotFound();
            }

            return Ok(vsToReturn);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<VocationSpellResponseDto>))]
        public ActionResult<List<VocationSpellResponseDto>> GetVocationSpells()
        {
            var vocationSpells = _vocationSpellService.GetVocationSpells();
            return Ok(vocationSpells);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(VocationSpellResponseDto))]
        [ProducesResponseType(400)]
        public ActionResult<VocationSpellResponseDto> CreateVocationSpell(VocationSpellResponseDto createVocationSpell)
        {
            VocationSpellResponseDto vsToReturn = new VocationSpellResponseDto();
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            vsToReturn = _vocationSpellService.CreateVocationSpell(createVocationSpell);

            if(vsToReturn?.VocationId == 0 || vsToReturn?.SpellId == 0 || vsToReturn == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetVocationSpell), new { vocationId = vsToReturn.VocationId, spellId = vsToReturn.SpellId }
                , createVocationSpell);
        }
    }
}