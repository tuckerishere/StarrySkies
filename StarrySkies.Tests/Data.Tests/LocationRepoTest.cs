using System;
using Microsoft.EntityFrameworkCore;
using StarrySkies.Data.Data;
using StarrySkies.Data.Repositories.LocationRepo;
using Xunit;
using Microsoft.EntityFrameworkCore.InMemory;
using StarrySkies.Data.Models;
using System.Collections.Generic;

namespace StarrySkies.Tests.Data.Tests
{
    public class LocationRepoTest
    {
        private ILocationRepository GetInMemoryLocationRepository()
        {
            DbContextOptions<ApplicationDbContext> options;
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(databaseName: "StarrySkiesTest");
            options = builder.Options;
            ApplicationDbContext applicationDbContext = new ApplicationDbContext(options);
            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Database.EnsureCreated();
            return new LocationRepository(applicationDbContext);
        }

        [Fact]
        public void CreateNewLocation()
        {
            ILocationRepository repo = GetInMemoryLocationRepository();
            Location location = new Location()
            {
                Id = 1,
                Name = "Hogwarts",
                Description = "Big Castle"
            };

            repo.CreateLocation(location);

            Location response = repo.GetLocationById(1);

            Assert.Equal(1, location.Id);
            Assert.Equal("Hogwarts", response.Name);
            Assert.Equal("Big Castle", response.Description);
        }

        [Fact]
        public void GetAllLocationsTest()
        {
            ILocationRepository repo = GetInMemoryLocationRepository();
            Location locationOne = new Location()
            {
                Id = 1,
                Name = "Home",
                Description = "Test"
            };
            Location locationTwo = new Location()
            {
                Id = 2,
                Name = "Store",
                Description = "Buy"
            };

            repo.CreateLocation(locationOne);
            repo.CreateLocation(locationTwo);
            repo.SaveChanges();

            ICollection<Location> response = repo.GetAllLocations();

            Assert.Equal(2, response.Count);
        }

        [Fact]
        public void UpdateLocationTest()
        {
            ILocationRepository repo = GetInMemoryLocationRepository();
            Location location = new Location()
            {
                Id = 1,
                Name = "Hogwarts",
                Description = "Test"
            };

            repo.CreateLocation(location);
            repo.SaveChanges();
            location.Name = "Bat Cave";
            repo.UpdateLocation(location);
            repo.SaveChanges();

            Location response = repo.GetLocationById(1);

            Assert.Equal("Bat Cave", response.Name);

        }

        [Fact]
        public void DeleteLocationTest()
        {
            ILocationRepository repo = GetInMemoryLocationRepository();
            Location location = new Location()
            {
                Id = 1,
                Name = "Home",
                Description = "Test"
            };

            repo.CreateLocation(location);
            repo.SaveChanges();
            Location response = repo.GetLocationById(1);
            Assert.Equal("Home", location.Name);

            repo.DeleteLocation(location);
            repo.SaveChanges();
            response = repo.GetLocationById(1);

            Assert.Null(response);
        }
    }
}