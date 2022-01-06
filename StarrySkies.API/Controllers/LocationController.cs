using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StarrySkies.Data.Models;
using StarrySkies.Services.DTOs;
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
        [ProducesResponseType(200, Type = typeof(LocationResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<List<LocationResponseDto>> GetAllLocations()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            List<LocationResponseDto> allLocations = _locationService.GetAllLocations().ToList();

            return Ok(allLocations);
        }

        [HttpGet("{id}", Name = "GetLocation")]
        [ProducesResponseType(200, Type = typeof(LocationResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<LocationResponseDto> GetLocation(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            LocationResponseDto location = _locationService.GetLocation(id);

            if (location.Id == 0 || location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(LocationResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<LocationResponseDto> CreateLocation([FromBody] CreateLocationDto location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LocationResponseDto createdLocation = _locationService.CreateLocation(location);

            if (createdLocation.Name == null)
            {
                return BadRequest("Please enter name.");
            }

            return CreatedAtAction(nameof(GetLocation), new { id = createdLocation.Id }, createdLocation);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(LocationResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<LocationResponseDto> DeleteLocation(int id)
        {
            LocationResponseDto locationToDelete = _locationService.DeleteLocation(id);
            if (locationToDelete == null || locationToDelete.Id == 0)
            {
                return NotFound();
            }

            return Ok(locationToDelete);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(LocationResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<LocationResponseDto> UpdateLocation(int id, [FromBody] CreateLocationDto locationToUpdate)
        {

            if (locationToUpdate.Name == null || locationToUpdate.Name.Trim() == "")
            {
                return BadRequest("Please enter name.");
            }

            LocationResponseDto updatedLocation = new LocationResponseDto();

            updatedLocation = _locationService.UpdateLocation(id, locationToUpdate);

            if (updatedLocation.Id == 0)
            {
                return NotFound();
            }

            return Ok(updatedLocation);
        }
    }
}