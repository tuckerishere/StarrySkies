using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarrySkies.Data.Models;
using StarrySkies.Services.DTOs;

namespace StarrySkies.Services.Services.Locations
{
    public interface ILocationService
    {
        ICollection<LocationResponseDto> GetAllLocations();
        LocationResponseDto GetLocation(int id);
        LocationResponseDto CreateLocation(CreateLocationDto location);
        LocationResponseDto DeleteLocation(RequestLocationDto location);
        LocationResponseDto UpdateLocation(int id, CreateLocationDto location);
    }
}