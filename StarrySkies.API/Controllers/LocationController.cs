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
        private readonly IMapper _mapper;
        public LocationController(ILocationService locationService, IMapper mapper)
        {
            _locationService = locationService;
            _mapper = mapper;
        }

        [HttpGet]
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
        public ActionResult<LocationResponseDto> GetLocation(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            LocationResponseDto location = _locationService.GetLocation(id);

            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        [HttpPost]
        public ActionResult CreateLocation([FromBody] CreateLocationDto location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            LocationResponseDto createdLocation = _locationService.CreateLocation(location);

            return CreatedAtAction(nameof(GetLocation), new { id = createdLocation.Id }, createdLocation);
        }

    }
}