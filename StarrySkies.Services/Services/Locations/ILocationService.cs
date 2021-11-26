using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarrySkies.Data.Models;

namespace StarrySkies.Services.Services.Locations
{
    public interface ILocationService
    {
        ICollection<Location> GetAllLocations();
        Location GetLocation(int id);
        Location CreateLocation(Location location);
        Location DeleteLocation(Location location);
        Location UpdateLocation(int id, Location location);
    }
}