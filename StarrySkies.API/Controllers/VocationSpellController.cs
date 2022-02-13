using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StarrySkies.Services.DTOs.VocationSpellDtos;
using StarrySkies.Services.ResponseModels;
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

        [HttpGet("{vocationId}/vocation/{spellId}/spell", Name = "GetVocationSpell")]
        [ProducesResponseType(200, Type = typeof(VocationSpellResponseDto))]
        [ProducesResponseType(400)]
        public ActionResult<ServiceResponse<VocationSpellResponseDto>> GetVocationSpell(int vocationId, int spellId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vsToReturn = _vocationSpellService.GetVocationSpell(vocationId, spellId);

            if (!vsToReturn.Success)
            {
                return NotFound(vsToReturn);
            }

            return Ok(vsToReturn);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ServiceResponse<List<VocationSpellResponseDto>>))]
        public ActionResult<ServiceResponse<VocationSpellResponseDto>> GetVocationSpells()
        {
            var vocationSpells = _vocationSpellService.GetVocationSpells();
            return Ok(vocationSpells);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ServiceResponse<VocationSpellResponseDto>))]
        [ProducesResponseType(400)]
        public ActionResult<ServiceResponse<VocationSpellResponseDto>> CreateVocationSpell(VocationSpellResponseDto createVocationSpell)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var vsToReturn = _vocationSpellService.CreateVocationSpell(createVocationSpell);

            if (!vsToReturn.Success)
            {
                return BadRequest(vsToReturn);
            }

            return CreatedAtAction(nameof(GetVocationSpell), new { vocationId = vsToReturn.Data.VocationId, spellId = vsToReturn.Data.SpellId }
                , vsToReturn);
        }

        [HttpDelete("{vocationId}/vocation/{spellId}/spell")]
        [ProducesResponseType(200, Type = typeof(ServiceResponse<VocationSpellResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<ServiceResponse<VocationSpellResponseDto>> DeleteVocationSpell(int vocationId, int spellId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var vsToReturn = _vocationSpellService.DeleteVocationSpell(vocationId, spellId);

            if (!vsToReturn.Success)
            {
                return NotFound(vsToReturn);
            }

            return Ok(vsToReturn);
        }

        [HttpPut("{vocationId}/vocation/{spellId}/spell")]
        [ProducesResponseType(200, Type = typeof(ServiceResponse<VocationSpellResponseDto>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public ActionResult<ServiceResponse<VocationSpellResponseDto>> UpdateVocationSpell(int vocationId, int spellId,
            VocationSpellResponseDto updatedVocationSpell)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vsToUpdate = _vocationSpellService.UpdateVocationSpell(vocationId, spellId, updatedVocationSpell);

            if (!vsToUpdate.Success)
            {
                return NotFound(vsToUpdate);
            }

            return Ok(vsToUpdate);
        }
    }
}