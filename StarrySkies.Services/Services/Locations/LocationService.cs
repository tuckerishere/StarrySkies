using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StarrySkies.Data.Models;
using StarrySkies.Data.Repositories.LocationRepo;
using StarrySkies.Services.DTOs;
using StarrySkies.Services.ResponseModels;

namespace StarrySkies.Services.Services.Locations
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepo;
        private readonly IMapper _mapper;
        public LocationService(ILocationRepository locationrepo, IMapper mapper)
        {
            _locationRepo = locationrepo;
            _mapper = mapper;
        }
        public ServiceResponse<LocationResponseDto> CreateLocation(CreateLocationDto location)
        {
            ServiceResponse<LocationResponseDto> locationToReturn = new ServiceResponse<LocationResponseDto>();
            Location createdLocation = _mapper.Map<CreateLocationDto, Location>(location);
            if (createdLocation.Name != null
                && createdLocation.Name.Trim() != "")
            {
                _locationRepo.CreateLocation(createdLocation);
                _locationRepo.SaveChanges();
                locationToReturn.Data = _mapper.Map<Location, LocationResponseDto>(createdLocation);
            }
            else
            {
                locationToReturn.Success = false;
                locationToReturn.Message = "Please enter location name.";
            }

            return locationToReturn;
        }

        public ServiceResponse<LocationResponseDto> DeleteLocation(int id)
        {
            ServiceResponse<LocationResponseDto> locationToReturn = new ServiceResponse<LocationResponseDto>();
            Location locationExists = _locationRepo.GetLocationById(id);

            if (locationExists != null)
            {
                _locationRepo.DeleteLocation(locationExists);
                _locationRepo.SaveChanges();
                locationToReturn.Data = _mapper.Map<Location, LocationResponseDto>(locationExists);
            }
            else
            {
                locationToReturn.Success = false;
                locationToReturn.Message = "Location not found.";
            }

            return locationToReturn;
        }

        public ServiceResponse<ICollection<LocationResponseDto>> GetAllLocations()
        {
            ICollection<Location> locations = _locationRepo.GetAllLocations();
            ServiceResponse<ICollection<LocationResponseDto>> locationsToReturn = new ServiceResponse<ICollection<LocationResponseDto>>();
            locationsToReturn.Data = _mapper.Map<ICollection<Location>, ICollection<LocationResponseDto>>(locations);

            return locationsToReturn;
        }

        public ServiceResponse<LocationResponseDto> GetLocation(int id)
        {
            ServiceResponse<LocationResponseDto> locationToReturn = new ServiceResponse<LocationResponseDto>();
            Location location = _locationRepo.GetLocationById(id);
            if (location != null)
            {
                locationToReturn.Data = _mapper.Map<Location, LocationResponseDto>(location);
            }
            else
            {
                locationToReturn.Success = false;
                locationToReturn.Message = "Location not found.";
            }
            return locationToReturn;
        }

        public ServiceResponse<LocationResponseDto> UpdateLocation(int id, CreateLocationDto location)
        {
            ServiceResponse<LocationResponseDto> locationToReturn = new ServiceResponse<LocationResponseDto>();
            Location locationToUpdate = _locationRepo.GetLocationById(id);
            if (locationToUpdate != null
                && location.Name != null
                && location.Name.Trim() != "")
            {
                locationToUpdate.Name = location.Name;
                locationToUpdate.Description = location.Description;

                _locationRepo.UpdateLocation(locationToUpdate);
                _locationRepo.SaveChanges();
                locationToReturn.Data = _mapper.Map<Location, LocationResponseDto>(locationToUpdate);
            }
            else
            {
                locationToReturn.Success = false;
                locationToReturn.Message = "Unable to updated location.";
            }
            return locationToReturn;
        }
    }
}