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
        public void GetLocationNullTest()
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
            var results = locationService.GetLocation(2);

            //Assert
            Assert.Equal(0, results.Id);
            Assert.Null(results.Name);
            Assert.Null(results.Description);
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
        public void ReturnEmptyListGetAllLocations()
        {
            //Arrange
            var locationRepo = new Mock<ILocationRepository>();
            ICollection<Location> locationList = new List<Location>();
            locationRepo.Setup(x => x.GetAllLocations()).Returns(locationList);

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
            Assert.Null(results.Name);
            Assert.Equal(0, results.Id);
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

        [Fact]
        public void DeleteLocationServiceTest()
        {
            //Assert
            var locationRepo = new Mock<ILocationRepository>();
            Location location = new Location();
            location.Id = 1;
            location.Name = "Test";
            location.Description = "Testeroni";

            locationRepo.Setup(x => x.GetLocationById(location.Id)).Returns(location);
            locationRepo.Setup(x => x.DeleteLocation(location));
            locationRepo.Setup(x => x.SaveChanges());

            var locationService = new LocationService(locationRepo.Object, _mapper);

            //Act
            var results = locationService.DeleteLocation(location.Id);

            //Assert
            Assert.Equal(1, results.Id);
            Assert.Equal("Test", results.Name);
            locationRepo.Verify(l => l.DeleteLocation(It.IsAny<Location>()), Times.Once);
        }

        [Fact]
        public void DeleteLocationNotFound()
        {
            //Assert
            var locationRepo = new Mock<ILocationRepository>();
            Location location = new Location();
            location.Id = 0;
            location.Name = null;
            location.Description = null;

            locationRepo.Setup(x => x.GetLocationById(2)).Returns(location);

            var locationService = new LocationService(locationRepo.Object, _mapper);

            //Act
            var results = locationService.DeleteLocation(2);

            //Assert
            Assert.Equal(0, results.Id);
            Assert.Null(results.Name);
            locationRepo.Verify(l => l.DeleteLocation(It.IsAny<Location>()), Times.Never);
        }

        [Fact]
        public void UpdateLocationServiceTest()
        {
            //Assert
            var locationRepo = new Mock<ILocationRepository>();
            Location location = new Location();
            location.Id = 1;
            location.Name = "Hogwarts";
            location.Description = "Castle";

            CreateLocationDto updatedLocation = new CreateLocationDto();
            updatedLocation.Name = "Bat Cave";
            updatedLocation.Description = "Secret";

            locationRepo.Setup(x => x.GetLocationById(location.Id)).Returns(location);

            var locationService = new LocationService(locationRepo.Object, _mapper);

            //Act
            var results = locationService.UpdateLocation(1, updatedLocation);

            //Assert
            Assert.Equal(1, results.Id);
            Assert.Equal("Bat Cave", results.Name);
            Assert.Equal("Secret", results.Description);
            locationRepo.Verify(x => x.UpdateLocation(It.IsAny<Location>()), Times.Once);
        }

        [Fact]
        public void UpdateLocationNotFoundTest()
        {
            var locationRepo = new Mock<ILocationRepository>();
            Location location = new Location();
            location.Id = 1;
            location.Name = "Hogwarts";
            location.Description = "Castle";

            CreateLocationDto updatedLocation = new CreateLocationDto();
            location.Name = "Bat Cave";

            locationRepo.Setup(x => x.GetLocationById(1)).Returns(location);

            var locationService = new LocationService(locationRepo.Object, _mapper);

            //Act
            var results = locationService.UpdateLocation(2, updatedLocation);

            //Assert
            Assert.Equal(0, results.Id);
            Assert.Null(results.Name);
            locationRepo.Verify(x => x.UpdateLocation(It.IsAny<Location>()), Times.Never);
        }

        [Fact]
        public void UpdatedLocationNameNullTest()
        {
            //Arrange
            var locationRepo = new Mock<ILocationRepository>();
            Location location = new Location();
            location.Id = 1;
            location.Name = "Hogwarts";
            location.Description = "Castle";

            CreateLocationDto updatedLocation = new CreateLocationDto();
            updatedLocation.Name = null;

            locationRepo.Setup(x => x.GetLocationById(1)).Returns(location);

            var locationService = new LocationService(locationRepo.Object, _mapper);

            //Act
            var results = locationService.UpdateLocation(1, updatedLocation);

            //Assert
            Assert.Equal(0, results.Id);
            Assert.Null(results.Name);
            locationRepo.Verify(x => x.UpdateLocation(It.IsAny<Location>()), Times.Never);
        }
    }
}