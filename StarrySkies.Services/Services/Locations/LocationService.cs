using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StarrySkies.Data.Models;
using StarrySkies.Data.Repositories.LocationRepo;
using StarrySkies.Services.DTOs;

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
        public LocationResponseDto CreateLocation(CreateLocationDto location)
        {
            LocationResponseDto locationToReturn = new LocationResponseDto();
            Location createdLocation = _mapper.Map<CreateLocationDto, Location>(location);
            if (createdLocation.Name != null
                && createdLocation.Name.Trim() != "")
            {
                _locationRepo.CreateLocation(createdLocation);
                _locationRepo.SaveChanges();
                locationToReturn = _mapper.Map<Location, LocationResponseDto>(createdLocation);
            }

            return locationToReturn;
        }

        public LocationResponseDto DeleteLocation(int id)
        {
            LocationResponseDto locationToReturn = new LocationResponseDto();
            Location locationExists = _locationRepo.GetLocationById(id);

            if (locationExists != null)
            {
                if (locationExists.Id != 0)
                {
                    _locationRepo.DeleteLocation(locationExists);
                    _locationRepo.SaveChanges();
                    locationToReturn = _mapper.Map<Location, LocationResponseDto>(locationExists);
                }
            }

            return locationToReturn;
        }

        public ICollection<LocationResponseDto> GetAllLocations()
        {
            ICollection<Location> locations = _locationRepo.GetAllLocations();
            ICollection<LocationResponseDto> locationsToReturn = _mapper.Map<ICollection<Location>, ICollection<LocationResponseDto>>(locations);

            return locationsToReturn;
        }

        public LocationResponseDto GetLocation(int id)
        {
            LocationResponseDto locationToReturn = new LocationResponseDto();
            Location location = _locationRepo.GetLocationById(id);
            if (location != null)
            {
                locationToReturn = _mapper.Map<Location, LocationResponseDto>(location);
            }
            return locationToReturn;
        }

        public LocationResponseDto UpdateLocation(int id, CreateLocationDto location)
        {
            LocationResponseDto locationToReturn = new LocationResponseDto();
            Location locationToUpdate = _locationRepo.GetLocationById(id);
            if (locationToUpdate != null
                && location.Name != null
                && location.Name.Trim() != "")
            {
                Location updatedLocation = _mapper.Map<CreateLocationDto, Location>(location);
                locationToUpdate.Name = updatedLocation.Name;
                locationToUpdate.Description = updatedLocation.Description;

                _locationRepo.UpdateLocation(locationToUpdate);
                _locationRepo.SaveChanges();
                locationToReturn = _mapper.Map<Location, LocationResponseDto>(locationToUpdate);

            }
            return locationToReturn;
        }
    }
}