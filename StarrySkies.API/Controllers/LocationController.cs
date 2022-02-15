using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StarrySkies.Data.Models;
using StarrySkies.Services.DTOs;
using StarrySkies.Services.ResponseModels;
using StarrySkies.Services.Services.Locations;

namespace StarrySkies.API.Controllers
{
    [ApiController]
    [Route("api/locations")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ServiceResponse<List<LocationResponseDto>>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<ServiceResponse<List<LocationResponseDto>>> GetAllLocations()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            ServiceResponse<List<LocationResponseDto>> allLocations = new ServiceResponse<List<LocationResponseDto>>();
            allLocations.Data = _locationService.GetAllLocations().Data.ToList();

            return Ok(allLocations);
        }

        [HttpGet("{id}", Name = "GetLocation")]
        [ProducesResponseType(200, Type = typeof(ServiceResponse<LocationResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<LocationResponseDto> GetLocation(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            ServiceResponse<LocationResponseDto> location = _locationService.GetLocation(id);

            if (!location.Success)
            {
                return NotFound(location);
            }

            return Ok(location);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ServiceResponse<LocationResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<ServiceResponse<LocationResponseDto>> CreateLocation([FromBody] CreateLocationDto location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServiceResponse<LocationResponseDto> createdLocation = _locationService.CreateLocation(location);

            if (!createdLocation.Success)
            {
                return BadRequest(createdLocation);
            }

            return CreatedAtAction(nameof(GetLocation), new { id = createdLocation.Data.Id }, createdLocation);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(ServiceResponse<LocationResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<ServiceResponse<LocationResponseDto>> DeleteLocation(int id)
        {
            ServiceResponse<LocationResponseDto> locationToDelete = _locationService.DeleteLocation(id);
            if (!locationToDelete.Success)
            {
                return NotFound(locationToDelete);
            }

            return Ok(locationToDelete);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(LocationResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<ServiceResponse<LocationResponseDto>> UpdateLocation(int id, [FromBody] CreateLocationDto locationToUpdate)
        {

            if (locationToUpdate.Name == null || locationToUpdate.Name.Trim() == "")
            {
                return BadRequest("Please enter name.");
            }

            ServiceResponse<LocationResponseDto> updatedLocation = new ServiceResponse<LocationResponseDto>();

            updatedLocation = _locationService.UpdateLocation(id, locationToUpdate);

            if (!updatedLocation.Success)
            {
                return NotFound(updatedLocation);
            }

            return Ok(updatedLocation);
        }
    }
}