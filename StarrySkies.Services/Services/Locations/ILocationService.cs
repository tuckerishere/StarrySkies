using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarrySkies.Data.Models;
using StarrySkies.Services.DTOs;
using StarrySkies.Services.ResponseModels;

namespace StarrySkies.Services.Services.Locations
{
    public interface ILocationService
    {
        ServiceResponse<ICollection<LocationResponseDto>> GetAllLocations();
        ServiceResponse<LocationResponseDto> GetLocation(int id);
        ServiceResponse<LocationResponseDto> CreateLocation(CreateLocationDto location);
        ServiceResponse<LocationResponseDto> DeleteLocation(int id);
        ServiceResponse<LocationResponseDto> UpdateLocation(int id, CreateLocationDto location);
    }
}