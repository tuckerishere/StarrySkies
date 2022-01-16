using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StarrySkies.Services.DTOs.VocationDtos;
using StarrySkies.Services.Services.Vocations;

namespace StarrySkies.API.Controllers
{
    [ApiController]
    [Route("/api/vocations")]
    public class VocationController : ControllerBase
    {
        private readonly IVocationService _vocationService;

        public VocationController(IVocationService vocationService)
        {
            _vocationService = vocationService;
        }

        [HttpGet("id", Name = "GetVocation")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type=typeof(VocationResponseDto))]
        [ProducesResponseType(400)]
        public ActionResult<VocationResponseDto> GetVocation(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            VocationResponseDto vocationToReturn = _vocationService.GetVocationById(id);

            if(vocationToReturn == null || vocationToReturn?.Id == 0)
            {
                return NotFound();
            }

            return Ok(vocationToReturn);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<List<VocationResponseDto>> GetAllVocations()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var vocationsToReturn = _vocationService.GetVocations();

            return Ok(vocationsToReturn);
        }

        [HttpPost]
        [ProducesResponseType(201, Type=typeof(VocationResponseDto))]
        [ProducesResponseType(400)]
        public ActionResult<VocationResponseDto> CreateVocation([FromBody] CreateVocationDto vocation)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VocationResponseDto vocationToCreate = new VocationResponseDto();

            if(vocation == null || string.IsNullOrEmpty(vocation?.Name))
            {
                return BadRequest("Please enter Name.");
            }

            vocationToCreate = _vocationService.CreateVocation(vocation);

            return CreatedAtAction(nameof(GetVocation), new { id = vocationToCreate.Id }, vocationToCreate);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type=typeof(VocationResponseDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public ActionResult<VocationResponseDto> DeleteVocation(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vocationToDelete = _vocationService.DeleteVocation(id);

            if(vocationToDelete == null || vocationToDelete?.Id == 0)
            {
                return NotFound();
            }

            return Ok(vocationToDelete);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type=typeof(VocationResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<VocationResponseDto> UpdateVocation(int id, CreateVocationDto vocation)
        {
            if(vocation == null || string.IsNullOrEmpty(vocation?.Name))
            {
                return BadRequest("Please enter Vocation Name");
            }

            var vocationToUpdate = _vocationService.UpdateVocation(id, vocation);

            if(vocationToUpdate == null || vocationToUpdate?.Id ==0)
            {
                return NotFound();
            }

            return Ok(vocationToUpdate);
        }
    }
}