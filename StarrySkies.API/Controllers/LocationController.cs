using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarrySkies.Data.Models;
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
        public ActionResult<List<Location>> GetAllLocations()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            List<Location> allLocations = new List<Location>();
            allLocations = _locationService.GetAllLocations().ToList();

            return Ok(allLocations);
        }

        [HttpGet("{id}", Name = "GetLocation")]
        public ActionResult<Location> GetLocation(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var location = _locationService.GetLocation(id);

            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        [HttpPost]
        public ActionResult CreateLocation([FromBody] Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var createdLocation = _locationService.CreateLocation(location);

            return CreatedAtAction(nameof(GetLocation), new { id = createdLocation.Id }, createdLocation);
        }

    }
}