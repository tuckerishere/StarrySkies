using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StarrySkies.Services.DTOs.SpellDtos;
using StarrySkies.Services.Services.Spells;

namespace StarrySkies.API.Controllers
{
    [ApiController]
    [Route("/api/spells")]
    public class SpellController : ControllerBase
    {
        private readonly ISpellService _spellService;
        public SpellController(ISpellService spellService)
        {
            _spellService = spellService;
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type=typeof(List<SpellResponseDto>))]
        public ActionResult<List<SpellResponseDto>> GetAllSpells()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vocationsToReturn = _spellService.GetAllSpells();
            return Ok(vocationsToReturn);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type=typeof(SpellResponseDto))]
        public ActionResult<SpellResponseDto> GetSpell(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var spellToReturn = _spellService.GetSpell(id);
            if(spellToReturn == null || spellToReturn?.Id == 0)
            {
                return NotFound();
            }

            return Ok(spellToReturn);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(SpellResponseDto))]
        [ProducesResponseType(400)]
        public ActionResult<SpellResponseDto> CreateSpell(CreateSpellDto createSpell)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (createSpell == null || string.IsNullOrWhiteSpace(createSpell.Name))
            {
                return BadRequest("Please enter Spell Name");
            }

            var createdSpell = _spellService.CreateSpell(createSpell);

            return CreatedAtAction(nameof(GetSpell), new { id = createdSpell.Id }, createdSpell);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type=typeof(SpellResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<SpellResponseDto> DeleteSpell(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SpellResponseDto spellToDelete = _spellService.DeleteSpell(id);

            if(spellToDelete == null || spellToDelete?.Id == 0)
            {
                return NotFound();
            }

            return Ok(spellToDelete);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type=typeof(SpellResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<SpellResponseDto> UpdateSpell(int id, CreateSpellDto updatedSpell)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(updatedSpell == null || string.IsNullOrWhiteSpace(updatedSpell?.Name))
            {
                return BadRequest("Please enter name");
            }

            var updatedSpellToReturn = _spellService.UpdateSpell(id, updatedSpell);

            if(updatedSpellToReturn == null || updatedSpellToReturn?.Id == 0)
            {
                return NotFound();
            }

            return Ok(updatedSpellToReturn);
        }
    }
}