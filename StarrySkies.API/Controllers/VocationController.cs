using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StarrySkies.Services.DTOs.VocationDtos;
using StarrySkies.Services.ResponseModels;
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
        [ProducesResponseType(200, Type = typeof(ServiceResponse<VocationResponseDto>))]
        [ProducesResponseType(400)]
        public ActionResult<ServiceResponse<VocationResponseDto>> GetVocation(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            ServiceResponse<VocationResponseDto> vocationToReturn = _vocationService.GetVocationById(id);

            if (!vocationToReturn.Success)
            {
                return NotFound(vocationToReturn);
            }

            return Ok(vocationToReturn);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ServiceResponse<List<VocationResponseDto>>))]
        [ProducesResponseType(400)]
        public ActionResult<ServiceResponse<List<VocationResponseDto>>> GetAllVocations()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var vocationsToReturn = _vocationService.GetVocations();

            return Ok(vocationsToReturn);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ServiceResponse<VocationResponseDto>))]
        [ProducesResponseType(400)]
        public ActionResult<ServiceResponse<VocationResponseDto>> CreateVocation([FromBody] CreateVocationDto vocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vocationToCreate = _vocationService.CreateVocation(vocation);

            if (!vocationToCreate.Success)
            {
                return BadRequest(vocationToCreate);
            }

            return CreatedAtAction(nameof(GetVocation), new { id = vocationToCreate.Data.Id }, vocationToCreate);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(ServiceResponse<VocationResponseDto>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public ActionResult<ServiceResponse<VocationResponseDto>> DeleteVocation(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vocationToDelete = _vocationService.DeleteVocation(id);

            if (!vocationToDelete.Success)
            {
                return NotFound(vocationToDelete);
            }

            return Ok(vocationToDelete);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(ServiceResponse<VocationResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<ServiceResponse<VocationResponseDto>> UpdateVocation(int id, CreateVocationDto vocation)
        {
            var vocationToUpdate = _vocationService.UpdateVocation(id, vocation);

            if (!vocationToUpdate.Success)
            {
                return BadRequest(vocationToUpdate);
            }

            return Ok(vocationToUpdate);
        }
    }
}