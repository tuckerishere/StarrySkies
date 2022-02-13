using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StarrySkies.API.Controllers;
using StarrySkies.Services.DTOs.VocationSpellDtos;
using StarrySkies.Services.ResponseModels;
using StarrySkies.Services.Services.VocationSpells;
using Xunit;

namespace StarrySkies.Tests.Controllers.Tests
{
    public class VocationSpellControllerTest
    {
        [Fact]
        public void GetVocationSpellByIdSuccessStatus()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            var vocationSpellReturn = new VocationSpellResponseDto();
            vocationSpellReturn.SpellId = 1;
            vocationSpellReturn.VocationId = 1;
            vocationSpellReturn.LevelLearned = 1;
            var serviceResponse = new ServiceResponse<VocationSpellResponseDto>();
            serviceResponse.Data = vocationSpellReturn;

            vocationSpellService.Setup(x => x.GetVocationSpell(1, 1)).Returns(serviceResponse);

            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var initialResult = vocationSpellController.GetVocationSpell(1, 1);
            var result = (initialResult.Result as OkObjectResult).StatusCode;

            //Assert
            Assert.Equal(200, result);
        }

        [Fact]
        public void GetVocationSpellNullNotFoundCode()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            var serviceResponse = new ServiceResponse<VocationSpellResponseDto>();
            serviceResponse.Success = false;
            vocationSpellService.Setup(x => x.GetVocationSpell(1, 1)).Returns(serviceResponse);

            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var initialResult = vocationSpellController.GetVocationSpell(1, 1);
            var result = (initialResult.Result as NotFoundObjectResult).StatusCode;

            //Assert
            Assert.Equal(404, result);
        }

        [Fact]
        public void GetVocationSpellNoVocationID()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            var vsToReturn = new VocationSpellResponseDto();
            vsToReturn.SpellId = 1;
            var serviceResponse = new ServiceResponse<VocationSpellResponseDto>();
            serviceResponse.Success = false;


            vocationSpellService.Setup(x => x.GetVocationSpell(1, 1)).Returns(serviceResponse);
            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var initialResult = vocationSpellController.GetVocationSpell(1, 1);
            var result = (initialResult.Result as NotFoundObjectResult).StatusCode;

            //Assert
            Assert.Equal(404, result);
        }

        [Fact]
        public void GetVocationSpellNoSpellID()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            var vsToReturn = new VocationSpellResponseDto();
            vsToReturn.VocationId = 1;
            var serviceResponse = new ServiceResponse<VocationSpellResponseDto>();
            serviceResponse.Success = false;

            vocationSpellService.Setup(x => x.GetVocationSpell(1, 1)).Returns(serviceResponse);

            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var initialResult = vocationSpellController.GetVocationSpell(1, 1);
            var result = (initialResult.Result as NotFoundObjectResult).StatusCode;

            //Assert
            Assert.Equal(404, result);
        }

        [Fact]
        public void VocationSpellReturnsSuccessfulObject()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            var vsToReturn = new VocationSpellResponseDto();
            vsToReturn.LevelLearned = 1;
            vsToReturn.SpellId = 2;
            vsToReturn.VocationId = 6;
            var serviceResponse = new ServiceResponse<VocationSpellResponseDto>();
            serviceResponse.Data = vsToReturn;

            vocationSpellService.Setup(x => x.GetVocationSpell(6, 1)).Returns(serviceResponse);

            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var initialResult = vocationSpellController.GetVocationSpell(6, 1);
            var result = (initialResult.Result as OkObjectResult).Value as ServiceResponse<VocationSpellResponseDto>;

            //Assert
            Assert.Equal(6, result.Data.VocationId);
            Assert.Equal(2, result.Data.SpellId);
            Assert.Equal(1, result.Data.LevelLearned);
        }

        [Fact]
        public void VocationSpellReturnAllSuccessStatusCode()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            var vocationSpellOne = new VocationSpellResponseDto();
            vocationSpellOne.LevelLearned = 1;
            vocationSpellOne.SpellId = 1;
            vocationSpellOne.VocationId = 1;
            var vocationSpellTwo = new VocationSpellResponseDto();
            vocationSpellOne.SpellId = 2;
            vocationSpellOne.VocationId = 2;
            vocationSpellTwo.LevelLearned = 2;
            List<VocationSpellResponseDto> vsList = new List<VocationSpellResponseDto>();
            vsList.Add(vocationSpellOne);
            vsList.Add(vocationSpellTwo);
            var serviceResponse = new ServiceResponse<ICollection<VocationSpellResponseDto>>();
            serviceResponse.Data = vsList;

            vocationSpellService.Setup(x => x.GetVocationSpells()).Returns(serviceResponse);
            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var initalResult = vocationSpellController.GetVocationSpells();
            var result = (initalResult.Result as OkObjectResult).StatusCode;

            //Assert
            Assert.Equal(200, result);
        }

        [Fact]
        public void VocationSpellReturnAllSuccess()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            var vocationSpellOne = new VocationSpellResponseDto();
            vocationSpellOne.LevelLearned = 1;
            vocationSpellOne.SpellId = 1;
            vocationSpellOne.VocationId = 1;
            var vocationSpellTwo = new VocationSpellResponseDto();
            vocationSpellOne.SpellId = 2;
            vocationSpellOne.VocationId = 2;
            vocationSpellTwo.LevelLearned = 2;
            List<VocationSpellResponseDto> vsList = new List<VocationSpellResponseDto>();
            vsList.Add(vocationSpellOne);
            vsList.Add(vocationSpellTwo);
            var serviceResponse = new ServiceResponse<ICollection<VocationSpellResponseDto>>();
            serviceResponse.Data = vsList;


            vocationSpellService.Setup(x => x.GetVocationSpells()).Returns(serviceResponse);
            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var initalResult = vocationSpellController.GetVocationSpells();
            var result = (initalResult.Result as OkObjectResult).Value as ServiceResponse<ICollection<VocationSpellResponseDto>>;

            //Assert
            Assert.Equal(2, result.Data.Count);
        }

        [Fact]
        public void VocationSpellReturnsEmptyList()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            List<VocationSpellResponseDto> vsList = new List<VocationSpellResponseDto>();
            var serviceResponse = new ServiceResponse<ICollection<VocationSpellResponseDto>>();
            serviceResponse.Data = vsList;

            vocationSpellService.Setup(x => x.GetVocationSpells()).Returns(serviceResponse);
            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var initalResult = vocationSpellController.GetVocationSpells();
            var result = (initalResult.Result as OkObjectResult).Value as ServiceResponse<ICollection<VocationSpellResponseDto>>;

            //Assert
            Assert.Empty(result.Data);
        }

        [Fact]
        public void VocationSpellEmptyListStatusCode()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            List<VocationSpellResponseDto> vsList = new List<VocationSpellResponseDto>();
            var serviceResponse = new ServiceResponse<ICollection<VocationSpellResponseDto>>();
            serviceResponse.Data = vsList;
            vocationSpellService.Setup(x => x.GetVocationSpells()).Returns(serviceResponse);
            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var initalResult = vocationSpellController.GetVocationSpells();
            var result = (initalResult.Result as OkObjectResult).StatusCode;

            //Assert
            Assert.Equal(200, result);
        }

        [Fact]
        public void CreateVocationSpellSuccessObject()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            var vsToCreate = new VocationSpellResponseDto();
            vsToCreate.SpellId = 1;
            vsToCreate.VocationId = 1;
            vsToCreate.LevelLearned = 1;
            var vsToReturn = new VocationSpellResponseDto();
            vsToReturn.SpellId = 1;
            vsToReturn.VocationId = 1;
            vsToReturn.LevelLearned = 1;
            var serviceResponse = new ServiceResponse<VocationSpellResponseDto>();
            serviceResponse.Data = vsToReturn;
            vocationSpellService.Setup(x => x.CreateVocationSpell(vsToCreate)).Returns(serviceResponse);

            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var intialResult = vocationSpellController.CreateVocationSpell(vsToCreate);
            var result = (intialResult.Result as CreatedAtActionResult).Value as ServiceResponse<VocationSpellResponseDto>;

            //Assert
            Assert.Equal(1, result.Data.LevelLearned);
            Assert.Equal(1, result.Data.SpellId);
            Assert.Equal(1, result.Data.VocationId);
        }

        [Fact]
        public void CreateVocationSpellSuccessStatusCode()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            var vsToCreate = new VocationSpellResponseDto();
            vsToCreate.SpellId = 1;
            vsToCreate.VocationId = 1;
            vsToCreate.LevelLearned = 1;
            var vsToReturn = new VocationSpellResponseDto();
            vsToReturn.SpellId = 1;
            vsToReturn.VocationId = 1;
            vsToReturn.LevelLearned = 1;
            var serviceResponse = new ServiceResponse<VocationSpellResponseDto>();
            serviceResponse.Data = vsToReturn;
            vocationSpellService.Setup(x => x.CreateVocationSpell(vsToCreate)).Returns(serviceResponse);

            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var intialResult = vocationSpellController.CreateVocationSpell(vsToCreate);
            var result = (intialResult.Result as CreatedAtActionResult).StatusCode;

            //Assert
            Assert.Equal(201, result);
        }

        [Fact]
        public void CreateVocationSpellReturnsNull()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            var vocationSpellDto = new VocationSpellResponseDto();
            var serviceResponse = new ServiceResponse<VocationSpellResponseDto>();
            serviceResponse.Success = false;
            vocationSpellService.Setup(x => x.CreateVocationSpell(vocationSpellDto)).Returns(serviceResponse);

            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var initialResult = vocationSpellController.CreateVocationSpell(vocationSpellDto);
            var result = (initialResult.Result as BadRequestObjectResult).StatusCode;

            //Assert
            Assert.Equal(400, result);
        }

        [Fact]
        public void CreateVocationSpellReturnsNoSpell()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            var vocationSpellDto = new VocationSpellResponseDto();
            vocationSpellDto.VocationId = 1;
            vocationSpellDto.LevelLearned = 2;
            var serviceResponse = new ServiceResponse<VocationSpellResponseDto>();
            serviceResponse.Success = false;

            vocationSpellService.Setup(x => x.CreateVocationSpell(vocationSpellDto)).Returns(serviceResponse);

            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var intialResult = vocationSpellController.CreateVocationSpell(vocationSpellDto);
            var result = (intialResult.Result as BadRequestObjectResult).StatusCode;

            //Assert
            Assert.Equal(400, result);
        }

        [Fact]
        public void CreateVocationSpellReturnsNoVocation()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            var vocationSpellDto = new VocationSpellResponseDto();
            vocationSpellDto.SpellId = 1;
            vocationSpellDto.LevelLearned = 2;
            var serviceResponse = new ServiceResponse<VocationSpellResponseDto>();
            serviceResponse.Success = false;

            vocationSpellService.Setup(x => x.CreateVocationSpell(vocationSpellDto)).Returns(serviceResponse);

            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var intialResult = vocationSpellController.CreateVocationSpell(vocationSpellDto);
            var result = (intialResult.Result as BadRequestObjectResult).StatusCode;

            //Assert
            Assert.Equal(400, result);
        }

        [Fact]
        public void DeleteVocationSpellSuccessObject()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            var vocationSpellDto = new VocationSpellResponseDto();
            vocationSpellDto.LevelLearned = 1;
            vocationSpellDto.SpellId = 1;
            vocationSpellDto.VocationId = 1;
            var serviceResponse = new ServiceResponse<VocationSpellResponseDto>();
            serviceResponse.Data = vocationSpellDto;
            vocationSpellService.Setup(x => x.DeleteVocationSpell(1, 1)).Returns(serviceResponse);

            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var initialResult = vocationSpellController.DeleteVocationSpell(1, 1);
            var result = (initialResult.Result as OkObjectResult).Value as ServiceResponse<VocationSpellResponseDto>;

            //Assert
            Assert.Equal(1, result.Data.LevelLearned);
            Assert.Equal(1, result.Data.SpellId);
            Assert.Equal(1, result.Data.VocationId);
        }

        [Fact]
        public void DeleteVocationSpellOkStatusCode()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            var vocationSpellDto = new VocationSpellResponseDto();
            vocationSpellDto.VocationId = 1;
            vocationSpellDto.LevelLearned = 1;
            vocationSpellDto.SpellId = 1;
            var serviceResponse = new ServiceResponse<VocationSpellResponseDto>();
            serviceResponse.Data = vocationSpellDto;
            vocationSpellService.Setup(x => x.DeleteVocationSpell(1, 1)).Returns(serviceResponse);
            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var initialResult = vocationSpellController.DeleteVocationSpell(1, 1);
            var result = (initialResult.Result as OkObjectResult).StatusCode;

            //Assert
            Assert.Equal(200, result);
        }

        [Fact]
        public void DeleteVocationSpellVocationNotFound()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            var serviceResponse = new ServiceResponse<VocationSpellResponseDto>();
            serviceResponse.Success = false;

            vocationSpellService.Setup(x => x.DeleteVocationSpell(1, 1)).Returns(serviceResponse);
            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var initialResult = vocationSpellController.DeleteVocationSpell(1, 1);
            var result = (initialResult.Result as NotFoundObjectResult).StatusCode;

            //Assert
            Assert.Equal(404, result);
        }

        [Fact]
        public void DeleteVocationSpellSpellsNotFound()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            var serviceResponse = new ServiceResponse<VocationSpellResponseDto>();
            serviceResponse.Success = false;

            vocationSpellService.Setup(x => x.DeleteVocationSpell(1, 1)).Returns(serviceResponse);
            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var initialResult = vocationSpellController.DeleteVocationSpell(1, 1);
            var result = (initialResult.Result as NotFoundObjectResult).StatusCode;

            //Assert
            Assert.Equal(404, result);
        }

        [Fact]
        public void DeleteVocationSpellNull()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            var serviceResponse = new ServiceResponse<VocationSpellResponseDto>();
            serviceResponse.Success = false;

            vocationSpellService.Setup(x => x.DeleteVocationSpell(1, 1)).Returns(serviceResponse);
            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var initialResult = vocationSpellController.DeleteVocationSpell(1, 1);
            var result = (initialResult.Result as NotFoundObjectResult).StatusCode;

            //Assert
            Assert.Equal(404, result);
        }

        [Fact]
        public void UpdateVocationSpellSuccessObject()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            var vocationSpellDto = new VocationSpellResponseDto();
            vocationSpellDto.SpellId = 2;
            vocationSpellDto.VocationId = 2;
            vocationSpellDto.LevelLearned = 2;
            var serviceResponse = new ServiceResponse<VocationSpellResponseDto>();
            serviceResponse.Data = vocationSpellDto;

            vocationSpellService.Setup(x => x.UpdateVocationSpell(1, 1, vocationSpellDto)).Returns(serviceResponse);
            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var initialResult = vocationSpellController.UpdateVocationSpell(1, 1, vocationSpellDto);
            var result = (initialResult.Result as OkObjectResult).Value as ServiceResponse<VocationSpellResponseDto>;

            //Assert
            Assert.Equal(2, result.Data.LevelLearned);
            Assert.Equal(2, result.Data.SpellId);
            Assert.Equal(2, result.Data.VocationId);
        }

        [Fact]
        public void UpdateVocationSpellSuccessStatusCode()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            var vocationSpellDto = new VocationSpellResponseDto();
            vocationSpellDto.SpellId = 2;
            vocationSpellDto.VocationId = 2;
            vocationSpellDto.LevelLearned = 2;
            var serviceResponse = new ServiceResponse<VocationSpellResponseDto>();
            serviceResponse.Data = vocationSpellDto;

            vocationSpellService.Setup(x => x.UpdateVocationSpell(1, 1, vocationSpellDto)).Returns(serviceResponse);
            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var initialResult = vocationSpellController.UpdateVocationSpell(1, 1, vocationSpellDto);
            var result = (initialResult.Result as OkObjectResult).StatusCode;

            //Assert
            Assert.Equal(200, result);
        }

        [Fact]
        public void UpdateVocationSpellVocationNotFound()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            var vocationSpellDto = new VocationSpellResponseDto();
            vocationSpellDto.LevelLearned = 2;
            vocationSpellDto.VocationId = 2;
            vocationSpellDto.SpellId = 2;
            var serviceResponse = new ServiceResponse<VocationSpellResponseDto>();
            serviceResponse.Success = false;

            vocationSpellService.Setup(x => x.UpdateVocationSpell(1, 1, vocationSpellDto)).Returns(serviceResponse);
            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var initialResult = vocationSpellController.UpdateVocationSpell(1, 1, vocationSpellDto);
            var result = (initialResult.Result as NotFoundObjectResult).StatusCode;

            //Assert
            Assert.Equal(404, result);
        }

        [Fact]
        public void UpdateVocationSpellNotFoundSpell()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            var vocationSpellDto = new VocationSpellResponseDto();
            vocationSpellDto.LevelLearned = 2;
            vocationSpellDto.VocationId = 2;
            vocationSpellDto.SpellId = 2;
            var serviceResponse = new ServiceResponse<VocationSpellResponseDto>();
            serviceResponse.Success = false;

            vocationSpellService.Setup(x => x.UpdateVocationSpell(1, 1, vocationSpellDto)).Returns(serviceResponse);
            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var initialResult = vocationSpellController.UpdateVocationSpell(1, 1, vocationSpellDto);
            var result = (initialResult.Result as NotFoundObjectResult).StatusCode;

            //Assert
            Assert.Equal(404, result);
        }

        [Fact]
        public void UpdateVocationSpellNull()
        {
            //Arrange
            var vocationSpellService = new Mock<IVocationSpellService>();
            var vocationSpellDto = new VocationSpellResponseDto();
            vocationSpellDto.LevelLearned = 2;
            vocationSpellDto.VocationId = 2;
            vocationSpellDto.SpellId = 2;
            var serviceResponse = new ServiceResponse<VocationSpellResponseDto>();
            serviceResponse.Success = false;

            vocationSpellService.Setup(x => x.UpdateVocationSpell(1, 1, vocationSpellDto)).Returns(serviceResponse);
            var vocationSpellController = new VocationSpellController(vocationSpellService.Object);

            //Act
            var initialResult = vocationSpellController.UpdateVocationSpell(1, 1, vocationSpellDto);
            var result = (initialResult.Result as NotFoundObjectResult).StatusCode;

            //Assert
            Assert.Equal(404, result);
        }
    }
}