using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StarrySkies.API.Controllers;
using StarrySkies.Services.DTOs.SpellDtos;
using StarrySkies.Services.Services.Spells;
using Xunit;

namespace StarrySkies.Tests.Controllers.Tests
{
    public class SpellsControllerTest
    {
        [Fact]
        public void GetAllSpellsTest()
        {
            //Arrange
            var spellService = new Mock<ISpellService>();
            SpellResponseDto spell = new SpellResponseDto();
            spell.Id = 1;
            spell.Name = "Zoom";
            SpellResponseDto spellTwo = new SpellResponseDto();
            spellTwo.Id = 2;
            spellTwo.Name = "Kafrizzle";
            List<SpellResponseDto> spellList = new List<SpellResponseDto>();
            spellList.Add(spell);
            spellList.Add(spellTwo);

            spellService.Setup(x => x.GetAllSpells()).Returns(spellList);
            var spellController = new SpellController(spellService.Object);

            //Act
            var initialResult = spellController.GetAllSpells();
            var result = (initialResult.Result as OkObjectResult).Value as List<SpellResponseDto>;

            //Assert
            Assert.Equal(1, result[0].Id);
            Assert.Equal(2, result[1].Id);
            Assert.Equal("Zoom", result[0].Name);
        }

        [Fact]
        public void GetAllSpellsReturnsEmptyList()
        {
            //Arrange
            var spellService = new Mock<ISpellService>();
            List<SpellResponseDto> spellList = new List<SpellResponseDto>();

            spellService.Setup(x => x.GetAllSpells()).Returns(spellList);
            var spellController = new SpellController(spellService.Object);

            //Act
            var initialResult = spellController.GetAllSpells();
            var result = (initialResult.Result as OkObjectResult).Value as List<SpellResponseDto>;

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetAllSpellsOkResponseCode()
        {
            //Arrange
            var spellService = new Mock<ISpellService>();
            List<SpellResponseDto> spellList = new List<SpellResponseDto>();

            spellService.Setup(x => x.GetAllSpells()).Returns(spellList);
            var spellController = new SpellController(spellService.Object);

            //Act
            var initialResult = spellController.GetAllSpells();
            var result = (initialResult.Result as OkObjectResult).StatusCode;

            //Assert
            Assert.Equal(200, result);
        }

        [Fact]
        public void GetSpellOkTest()
        {
            //Arrange
            var spellService = new Mock<ISpellService>();
            SpellResponseDto spell = new SpellResponseDto();
            spell.Id = 1;
            spell.Name = "Zoom";

            spellService.Setup(x => x.GetSpell(1)).Returns(spell);
            var spellController = new SpellController(spellService.Object);

            //Act
            var initialResult = spellController.GetSpell(1);
            var result = (initialResult.Result as OkObjectResult).Value as SpellResponseDto;

            //Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Zoom", result.Name);
        }

        [Fact]
        public void GetSpell200StatusCodeTest()
        {
            //Arrange
            var spellService = new Mock<ISpellService>();
            SpellResponseDto spell = new SpellResponseDto();
            spell.Id = 1;
            spell.Name = "Zoom";

            spellService.Setup(x => x.GetSpell(1)).Returns(spell);
            var spellController = new SpellController(spellService.Object);

            //Act
            var initialResult = spellController.GetSpell(1);
            var result = (initialResult.Result as OkObjectResult).StatusCode;

            //Assert
            Assert.Equal(200, result);
        }

        [Fact]
        public void GetSpellNullTest()
        {
            //Arrange
            var spellService = new Mock<ISpellService>();

            spellService.Setup(x => x.GetSpell(1));
            var spellController = new SpellController(spellService.Object);

            //Act
            var initialResult = spellController.GetSpell(1);
            var result = (initialResult.Result as NotFoundResult).StatusCode;

            //Assert
            Assert.Equal(404, result);
        }

        [Fact]
        public void GetSpellResponseEmptyNotFoundTest()
        {
            //Arrange
            var spellService = new Mock<ISpellService>();
            SpellResponseDto spell = new SpellResponseDto();

            spellService.Setup(x => x.GetSpell(1)).Returns(spell);
            var spellController = new SpellController(spellService.Object);

            //Act
            var initialResult = spellController.GetSpell(1);
            var result = (initialResult.Result as NotFoundResult).StatusCode;

            //Assert
            Assert.Equal(404, result);
        }

        [Fact]
        public void CreateNewSpellControllerTest()
        {
            //Arrange
            var spellService = new Mock<ISpellService>();
            SpellResponseDto createdSpell = new SpellResponseDto();
            createdSpell.Id = 1;
            createdSpell.Name = "Zoom";
            CreateSpellDto createSpell = new CreateSpellDto();
            createSpell.Name = "Zoom";

            spellService.Setup(x => x.CreateSpell(createSpell)).Returns(createdSpell);
            var spellController = new SpellController(spellService.Object);

            //Act
            var initialResult = spellController.CreateSpell(createSpell);
            var result = (initialResult.Result as CreatedAtActionResult).Value as SpellResponseDto;

            //Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Zoom", result.Name);
        }

        [Fact]
        public void CreateNewSpellControllerStatusCodeTest()
        {
            //Arrange
            var spellService = new Mock<ISpellService>();
            SpellResponseDto createdSpell = new SpellResponseDto();
            createdSpell.Id = 1;
            createdSpell.Name = "Zoom";
            CreateSpellDto createSpell = new CreateSpellDto();
            createSpell.Name = "Zoom";

            spellService.Setup(x => x.CreateSpell(createSpell)).Returns(createdSpell);
            var spellController = new SpellController(spellService.Object);

            //Act
            var initialResult = spellController.CreateSpell(createSpell);
            var result = (initialResult.Result as CreatedAtActionResult).StatusCode;

            //Assert
            Assert.Equal(201, result);
        }

        [Fact]
        public void CreateSpellNullNameTest()
        {
            //Arrange
            var spellService = new Mock<ISpellService>();
            CreateSpellDto createSpell = new CreateSpellDto();

            spellService.Setup(x => x.CreateSpell(createSpell));
            var spellController = new SpellController(spellService.Object);

            //Act
            var initialResult = spellController.CreateSpell(createSpell);
            var result = (initialResult.Result as BadRequestObjectResult).StatusCode;

            //Arrange
            Assert.Equal(400, result);
        }

        [Fact]
        public void CreateSpellEmptyNameTest()
        {
            //Arrange
            var spellService = new Mock<ISpellService>();
            CreateSpellDto createSpell = new CreateSpellDto();
            createSpell.Name = "       ";

            spellService.Setup(x => x.CreateSpell(createSpell));
            var spellController = new SpellController(spellService.Object);

            //Act
            var initialResult = spellController.CreateSpell(createSpell);
            var result = (initialResult.Result as BadRequestObjectResult).StatusCode;

            //Arrange
            Assert.Equal(400, result);
        }

        [Fact]
        public void DeleteSpellTestSuccess()
        {
            //Arrange
            var spellService = new Mock<ISpellService>();
            SpellResponseDto spellToDelete = new SpellResponseDto();
            spellToDelete.Id = 1;
            spellToDelete.Name = "Frizz";

            spellService.Setup(x => x.DeleteSpell(1)).Returns(spellToDelete);
            var spellController = new SpellController(spellService.Object);

            //Act
            var intialResult = spellController.DeleteSpell(1);
            var result = (intialResult.Result as OkObjectResult).Value as SpellResponseDto;

            //Arrange
            Assert.Equal(1, result.Id);
            Assert.Equal("Frizz", result.Name);
        }

        
        [Fact]
        public void DeleteSpellTestSuccessStatusCode()
        {
            //Arrange
            var spellService = new Mock<ISpellService>();
            SpellResponseDto spellToDelete = new SpellResponseDto();
            spellToDelete.Id = 1;
            spellToDelete.Name = "Frizz";

            spellService.Setup(x => x.DeleteSpell(1)).Returns(spellToDelete);
            var spellController = new SpellController(spellService.Object);

            //Act
            var intialResult = spellController.DeleteSpell(1);
            var result = (intialResult.Result as OkObjectResult).StatusCode;

            //Arrange
            Assert.Equal(200, result);
        }

        [Fact]
        public void DeleteRecordNotFound()
        {
            //Arrange
            var spellService = new Mock<ISpellService>();
            SpellResponseDto spell = new SpellResponseDto();
            spellService.Setup(x => x.GetSpell(1));
            var spellController = new SpellController(spellService.Object);

            //Act
            var initialResult = spellController.DeleteSpell(1);
            var result = (initialResult.Result as NotFoundResult).StatusCode;

            //Assert
            Assert.Equal(404, result);
        }

        [Fact]
        public void SpellControllerDeleteIDIsZeroTest()
        {
            //Arrange
            var spellService = new Mock<ISpellService>();
            SpellResponseDto deletedSpell = new SpellResponseDto();
            deletedSpell.Id = 0;

            spellService.Setup(x => x.DeleteSpell(1)).Returns(deletedSpell);
            var spellController = new SpellController(spellService.Object);

            //Act
            var initialResult = spellController.DeleteSpell(1);
            var result = (initialResult.Result as NotFoundResult).StatusCode;

            //Assert
            Assert.Equal(404, result);
        }

        [Fact]
        public void UpdatedSpellSuccess()
        {
            //Arrange
            var spellService = new Mock<ISpellService>();
            CreateSpellDto updatedSpell = new CreateSpellDto();
            updatedSpell.Name = "Zoom";
            SpellResponseDto spellToReturn = new SpellResponseDto();
            spellToReturn.Name = "Zoom";
            spellToReturn.Id = 1;

            spellService.Setup(x => x.UpdateSpell(1, updatedSpell)).Returns(spellToReturn);
            var spellController = new SpellController(spellService.Object);

            //Act
            var initialResult = spellController.UpdateSpell(1, updatedSpell);
            var result = (initialResult.Result as OkObjectResult).Value as SpellResponseDto;

            //Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Zoom", result.Name);
        }

        [Fact]
        public void UpdatedSpellSuccessStatusCode()
        {
            //Arrange
            var spellService = new Mock<ISpellService>();
            CreateSpellDto updatedSpell = new CreateSpellDto();
            updatedSpell.Name = "Zoom";
            SpellResponseDto spellToReturn = new SpellResponseDto();
            spellToReturn.Name = "Zoom";
            spellToReturn.Id = 1;

            spellService.Setup(x => x.UpdateSpell(1, updatedSpell)).Returns(spellToReturn);
            var spellController = new SpellController(spellService.Object);

            //Act
            var initialResult = spellController.UpdateSpell(1, updatedSpell);
            var result = (initialResult.Result as OkObjectResult).StatusCode;

            //Assert
            Assert.Equal(200, result);
        }

        [Fact]
        public void UpdatedSpellNameEmptyTest()
        {
            //Arrange
            var spellService = new Mock<ISpellService>();
            CreateSpellDto updatedSpell = new CreateSpellDto();
            updatedSpell.Name = "";

            var spellController = new SpellController(spellService.Object);

            //Act
            var initialResult = spellController.UpdateSpell(1, updatedSpell);
            var result = (initialResult.Result as BadRequestObjectResult).StatusCode;

            //Assert
            Assert.Equal(400, result);
        }

        [Fact]
        public void UpdatedSpellNameNullTest()
        {
            //Arrange
            var spellService = new Mock<ISpellService>();
            CreateSpellDto updatedSpell = new CreateSpellDto();

            var spellController = new SpellController(spellService.Object);

            //Act
            var initialResult = spellController.UpdateSpell(1, updatedSpell);
            var result = (initialResult.Result as BadRequestObjectResult).StatusCode;

            //Assert
            Assert.Equal(400, result);
        }

        [Fact]
        public void UpdatedSpellNotFoundTest()
        {
            //Arrange
            var spellService = new Mock<ISpellService>();
            CreateSpellDto updatedSpell = new CreateSpellDto();
            updatedSpell.Name = "Zoom";

            spellService.Setup(x => x.UpdateSpell(1, updatedSpell));
            var spellController = new SpellController(spellService.Object);

            //Act
            var initialResult = spellController.UpdateSpell(1, updatedSpell);
            var result = (initialResult.Result as NotFoundResult).StatusCode;

            //Assert
            Assert.Equal(404, result);
        }

        [Fact]
        public void UpdatedSpellNotFoundIdZeroTest()
        {
            //Arrange
            var spellService = new Mock<ISpellService>();
            CreateSpellDto updatedSpell = new CreateSpellDto();
            updatedSpell.Name = "Zoom";
            SpellResponseDto spellToReturn = new SpellResponseDto();
            spellToReturn.Id = 0;

            spellService.Setup(x => x.UpdateSpell(1, updatedSpell)).Returns(spellToReturn);
            var spellController = new SpellController(spellService.Object);

            //Act
            var initialResult = spellController.UpdateSpell(1, updatedSpell);
            var result = (initialResult.Result as NotFoundResult).StatusCode;

            //Assert
            Assert.Equal(404, result);
        }
    }
}