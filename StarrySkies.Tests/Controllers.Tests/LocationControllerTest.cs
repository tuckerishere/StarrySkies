using System.Runtime.Intrinsics.X86;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using StarrySkies.API.Controllers;
using StarrySkies.Services.DTOs;
using StarrySkies.Services.Mapping;
using StarrySkies.Services.Services.Locations;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using StarrySkies.Services.ResponseModels;

namespace StarrySkies.Tests.Controllers.Tests
{
    public class LocationControllerTest
    {
        [Fact]
        public void GetAllLocationsControllerTest()
        {
            //Arrange
            var locationService = new Mock<ILocationService>();
            LocationResponseDto location = new LocationResponseDto();
            location.Id = 1;
            location.Name = "Test";
            location.Description = "Testeroni";
            LocationResponseDto locationTwo = new LocationResponseDto();
            locationTwo.Id = 2;
            locationTwo.Name = "Spira";
            locationTwo.Description = "Spiral";
            ICollection<LocationResponseDto> locationList = new List<LocationResponseDto>();
            locationList.Add(location);
            locationList.Add(locationTwo);
            var serviceResponse = new ServiceResponse<ICollection<LocationResponseDto>>();
            serviceResponse.Data = locationList;

            locationService.Setup(x => x.GetAllLocations()).Returns(serviceResponse);

            var locationController = new LocationController(locationService.Object);

            //Act
            var results = locationController.GetAllLocations();

            //Assert
            var listResults = (results.Result as OkObjectResult).Value as ServiceResponse<List<LocationResponseDto>>;
            Assert.Equal(2, listResults.Data.Count());
            Assert.Equal("Spira", listResults.Data[1].Name);
        }

        [Fact]
        public void GetAllLocationsEmpty()
        {
            //Arrange
            var locationService = new Mock<ILocationService>();
            ICollection<LocationResponseDto> locationList = new List<LocationResponseDto>();
            var serviceResponse = new ServiceResponse<ICollection<LocationResponseDto>>();
            serviceResponse.Data = locationList;

            locationService.Setup(x => x.GetAllLocations()).Returns(serviceResponse);

            var locationController = new LocationController(locationService.Object);

            //Act
            var initalReults = locationController.GetAllLocations();

            //Assert
            var results = (initalReults.Result as OkObjectResult).Value as ServiceResponse<List<LocationResponseDto>>;
            Assert.Empty(results.Data);
        }

        [Fact]
        public void GetLocationByIdTest()
        {
            //Arrange
            var locationService = new Mock<ILocationService>();
            LocationResponseDto location = new LocationResponseDto();
            location.Id = 1;
            location.Name = "Terra";
            location.Description = "Earth";
            var serviceResponse = new ServiceResponse<LocationResponseDto>();
            serviceResponse.Data = location;

            locationService.Setup(x => x.GetLocation(location.Id)).Returns(serviceResponse);

            var locationController = new LocationController(locationService.Object);

            //Act
            var initialResult = locationController.GetLocation(1);

            //Assert
            var result = (initialResult.Result as OkObjectResult).Value as ServiceResponse<LocationResponseDto>;
            Assert.Equal(1, result.Data.Id);
            Assert.Equal("Terra", result.Data.Name);
            Assert.Equal("Earth", result.Data.Description);

        }
        [Fact]
        public void GetLocationNoResult()
        {
            //Arrange
            var locationService = new Mock<ILocationService>();
            var serviceResponse = new ServiceResponse<LocationResponseDto>();
            serviceResponse.Success = false;

            locationService.Setup(x => x.GetLocation(1)).Returns(serviceResponse);

            var locationController = new LocationController(locationService.Object);

            //Act
            var initialResults = locationController.GetLocation(1);

            //Assert
            var result = (initialResults.Result as NotFoundObjectResult).StatusCode;
            Assert.Equal(404, result);
        }

        [Fact]
        public void CreateLocationTest()
        {
            //Arrange
            var locationService = new Mock<ILocationService>();
            LocationResponseDto locationResponse = new LocationResponseDto();
            locationResponse.Id = 1;
            locationResponse.Name = "Test";
            locationResponse.Description = "Testing";
            CreateLocationDto createdLocation = new CreateLocationDto();
            createdLocation.Name = "Test";
            createdLocation.Description = "Testing";
            var serviceResponse = new ServiceResponse<LocationResponseDto>();
            serviceResponse.Data = locationResponse;

            locationService.Setup(x => x.CreateLocation(createdLocation)).Returns(serviceResponse);

            var locationController = new LocationController(locationService.Object);

            //Act
            var initialResult = locationController.CreateLocation(createdLocation);

            var result = (initialResult.Result as CreatedAtActionResult).Value as ServiceResponse<LocationResponseDto>;

            Assert.Equal(1, result.Data.Id);
            Assert.Equal("Test", result.Data.Name);
            Assert.Equal("Testing", result.Data.Description);
        }

        [Fact]
        public void SuccessfulCreatedLocationStatusCode()
        {
            //Arrange
            var locationService = new Mock<ILocationService>();
            LocationResponseDto locationResponse = new LocationResponseDto();
            locationResponse.Id = 1;
            locationResponse.Name = "Test";
            locationResponse.Description = "Testing";
            CreateLocationDto createdLocation = new CreateLocationDto();
            createdLocation.Name = "Test";
            createdLocation.Description = "Testing";
            var serviceResponse = new ServiceResponse<LocationResponseDto>();
            serviceResponse.Data = locationResponse;

            locationService.Setup(x => x.CreateLocation(createdLocation)).Returns(serviceResponse);

            var locationController = new LocationController(locationService.Object);

            //Act
            var initialResult = locationController.CreateLocation(createdLocation);
            var result = (initialResult.Result as CreatedAtActionResult).StatusCode;

            //Assert
            Assert.Equal(201, result);
        }

        [Fact]
        public void UnsuccessfulCreatedLocationStatusCode()
        {
            //Arrange
            var locationService = new Mock<ILocationService>();
            LocationResponseDto locationResponse = new LocationResponseDto();
            locationResponse.Id = 1;
            locationResponse.Description = "Testing";
            CreateLocationDto createdLocation = new CreateLocationDto();
            createdLocation.Description = "Testing";
            var serviceResponse = new ServiceResponse<LocationResponseDto>();
            serviceResponse.Data = locationResponse;
            serviceResponse.Success = false;

            locationService.Setup(x => x.CreateLocation(createdLocation)).Returns(serviceResponse);

            var locationController = new LocationController(locationService.Object);

            //Act
            var initialResult = locationController.CreateLocation(createdLocation);
            var result = (initialResult.Result as BadRequestObjectResult).StatusCode;

            //Assert
            Assert.Equal(400, result);
        }

        [Fact]
        public void DeleteLocationTest()
        {
            //Arrange
            var locationService = new Mock<ILocationService>();
            LocationResponseDto location = new LocationResponseDto();
            location.Id = 1;
            location.Name = "Leena";
            location.Description = "Goodbye";
            ServiceResponse<LocationResponseDto> serviceResponse = new ServiceResponse<LocationResponseDto>();
            serviceResponse.Data = location;

            locationService.Setup(x => x.DeleteLocation(location.Id)).Returns(serviceResponse);

            var locationController = new LocationController(locationService.Object);

            //Act
            var initialResult = locationController.DeleteLocation(location.Id);

            var result = (initialResult.Result as OkObjectResult).Value as ServiceResponse<LocationResponseDto>;

            //Assert
            Assert.Equal(1, result.Data.Id);
            Assert.Equal("Leena", result.Data.Name);
            Assert.Equal("Goodbye", result.Data.Description);
        }
        [Fact]
        public void SuccessfulDeleteLocationStatusCodeTest()
        {
            //Arrange
            var locationService = new Mock<ILocationService>();
            LocationResponseDto location = new LocationResponseDto();
            location.Id = 1;
            location.Name = "Leena";
            location.Description = "Goodbye";
            var serviceResponse = new ServiceResponse<LocationResponseDto>();
            serviceResponse.Data = location;

            locationService.Setup(x => x.DeleteLocation(location.Id)).Returns(serviceResponse);

            var locationController = new LocationController(locationService.Object);

            //Act
            var initialResult = locationController.DeleteLocation(location.Id);

            var result = (initialResult.Result as OkObjectResult).StatusCode;

            //Assert
            Assert.Equal(200, result);
        }

        [Fact]
        public void DeleteLocationNotFound()
        {
            //Arrange
            var locationService = new Mock<ILocationService>();
            var serviceResponse = new ServiceResponse<LocationResponseDto>();
            serviceResponse.Success = false;
            locationService.Setup(x => x.DeleteLocation(1)).Returns(serviceResponse);

            var locationController = new LocationController(locationService.Object);

            //Act
            var initialResults = locationController.DeleteLocation(1);

            var result = (initialResults.Result as NotFoundObjectResult).StatusCode;

            //Assert
            Assert.Equal(404, result);
        }

        [Fact]
        public void UpdateLocationTest()
        {
            //Arrange
            var locationService = new Mock<ILocationService>();
            LocationResponseDto location = new LocationResponseDto();
            location.Id = 1;
            location.Name = "Spira";
            location.Description = "Spiral";
            CreateLocationDto updatedLocation = new CreateLocationDto();
            updatedLocation.Name = "Terra";
            updatedLocation.Description = "Earth";
            LocationResponseDto updatedResponse = new LocationResponseDto();
            updatedResponse.Id = 1;
            updatedResponse.Name = "Terra";
            updatedResponse.Description = "Earth";
            var serviceResponse = new ServiceResponse<LocationResponseDto>();
            serviceResponse.Data = updatedResponse;

            locationService.Setup(x => x.UpdateLocation(location.Id, updatedLocation)).Returns(serviceResponse);

            var locationController = new LocationController(locationService.Object);

            //Act
            var initialResults = locationController.UpdateLocation(1, updatedLocation);
            var results = (initialResults.Result as OkObjectResult).Value as ServiceResponse<LocationResponseDto>;

            //Assert
            Assert.Equal(1, results.Data.Id);
            Assert.Equal("Terra", results.Data.Name);
            Assert.Equal("Earth", results.Data.Description);
        }

        [Fact]
        public void UpdateLocationDoesNotExist()
        {
            //Arrange
            var locationService = new Mock<ILocationService>();
            CreateLocationDto updatedLocation = new CreateLocationDto();
            updatedLocation.Name = "Updated";
            updatedLocation.Description = "oops";
            var serviceResponse = new ServiceResponse<LocationResponseDto>();
            serviceResponse.Success = false;

            locationService.Setup(x => x.UpdateLocation(1, updatedLocation)).Returns(serviceResponse);

            var locationController = new LocationController(locationService.Object);

            //Act
            var initialResults = locationController.UpdateLocation(1, updatedLocation);

            var results = (initialResults.Result as NotFoundObjectResult).StatusCode;

            //Assert
            Assert.Equal(404, results);
        }

        [Fact]
        public void UpdateLactionWithNullName()
        {
            //Arrange
            var locationService = new Mock<ILocationService>();
            LocationResponseDto location = new LocationResponseDto();
            location.Id = 1;
            location.Name = "Spira";
            location.Description = "Spiral";
            CreateLocationDto updatedLocation = new CreateLocationDto();
            updatedLocation.Description = "Earth";
            LocationResponseDto updatedResponse = new LocationResponseDto();
            updatedResponse.Id = 1;
            updatedResponse.Name = "Spira";
            updatedResponse.Description = "Spiral";
            var serviceResponse = new ServiceResponse<LocationResponseDto>();

            locationService.Setup(x => x.UpdateLocation(location.Id, updatedLocation)).Returns(serviceResponse);

            var locationController = new LocationController(locationService.Object);

            //Act
            var initialResults = locationController.UpdateLocation(1, updatedLocation);
            var results = (initialResults.Result as BadRequestObjectResult).StatusCode;

            //Assert
            Assert.Equal(400, results);
        }

        [Fact]
        public void UpdateLactionNameAsEmptyString()
        {
            //Arrange
            var locationService = new Mock<ILocationService>();
            LocationResponseDto location = new LocationResponseDto();
            location.Id = 1;
            location.Name = "Spira";
            location.Description = "Spiral";
            CreateLocationDto updatedLocation = new CreateLocationDto();
            updatedLocation.Name = "";
            updatedLocation.Description = "Earth";
            LocationResponseDto updatedResponse = new LocationResponseDto();
            updatedResponse.Id = 1;
            updatedResponse.Name = "Spira";
            updatedResponse.Description = "Spiral";
            var serviceResponse = new ServiceResponse<LocationResponseDto>();
            serviceResponse.Data = updatedResponse;

            locationService.Setup(x => x.UpdateLocation(location.Id, updatedLocation)).Returns(serviceResponse);

            var locationController = new LocationController(locationService.Object);

            //Act
            var initialResults = locationController.UpdateLocation(1, updatedLocation);
            var results = (initialResults.Result as BadRequestObjectResult).StatusCode;

            //Assert
            Assert.Equal(400, results);
        }
    }
}