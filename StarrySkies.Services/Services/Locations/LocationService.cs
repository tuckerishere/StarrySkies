using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarrySkies.Data.Models;
using StarrySkies.Data.Repositories.LocationRepo;

namespace StarrySkies.Services.Services.Locations
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepo;
        public LocationService(ILocationRepository locationrepo)
        {
            _locationRepo = locationrepo;
        }
        public Location CreateLocation(Location location)
        {
            Location createdLocation = new Location();
            if (location.Name != null)
            {
                createdLocation = location;
                _locationRepo.CreateLocation(location);
                _locationRepo.SaveChanges();
            }
            return location;
        }

        public Location DeleteLocation(Location location)
        {
            Location deletedLocation = new Location();
            if (location != null)
            {
                deletedLocation = location;
                _locationRepo.DeleteLocation(location);
                _locationRepo.SaveChanges();
            }

            return deletedLocation;
        }

        public ICollection<Location> GetAllLocations()
        {
            return _locationRepo.GetAllLocations();
        }

        public Location GetLocation(int id)
        {
            var location = _locationRepo.GetLocationById(id);
            return location;
        }

        public Location UpdateLocation(int id, Location location)
        {
            
            _locationRepo.UpdateLocation(location);
            _locationRepo.SaveChanges();
            return location;
        }
    }
}