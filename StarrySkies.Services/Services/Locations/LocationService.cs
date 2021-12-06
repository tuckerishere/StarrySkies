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
            Location createdLocation = _mapper.Map<CreateLocationDto, Location>(location);
            if (createdLocation.Name != null)
            {
                _locationRepo.CreateLocation(createdLocation);
                _locationRepo.SaveChanges();
            }

            LocationResponseDto locationToReturn = _mapper.Map<Location, LocationResponseDto>(createdLocation);
            return locationToReturn;
        }

        public LocationResponseDto DeleteLocation(RequestLocationDto location)
        {
            Location deletedLocation = _mapper.Map<RequestLocationDto, Location>(location);
            Location locationExists = _locationRepo.GetLocationById(deletedLocation.Id);

            if (location != null && locationExists != null)
            {
                _locationRepo.DeleteLocation(deletedLocation);
                _locationRepo.SaveChanges();
            }

            LocationResponseDto locationToReturn = _mapper.Map<Location, LocationResponseDto>(deletedLocation);
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
            Location location = _locationRepo.GetLocationById(id);
            LocationResponseDto locationToReturn = _mapper.Map<Location, LocationResponseDto>(location);
            return locationToReturn;
        }

        public LocationResponseDto UpdateLocation(int id, CreateLocationDto location)
        {
            Location locationToUpdate = _mapper.Map<CreateLocationDto, Location>(location);
            _locationRepo.UpdateLocation(locationToUpdate);
            _locationRepo.SaveChanges();
            LocationResponseDto locationToReturn = _mapper.Map<Location, LocationResponseDto>(locationToUpdate);
            return locationToReturn;
        }
    }
}