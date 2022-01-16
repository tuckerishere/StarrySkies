using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StarrySkies.API.Controllers;
using StarrySkies.Services.DTOs.VocationDtos;
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

            vocationService.Setup(x => x.GetVocations()).Returns(vocationList);
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.GetAllVocations();
            var result = (initialResult.Result as OkObjectResult).Value as List<VocationResponseDto>;

            //Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetVocationsEmptyList()
        {
            //Arrange
            var vocationService = new Mock<IVocationService>();
            var vocationList = new List<VocationResponseDto>();

            vocationService.Setup(x => x.GetVocations()).Returns(vocationList);
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initalResult = vocationController.GetAllVocations();
            var result = (initalResult.Result as OkObjectResult).Value as List<VocationResponseDto>;

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetAllVocationsStatusCodeTest()
        {
            //Arrange
            var vocationService = new Mock<IVocationService>();
            var vocationList = new List<VocationResponseDto>();

            vocationService.Setup(x => x.GetVocations()).Returns(vocationList);
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

            vocationService.Setup(x => x.GetVocationById(1)).Returns(vocation);
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.GetVocation(1);
            var result = (initialResult.Result as OkObjectResult).Value as VocationResponseDto;

            //Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Thief", result.Name);
        }

        [Fact]
        public void GetVocationByIdStatusCode200Test()
        {
            //Arrange
            var vocationService = new Mock<IVocationService>();
            var vocation = new VocationResponseDto();
            vocation.Id = 1;
            vocation.Name = "Sage";

            vocationService.Setup(x => x.GetVocationById(1)).Returns(vocation);
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

            vocationService.Setup(x => x.GetVocationById(1)).Returns(vocation);
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.GetVocation(1);
            var result = (initialResult.Result as NotFoundResult).StatusCode;
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

            vocationService.Setup(x => x.CreateVocation(vocationToCreate)).Returns(vocationToReturn);
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.CreateVocation(vocationToCreate);
            var result = (initialResult.Result as CreatedAtActionResult).Value as VocationResponseDto;

            //Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Mage", result.Name);
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

            vocationService.Setup(x => x.CreateVocation(vocationToCreate)).Returns(vocationToReturn);
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
            vocationService.Setup(x => x.CreateVocation(null));

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

            vocationService.Setup(x => x.DeleteVocation(1)).Returns(vocationToDelete);
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initalResult = vocationController.DeleteVocation(1);
            var result = (initalResult.Result as OkObjectResult).Value as VocationResponseDto;

            //Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Sage", result.Name);
        }

        [Fact]
        public void DeleteVocationControllerStatusCodeOK()
        {
            //Arrange
            var vocationService = new Mock<IVocationService>();
            var vocationToDelete = new VocationResponseDto();
            vocationToDelete.Id = 1;
            vocationToDelete.Name = "Mage";

            vocationService.Setup(x => x.DeleteVocation(1)).Returns(vocationToDelete);
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
            vocationService.Setup(x => x.DeleteVocation(1));

            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.DeleteVocation(1);
            var result = (initialResult.Result as NotFoundResult).StatusCode;

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

            vocationService.Setup(x => x.UpdateVocation(1, updatedVocation)).Returns(vocationToUpdate);
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.UpdateVocation(1, updatedVocation);
            var result = (initialResult.Result as OkObjectResult).Value as VocationResponseDto;

            //Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Thief", result.Name);
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

            vocationService.Setup(x => x.UpdateVocation(1, updateVocation)).Returns(vocationToUpdate);
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

            vocationService.Setup(x => x.UpdateVocation(1, updatedVocation));
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.UpdateVocation(1, updatedVocation);
            var result = (initialResult.Result as NotFoundResult).StatusCode;

            //Assert
            Assert.Equal(404, result);
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

            vocationService.Setup(x => x.UpdateVocation(1, updatedVocation)).Returns(vocationToUpdate);
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

            vocationService.Setup(x => x.UpdateVocation(1, null)).Returns(vocationToUpdate);
            var vocationController = new VocationController(vocationService.Object);

            //Act
            var initialResult = vocationController.UpdateVocation(1, null);
            var result = (initialResult.Result as BadRequestObjectResult).StatusCode;

            //Assert
            Assert.Equal(400, result);
        }
    }
}