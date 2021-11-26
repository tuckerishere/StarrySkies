using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarrySkies.Data.Models;

namespace StarrySkies.Data.Repositories.LocationRepo
{
    public interface ILocationRepository
    {
        ICollection<Location> GetAllLocations();
        Location GetLocationById(int id);
        void CreateLocation(Location location);
        void DeleteLocation(Location location);
        void UpdateLocation(Location location);
        bool SaveChanges();
    }
}