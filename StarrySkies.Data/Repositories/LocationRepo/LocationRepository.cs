using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StarrySkies.Data.Data;
using StarrySkies.Data.Models;

namespace StarrySkies.Data.Repositories.LocationRepo
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _context;
        public LocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void CreateLocation(Location location)
        {
            _context.Locations.Add(location);
        }

        public void DeleteLocation(Location location)
        {
            _context.Locations.Remove(location);
        }

        public ICollection<Location> GetAllLocations()
        {
            return _context.Locations.OrderBy(n => n.Name).ToList();
        }

        public Location GetLocationById(int id)
        {
            return _context.Locations.Find(id);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void UpdateLocation(Location location)
        {
            _context.Locations.Update(location);
        }
    }
}