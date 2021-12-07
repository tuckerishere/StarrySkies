using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using StarrySkies.Data.Data;
using StarrySkies.Data.Models;
using StarrySkies.Data.Repositories.LocationRepo;
using StarrySkies.Services.DTOs;
using StarrySkies.Services.Mapping;
using StarrySkies.Services.Services.Locations;
using Xunit;

namespace StarrySkies.Tests.Service.Tests
{
    public class LocationServiceTest
    {
        private readonly IMapper _mapper;
        public LocationServiceTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new DtoToModel());
                    mc.AddProfile(new ModelToDto());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }
        [Fact]
        public void CreateNewLocationTest()
        {
            //Arrange
            var locationRepo = new Mock<ILocationRepository>();
            Location location = new Location();
            location.Name = "Test";
            location.Description = "Testing";
            location.Id = 1;

            CreateLocationDto locationToCreate = new CreateLocationDto();
            locationToCreate.Name = "Test";
            locationToCreate.Description = "Testing";

            locationRepo.Setup(x => x.CreateLocation(location));

            //Act
            var locationService = new LocationService(locationRepo.Object, _mapper);

            var results = locationService.CreateLocation(locationToCreate);

            //Assert
            Assert.Equal("Test", results.Name);
            Assert.Equal("Testing", results.Description);
        }

        [Fact]
        public void GetLocationTest()
        {
            //Arrange
            var locationRepo = new Mock<ILocationRepository>();
            Location location = new Location();
            location.Id = 1;
            location.Name = "Home";
            location.Description = "Comfy";

            locationRepo.Setup(x => x.GetLocationById(location.Id)).Returns(location);
            var locationService = new LocationService(locationRepo.Object, _mapper);

            //Act
            var results = locationService.GetLocation(1);

            //Assert
            Assert.Equal(1, results.Id);
            Assert.Equal("Home", results.Name);
            Assert.Equal("Comfy", results.Description);
        }

        [Fact]
        public void LocationServiceGetAllLocationsTest()
        {
            //Arrange
            var locationRepo = new Mock<ILocationRepository>();
            Location location = new Location();
            location.Id = 1;
            location.Name = "Hogwarts";
            location.Description = "Castle";
            Location locationTwo = new Location();
            locationTwo.Id = 2;
            locationTwo.Name = "House";
            locationTwo.Description = "Small";
            ICollection<Location> locationCollection = new List<Location>();
            locationCollection.Add(location);
            locationCollection.Add(locationTwo);

            locationRepo.Setup(x => x.GetAllLocations()).Returns(locationCollection);
            var locationService = new LocationService(locationRepo.Object, _mapper);

            //Act
            var results = locationService.GetAllLocations();

            //Assert
            Assert.Equal(1, results.ElementAt(0).Id);
            Assert.Equal(2, results.ElementAt(1).Id);
            Assert.Equal(2, results.Count());
        }

        [Fact]
        public void LocationServiceReturnsNullTest()
        {
            //Arrange
            var locationRepo = new Mock<ILocationRepository>();
            ICollection<Location> locationCollection = new List<Location>();
            locationRepo.Setup(x => x.GetAllLocations()).Returns(locationCollection);

            var locationService = new LocationService(locationRepo.Object, _mapper);

            //Act
            var results = locationService.GetAllLocations();

            //Assert
            Assert.Empty(results);
        }

        [Fact]
        public void LocationServiceLocationReturnsNull()
        {
            //Arrange
            var locationRepo = new Mock<ILocationRepository>();
            Location location = new Location();

            locationRepo.Setup(x => x.GetLocationById(1));

            var locationService = new LocationService(locationRepo.Object, _mapper);

            //Act
            var results = locationService.GetLocation(1);

            //Assert
            Assert.Null(results);
        }

        [Fact]
        public void CreateLocationWithNullNameTest()
        {
            //Assert
            var locationRepo = new Mock<ILocationRepository>();
            Location location = new Location();
            CreateLocationDto createdLocation = new CreateLocationDto();

            locationRepo.Setup(x => x.CreateLocation(location));

            var locationService = new LocationService(locationRepo.Object, _mapper);

            //Act
            var results = locationService.CreateLocation(createdLocation);

            //Arrange
            locationRepo.Verify(l => l.CreateLocation(It.IsAny<Location>()), Times.Never);
        }
    }
}