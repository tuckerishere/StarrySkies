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

namespace StarrySkies.Tests.Controllers.Tests
{
    public class LocationControllerTest
    {
        private readonly IMapper _mapper;
        public LocationControllerTest()
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

            locationService.Setup(x => x.GetAllLocations()).Returns(locationList);

            var locationController = new LocationController(locationService.Object, _mapper);

            //Act
            var results = locationController.GetAllLocations();

            //Assert
            var listResults = (results.Result as OkObjectResult).Value as List<LocationResponseDto>;
            Assert.Equal(2, listResults.Count());
            Assert.Equal("Spira", listResults[1].Name);
        }

        [Fact]
        public void GetAllLocationsEmpty()
        {
            //Arrange
            var locationService = new Mock<ILocationService>();
            ICollection<LocationResponseDto> locationList = new List<LocationResponseDto>();

            locationService.Setup(x => x.GetAllLocations()).Returns(locationList);

            var locationController = new LocationController(locationService.Object, _mapper);

            //Act
            var initalReults = locationController.GetAllLocations();

            //Assert
            var results = (initalReults.Result as OkObjectResult).Value as List<LocationResponseDto>;
            Assert.Empty(results);
        }
    }
}