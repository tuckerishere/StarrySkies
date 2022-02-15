using System.ComponentModel.Design;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StarrySkies.API.Controllers;
using StarrySkies.Services.DTOs.VocationDtos;
using StarrySkies.Services.ResponseModels;
using StarrySkies.Services.Services.Vocations;
using Xunit;

namespace StarrySkies.Tests.Controllers.Tests
{
    public class VocationControllerTest
    {
        [Fact]
        public void GetAllVocationsTest()
        {
            //Arrange
            var vocationService = new Mock<IVocationService>();
            var vocationOne = new VocationResponseDto();
            vocationOne.Id = 1;
            vocationOne.Name = "Barbarian";
            var vocationTwo = new VocationResponseDto();
            vocationOne.Id = 2;
            vocationTwo.Name = "Thief";
            var vocationList = new List<VocationResponseDto>();
            vocationList.Add(vocationOne);
            vocationList.Add(vocationTwo);
            var serviceResponse = new ServiceResponse<ICollection<VocationResponseDto>>();
            serviceResponse.Data = vocationList;

            vocationService.Setup(x => x.GetVocations()).Returns(serviceResponse);
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.GetAllVocations();
            var result = (initialResult.Result as OkObjectResult).Value as ServiceResponse<ICollection<VocationResponseDto>>;

            //Assert
            Assert.Equal(2, result.Data.Count);
        }

        [Fact]
        public void GetVocationsEmptyList()
        {
            //Arrange
            var vocationService = new Mock<IVocationService>();
            var vocationList = new List<VocationResponseDto>();
            var serviceResponse = new ServiceResponse<ICollection<VocationResponseDto>>();

            vocationService.Setup(x => x.GetVocations()).Returns(serviceResponse);
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initalResult = vocationController.GetAllVocations();
            var result = (initalResult.Result as OkObjectResult).Value as ServiceResponse<ICollection<VocationResponseDto>>;

            //Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public void GetAllVocationsStatusCodeTest()
        {
            //Arrange
            var vocationService = new Mock<IVocationService>();
            var vocationList = new List<VocationResponseDto>();
            var serviceResponse = new ServiceResponse<ICollection<VocationResponseDto>>();

            vocationService.Setup(x => x.GetVocations()).Returns(serviceResponse);
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.GetAllVocations();
            var result = (initialResult.Result as OkObjectResult).StatusCode;

            //Assert
            Assert.Equal(200, result);
        }

        [Fact]
        public void GetVocationByIdTest()
        {
            //Arrange
            var vocationService = new Mock<IVocationService>();
            var vocation = new VocationResponseDto();
            vocation.Id = 1;
            vocation.Name = "Thief";
            var serviceResponse = new ServiceResponse<VocationResponseDto>();
            serviceResponse.Data = vocation;

            vocationService.Setup(x => x.GetVocationById(1)).Returns(serviceResponse);
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.GetVocation(1);
            var result = (initialResult.Result as OkObjectResult).Value as ServiceResponse<VocationResponseDto>;

            //Assert
            Assert.Equal(1, result.Data.Id);
            Assert.Equal("Thief", result.Data.Name);
        }

        [Fact]
        public void GetVocationByIdStatusCode200Test()
        {
            //Arrange
            var vocationService = new Mock<IVocationService>();
            var vocation = new VocationResponseDto();
            vocation.Id = 1;
            vocation.Name = "Sage";
            var serviceResponse = new ServiceResponse<VocationResponseDto>();
            serviceResponse.Data = vocation;

            vocationService.Setup(x => x.GetVocationById(1)).Returns(serviceResponse);
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.GetVocation(1);
            var result = (initialResult.Result as OkObjectResult).StatusCode;

            //Assert
            Assert.Equal(200, result);
        }

        [Fact]
        public void GetVocationDoesntExist()
        {
            //Arrange
            var vocationService = new Mock<IVocationService>();
            var vocation = new VocationResponseDto();
            var serviceResponse = new ServiceResponse<VocationResponseDto>();
            serviceResponse.Success = false;

            vocationService.Setup(x => x.GetVocationById(1)).Returns(serviceResponse);
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.GetVocation(1);
            var result = (initialResult.Result as NotFoundObjectResult).StatusCode;
            //Assert
            Assert.Equal(404, result);
        }

        [Fact]
        public void CreateVocationControllerTest()
        {
            //Arrange
            var vocationService = new Mock<IVocationService>();
            CreateVocationDto vocationToCreate = new CreateVocationDto();
            vocationToCreate.Name = "Mage";
            VocationResponseDto vocationToReturn = new VocationResponseDto();
            vocationToReturn.Id = 1;
            vocationToReturn.Name = "Mage";
            var serviceResponse = new ServiceResponse<VocationResponseDto>();
            serviceResponse.Data = vocationToReturn;

            vocationService.Setup(x => x.CreateVocation(vocationToCreate)).Returns(serviceResponse);
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.CreateVocation(vocationToCreate);
            var result = (initialResult.Result as CreatedAtActionResult).Value as ServiceResponse<VocationResponseDto>;

            //Assert
            Assert.Equal(1, result.Data.Id);
            Assert.Equal("Mage", result.Data.Name);
        }

        [Fact]
        public void CreateVocationStatusCodeTestOk()
        {
            //Arrange
            var vocationService = new Mock<IVocationService>();
            var vocationToCreate = new CreateVocationDto();
            vocationToCreate.Name = "Thief";
            var vocationToReturn = new VocationResponseDto();
            vocationToReturn.Id = 1;
            vocationToReturn.Name = "Thief";
            var serviceResponse = new ServiceResponse<VocationResponseDto>();
            serviceResponse.Data = vocationToReturn;

            vocationService.Setup(x => x.CreateVocation(vocationToCreate)).Returns(serviceResponse);
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.CreateVocation(vocationToCreate);
            var result = (initialResult.Result as CreatedAtActionResult).StatusCode;

            //Assert
            Assert.Equal(201, result);
        }

        [Fact]
        public void CreateVocationNullTest()
        {
            //Arrange
            var vocationService = new Mock<IVocationService>();
            var vocationToReturn = new VocationResponseDto();
            var serviceResponse = new ServiceResponse<VocationResponseDto>();
            serviceResponse.Success = false;
            vocationService.Setup(x => x.CreateVocation(null)).Returns(serviceResponse);

            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.CreateVocation(null);
            var result = (initialResult.Result as BadRequestObjectResult).StatusCode;

            //Assert
            Assert.Equal(400, result);
        }

        [Fact]
        public void CreateNewVocationEmptyString()
        {
            //Arrange
            var vocationService = new Mock<IVocationService>();
            var vocationToCreate = new CreateVocationDto();
            vocationToCreate.Name = "";
            var vocationToReturn = new VocationResponseDto();
            var serviceResponse = new ServiceResponse<VocationResponseDto>();
            serviceResponse.Success = false;

            vocationService.Setup(x => x.CreateVocation(vocationToCreate)).Returns(serviceResponse);

            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.CreateVocation(vocationToCreate);
            var result = (initialResult.Result as BadRequestObjectResult).StatusCode;

            //Assert
            Assert.Equal(400, result);
        }

        [Fact]
        public void DeleteVocationControllerTest()
        {
            //Arrange
            var vocationService = new Mock<IVocationService>();
            var vocationToDelete = new VocationResponseDto();
            vocationToDelete.Id = 1;
            vocationToDelete.Name = "Sage";
            var serviceResponse = new ServiceResponse<VocationResponseDto>();
            serviceResponse.Data = vocationToDelete;

            vocationService.Setup(x => x.DeleteVocation(1)).Returns(serviceResponse);
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initalResult = vocationController.DeleteVocation(1);
            var result = (initalResult.Result as OkObjectResult).Value as ServiceResponse<VocationResponseDto>;

            //Assert
            Assert.Equal(1, result.Data.Id);
            Assert.Equal("Sage", result.Data.Name);
        }

        [Fact]
        public void DeleteVocationControllerStatusCodeOK()
        {
            //Arrange
            var vocationService = new Mock<IVocationService>();
            var vocationToDelete = new VocationResponseDto();
            vocationToDelete.Id = 1;
            vocationToDelete.Name = "Mage";
            var serviceResponse = new ServiceResponse<VocationResponseDto>();
            serviceResponse.Data = vocationToDelete;

            vocationService.Setup(x => x.DeleteVocation(1)).Returns(serviceResponse);
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.DeleteVocation(1);
            var result = (initialResult.Result as OkObjectResult).StatusCode;

            //Assert
            Assert.Equal(200, result);
        }

        [Fact]
        public void DeleteVocationDoesNotExist()
        {
            //Arrange
            var vocationService = new Mock<IVocationService>();
            var serviceResponse = new ServiceResponse<VocationResponseDto>();
            serviceResponse.Success = false;
            vocationService.Setup(x => x.DeleteVocation(1)).Returns(serviceResponse);

            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.DeleteVocation(1);
            var result = (initialResult.Result as NotFoundObjectResult).StatusCode;

            //Assert
            Assert.Equal(404, result);
        }

        [Fact]
        public void UpdateVocationControllerTest()
        {
            //Arrange
            var vocationService = new Mock<IVocationService>();
            var updatedVocation = new CreateVocationDto();
            updatedVocation.Name = "Theif";
            var vocationToUpdate = new VocationResponseDto();
            vocationToUpdate.Id = 1;
            vocationToUpdate.Name = "Thief";
            var serviceResponse = new ServiceResponse<VocationResponseDto>();
            serviceResponse.Data = vocationToUpdate;

            vocationService.Setup(x => x.UpdateVocation(1, updatedVocation)).Returns(serviceResponse);
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.UpdateVocation(1, updatedVocation);
            var result = (initialResult.Result as OkObjectResult).Value as ServiceResponse<VocationResponseDto>;

            //Assert
            Assert.Equal(1, result.Data.Id);
            Assert.Equal("Thief", result.Data.Name);
        }

        [Fact]
        public void UpdateControllerTestStatusCodeOk()
        {
            //Arrange
            var vocationService = new Mock<IVocationService>();
            var updateVocation = new CreateVocationDto();
            updateVocation.Name = "Mage";
            var vocationToUpdate = new VocationResponseDto();
            vocationToUpdate.Id = 1;
            vocationToUpdate.Name = "Mage";
            var serviceResponse = new ServiceResponse<VocationResponseDto>();
            serviceResponse.Data = vocationToUpdate;

            vocationService.Setup(x => x.UpdateVocation(1, updateVocation)).Returns(serviceResponse);
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.UpdateVocation(1, updateVocation);
            var result = (initialResult.Result as OkObjectResult).StatusCode;

            //Assert
            Assert.Equal(200, result);
        }

        [Fact]
        public void UpateControllerTestVocationDoesNotExist()
        {
            //Arrange
            var vocationService = new Mock<IVocationService>();
            var updatedVocation = new CreateVocationDto();
            updatedVocation.Name = "Thief";
            var serviceResponse = new ServiceResponse<VocationResponseDto>();
            serviceResponse.Success = false;

            vocationService.Setup(x => x.UpdateVocation(1, updatedVocation)).Returns(serviceResponse);
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.UpdateVocation(1, updatedVocation);
            var result = (initialResult.Result as BadRequestObjectResult).StatusCode;

            //Assert
            Assert.Equal(400, result);
        }

        [Fact]
        public void UpateControllerTestUpdateNameEmpty()
        {
            //Arrange
            var vocationService = new Mock<IVocationService>();
            var updatedVocation = new CreateVocationDto();
            updatedVocation.Name = "";
            var vocationToUpdate = new VocationResponseDto();
            vocationToUpdate.Id = 1;
            vocationToUpdate.Name = "Thief";
            var serviceResponse = new ServiceResponse<VocationResponseDto>();
            serviceResponse.Success = false;

            vocationService.Setup(x => x.UpdateVocation(1, updatedVocation)).Returns(serviceResponse);
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.UpdateVocation(1, updatedVocation);
            var result = (initialResult.Result as BadRequestObjectResult).StatusCode;

            //Assert
            Assert.Equal(400, result);
        }

        [Fact]
        public void UpateControllerTestUpdateNull()
        {
            //Arrange
            var vocationService = new Mock<IVocationService>();
            var vocationToUpdate = new VocationResponseDto();
            vocationToUpdate.Id = 1;
            vocationToUpdate.Name = "Thief";
            var serverResponse = new ServiceResponse<VocationResponseDto>();
            serverResponse.Success = false;

            vocationService.Setup(x => x.UpdateVocation(1, null)).Returns(serverResponse);
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.UpdateVocation(1, null);
            var result = (initialResult.Result as BadRequestObjectResult).StatusCode;

            //Assert
            Assert.Equal(400, result);
        }
    }
}