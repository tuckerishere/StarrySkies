using System;
using System.Collections.Generic;
using AutoMapper;
using Moq;
using StarrySkies.Data.Models;
using StarrySkies.Data.Repositories.SpellRepo;
using StarrySkies.Data.Repositories.VocationRepo;
using StarrySkies.Data.Repositories.VocationSpellRepo;
using StarrySkies.Services.DTOs.VocationSpellDtos;
using StarrySkies.Services.Mapping;
using StarrySkies.Services.Services.VocationSpells;
using Xunit;

namespace StarrySkies.Tests.Service.Tests
{
    public class VocationSpellServiceTest
    {

        private readonly IMapper _mapper;

        public VocationSpellServiceTest()
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
        private VocationSpell CreateTestVocationSpell(int id, string testName)
        {
            Vocation vocation = new Vocation();
            vocation.Id = id;
            vocation.Name = testName;
            Spell spell = new Spell();
            spell.Id = id;
            spell.Name = testName;
            VocationSpell vocationSpell = new VocationSpell();
            vocationSpell.Id = id;
            vocationSpell.SpellId = id;
            vocationSpell.VocationId = id;
            vocationSpell.Spell = spell;
            vocationSpell.Vocation = vocation;
            vocationSpell.LevelLearned = id;

            return vocationSpell;
        }
        private VocationSpellResponseDto CreateTestVocationSpellResponseDto(int id)
        {
            var vocationSpellDto = new VocationSpellResponseDto();
            vocationSpellDto.SpellId = id;
            vocationSpellDto.VocationId = id;
            vocationSpellDto.LevelLearned = id;

            return vocationSpellDto;
        }

        private Spell CreateTestSpell(int id, string name)
        {
            Spell spell = new Spell();
            spell.Id = id;
            spell.Name = name;

            return spell;
        }

        private Vocation CreateTestVocation(int id, string name)
        {
            Vocation vocation = new Vocation();
            vocation.Id = id;
            vocation.Name = name;

            return vocation;
        }

        [Fact]
        public void GetVocationSpellSuccessTest()
        {
            //Arrange
            var vocationSpellRepo = new Mock<IVocationSpellRepo>();
            var vocationRepo = new Mock<IVocationRepo>();
            var spellRepo = new Mock<ISpellRepo>();

            VocationSpell vocationSpell = CreateTestVocationSpell(1, "TestName");

            vocationSpellRepo.Setup(x => x.GetVocationSpell(1, 1)).Returns(vocationSpell);
            var vocationService = new VocationSpellService(vocationSpellRepo.Object, vocationRepo.Object, spellRepo.Object, _mapper);

            //Act
            var result = vocationService.GetVocationSpell(1, 1);

            //Assert
            Assert.Equal(1, result.Data.VocationId);
            Assert.Equal(1, result.Data.SpellId);
            Assert.Equal(1, result.Data.LevelLearned);
        }

        [Fact]
        public void GetVocationSpellNotFoundResult()
        {
            //Arrange
            var vocationSpellRepo = new Mock<IVocationSpellRepo>();
            var spellRepo = new Mock<ISpellRepo>();
            var vocationRepo = new Mock<IVocationRepo>();

            VocationSpell vocationSpell = new VocationSpell();

            vocationSpellRepo.Setup(x => x.GetVocationSpell(1, 1));
            var vocationService = new VocationSpellService(vocationSpellRepo.Object, vocationRepo.Object, spellRepo.Object, _mapper);

            //Act
            var result = vocationService.GetVocationSpell(1, 1);

            //Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public void GetVocationSpellNullTest()
        {
            //Arrange
            var vocationSpellRepo = new Mock<IVocationSpellRepo>();
            var spellRepo = new Mock<ISpellRepo>();
            var vocationRepo = new Mock<IVocationRepo>();

            vocationSpellRepo.Setup(x => x.GetVocationSpell(1, 1));
            var vocationService = new VocationSpellService(vocationSpellRepo.Object, vocationRepo.Object, spellRepo.Object, _mapper);

            //Act
            var result = vocationService.GetVocationSpell(1, 1);

            //Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public void GetVocationSpellsAllSuccess()
        {
            //Arrange
            var vocationSpellRepo = new Mock<IVocationSpellRepo>();
            var vocationRepo = new Mock<IVocationRepo>();
            var spellRepo = new Mock<ISpellRepo>();

            VocationSpell vocationSpellOne = CreateTestVocationSpell(1, "Test Name");
            VocationSpell vocationSpellTwo = CreateTestVocationSpell(2, "Second Name");
            List<VocationSpell> vocationSpells = new List<VocationSpell>();
            vocationSpells.Add(vocationSpellOne);
            vocationSpells.Add(vocationSpellTwo);

            vocationSpellRepo.Setup(x => x.GetVocationSpells()).Returns(vocationSpells);
            var vocationService = new VocationSpellService(vocationSpellRepo.Object, vocationRepo.Object, spellRepo.Object, _mapper);

            //Act
            var result = vocationService.GetVocationSpells();

            //Assert
            Assert.Equal(2, result.Data.Count);
        }

        [Fact]
        public void GetVocationSpellsEmptyList()
        {
            //Arrange
            var vocationSpellRepo = new Mock<IVocationSpellRepo>();
            var spellRepo = new Mock<ISpellRepo>();
            var vocationRepo = new Mock<IVocationRepo>();

            vocationSpellRepo.Setup(x => x.GetVocationSpells());
            var vocationSpellService = new VocationSpellService(vocationSpellRepo.Object, vocationRepo.Object, spellRepo.Object, _mapper);

            //Act
            var result = vocationSpellService.GetVocationSpells();

            //Assert
            Assert.Empty(result.Data);
        }

        [Fact]
        public void CreateVocationSpellSuccess()
        {
            //Arrange
            var vocationSpellRepo = new Mock<IVocationSpellRepo>();
            var vocationRepo = new Mock<IVocationRepo>();
            var spellRepo = new Mock<ISpellRepo>();
            var vocationSpell = CreateTestVocationSpell(1, "Test Name");
            var vocation = CreateTestVocation(1, "Vocation");
            var spell = CreateTestSpell(1, "Spell");
            var vocationSpellDtoCreate = CreateTestVocationSpellResponseDto(1);

            vocationSpellRepo.Setup(x => x.CreateVocationSpell(vocationSpell));
            vocationSpellRepo.Setup(x => x.GetVocationSpell(1, 1));
            vocationRepo.Setup(x => x.GetVocationById(1)).Returns(vocation);
            spellRepo.Setup(x => x.GetSpell(1)).Returns(spell);

            var vocationSpellService = new VocationSpellService(vocationSpellRepo.Object, vocationRepo.Object, spellRepo.Object, _mapper);

            //Act
            var result = vocationSpellService.CreateVocationSpell(vocationSpellDtoCreate);

            //Assert
            Assert.Equal(1, result.Data.VocationId);
            Assert.Equal(1, result.Data.SpellId);
            Assert.Equal(1, result.Data.LevelLearned);
        }

        [Fact]
        public void CreateVocationSpellExistsNoSuccess()
        {
            //Arrange
            var vocationSpellRepo = new Mock<IVocationSpellRepo>();
            var vocationRepo = new Mock<IVocationRepo>();
            var spellRepo = new Mock<ISpellRepo>();
            var vocationSpell = CreateTestVocationSpell(1, "Test");
            var vocation = CreateTestVocation(1, "Test");
            var vocationSpellDtoCreate = CreateTestVocationSpellResponseDto(1);

            vocationSpellRepo.Setup(x => x.GetVocationSpell(1, 1)).Returns(vocationSpell);
            vocationRepo.Setup(x => x.GetVocationById(1)).Returns(vocation);
            spellRepo.Setup(x => x.GetSpell(1));

            var vocationService = new VocationSpellService(vocationSpellRepo.Object, vocationRepo.Object, spellRepo.Object, _mapper);

            //Act
            var result = vocationService.CreateVocationSpell(vocationSpellDtoCreate);

            //Assert
            Assert.Null(result.Data);
            vocationSpellRepo.Verify(x => x.CreateVocationSpell(It.IsAny<VocationSpell>()), Times.Never);
        }

        [Fact]
        public void CreateVocationSpellWhenSpellDoesntExist()
        {
            //Arrange
            var vocationSpellRepo = new Mock<IVocationSpellRepo>();
            var vocationRepo = new Mock<IVocationRepo>();
            var spellRepo = new Mock<ISpellRepo>();
            var vocationSpell = CreateTestVocationSpell(1, "Test");
            var vocationSpellResponseDto = CreateTestVocationSpellResponseDto(1);

            vocationSpellRepo.Setup(x => x.GetVocationSpell(1, 1));
            vocationRepo.Setup(x => x.GetVocationById(1));
            spellRepo.Setup(x => x.GetSpell(1));

            var vocationSpellService = new VocationSpellService(vocationSpellRepo.Object, vocationRepo.Object, spellRepo.Object, _mapper);

            //Act
            var result = vocationSpellService.CreateVocationSpell(vocationSpellResponseDto);

            //Assert
            Assert.Null(result.Data);
            vocationSpellRepo.Verify(x => x.CreateVocationSpell(It.IsAny<VocationSpell>()), Times.Never);
        }

        [Fact]
        public void CreateVocationSpellWhenVocationDoesntExist()
        {
            //Arrange
            var vocationSpellRepo = new Mock<IVocationSpellRepo>();
            var vocationRepo = new Mock<IVocationRepo>();
            var spellRepo = new Mock<ISpellRepo>();
            var spell = CreateTestSpell(1, "Test");
            var vocationSpell = CreateTestVocationSpell(1, "Test");
            var createVocationSpellDto = CreateTestVocationSpellResponseDto(1);

            vocationSpellRepo.Setup(x => x.GetVocationSpell(1, 1));
            vocationRepo.Setup(x => x.GetVocationById(1));
            spellRepo.Setup(x => x.GetSpell(1)).Returns(spell);

            var vocationSpellService = new VocationSpellService(vocationSpellRepo.Object, vocationRepo.Object, spellRepo.Object, _mapper);

            //Act
            var result = vocationSpellService.CreateVocationSpell(createVocationSpellDto);

            //Assert
            Assert.Null(result.Data);
            vocationSpellRepo.Verify(x => x.CreateVocationSpell(It.IsAny<VocationSpell>()), Times.Never);
        }

        [Fact]
        public void UpdateVocationSpellSuccess()
        {
            //Arrange
            var vocationSpellRepo = new Mock<IVocationSpellRepo>();
            var spellRepo = new Mock<ISpellRepo>();
            var vocationRepo = new Mock<IVocationRepo>();
            var spell = CreateTestSpell(2, "Test");
            var vocation = CreateTestVocation(1, "Vocation");
            var vocationSpell = CreateTestVocationSpell(1, "VocationSpell");
            var updatedVocationSpell = CreateTestVocationSpell(2, "Updated");
            var updatedVocationSpellDto = CreateTestVocationSpellResponseDto(2);

            vocationSpellRepo.Setup(x => x.GetVocationSpell(1, 1)).Returns(vocationSpell);
            vocationRepo.Setup(x => x.GetVocationById(2)).Returns(vocation);
            spellRepo.Setup(x => x.GetSpell(2)).Returns(spell);
            vocationSpellRepo.Setup(x => x.UpdateVocationSpell(updatedVocationSpell));
            vocationSpellRepo.Setup(x => x.SaveChanges());

            var vocationSpellService = new VocationSpellService(vocationSpellRepo.Object, vocationRepo.Object, spellRepo.Object, _mapper);

            //Act
            var result = vocationSpellService.UpdateVocationSpell(1, 1, updatedVocationSpellDto);

            //Assert
            Assert.Equal(2, result.Data.VocationId);
            Assert.Equal(2, result.Data.SpellId);
            Assert.Equal(2, result.Data.LevelLearned);
            vocationSpellRepo.Verify(x => x.UpdateVocationSpell(It.IsAny<VocationSpell>()), Times.Once);
            vocationSpellRepo.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public void UpdateVocationSpellExistsNotSuccessful()
        {
            //Arrange
            var vocationSpellRepo = new Mock<IVocationSpellRepo>();
            var spellRepo = new Mock<ISpellRepo>();
            var vocationRepo = new Mock<IVocationRepo>();
            var spell = CreateTestSpell(2, "Test");
            var vocation = CreateTestVocation(1, "Vocation");
            var vocationSpell = CreateTestVocationSpell(1, "VocationSpell");
            var updatedVocationSpell = CreateTestVocationSpell(2, "Updated");
            var updatedVocationSpellDto = CreateTestVocationSpellResponseDto(2);
            var vocationSpellTwo = CreateTestVocationSpell(2, "Twice");

            vocationSpellRepo.Setup(x => x.GetVocationSpell(1, 1)).Returns(vocationSpell);
            vocationSpellRepo.Setup(x => x.GetVocationSpell(2, 2)).Returns(vocationSpellTwo);
            vocationRepo.Setup(x => x.GetVocationById(2)).Returns(vocation);
            spellRepo.Setup(x => x.GetSpell(2)).Returns(spell);

            var vocationSpellService = new VocationSpellService(vocationSpellRepo.Object, vocationRepo.Object, spellRepo.Object, _mapper);

            //Act
            var result = vocationSpellService.UpdateVocationSpell(1, 1, updatedVocationSpellDto);

            //Assert
            Assert.Null(result.Data);
            Assert.False(result.Success);
            vocationSpellRepo.Verify(x => x.CreateVocationSpell(It.IsAny<VocationSpell>()), Times.Never);
            vocationSpellRepo.Verify(x => x.SaveChanges(), Times.Never);
        }

        [Fact]
        public void CreateVocationDoesNotExist()
        {
            //Arrange
            var vocationSpellRepo = new Mock<IVocationSpellRepo>();
            var vocationRepo = new Mock<IVocationRepo>();
            var spellRepo = new Mock<ISpellRepo>();
            var spell = CreateTestSpell(1, "Spell");
            var vocationSpell = CreateTestVocationSpell(1, "VocationSpell");
            var updatedVocationSpellDto = CreateTestVocationSpellResponseDto(1);

            vocationSpellRepo.Setup(x => x.GetVocationSpell(1, 1)).Returns(vocationSpell);
            spellRepo.Setup(x => x.GetSpell(1)).Returns(spell);
            var vocationSpellService = new VocationSpellService(vocationSpellRepo.Object, vocationRepo.Object, spellRepo.Object, _mapper);

            //Act
            var result = vocationSpellService.UpdateVocationSpell(1, 1, updatedVocationSpellDto);

            //Assert
            Assert.Null(result.Data);
            vocationSpellRepo.Verify(x => x.CreateVocationSpell(It.IsAny<VocationSpell>()), Times.Never);
            vocationSpellRepo.Verify(x => x.SaveChanges(), Times.Never);
        }

        [Fact]
        public void UpdateVocationSpellExistsNoSuccess()
        {
            //Arrange
            var vocationSpellRepo = new Mock<IVocationSpellRepo>();
            var spellRepo = new Mock<ISpellRepo>();
            var vocationRepo = new Mock<IVocationRepo>();
            var vocationSpell = CreateTestVocationSpell(1, "VocationSpell");
            var vocationSpellExists = CreateTestVocationSpell(2, "Exists");
            var spell = CreateTestSpell(2, "Exists");
            var vocation = CreateTestVocation(2, "VocationExists");
            var updatedVocation = CreateTestVocationSpellResponseDto(2);

            vocationSpellRepo.Setup(x => x.GetVocationSpell(1, 1)).Returns(vocationSpell);
            vocationSpellRepo.Setup(x => x.GetVocationSpell(2, 2)).Returns(vocationSpellExists);
            spellRepo.Setup(x => x.GetSpell(2)).Returns(spell);
            vocationRepo.Setup(x => x.GetVocationById(2)).Returns(vocation);

            var vocationSpellService = new VocationSpellService(vocationSpellRepo.Object, vocationRepo.Object, spellRepo.Object, _mapper);

            //Act
            var result = vocationSpellService.UpdateVocationSpell(1, 1, updatedVocation);

            //Assert
            Assert.Null(result.Data);
            vocationSpellRepo.Verify(x => x.UpdateVocationSpell(It.IsAny<VocationSpell>()), Times.Never);
            vocationSpellRepo.Verify(x => x.SaveChanges(), Times.Never);
        }

        [Fact]
        public void DeleteVocationSpellSuccess()
        {
            //Arrange
            var vocationSpellRepo = new Mock<IVocationSpellRepo>();
            var vocationRepo = new Mock<IVocationRepo>();
            var spellRepo = new Mock<ISpellRepo>();
            var vocationSpell = CreateTestVocationSpell(1, "VocationSpell");
            var vocationSpellToDelete = CreateTestVocationSpellResponseDto(1);

            vocationSpellRepo.Setup(x => x.GetVocationSpell(1, 1)).Returns(vocationSpell);
            vocationSpellRepo.Setup(x => x.DeleteVocationSpell(vocationSpell));
            vocationSpellRepo.Setup(x => x.SaveChanges()).Returns(true);

            var vocationSpellService = new VocationSpellService(vocationSpellRepo.Object, vocationRepo.Object, spellRepo.Object, _mapper);

            //Act
            var result = vocationSpellService.DeleteVocationSpell(1, 1);

            //Assert
            Assert.Equal(1, result.Data.SpellId);
            Assert.Equal(1, result.Data.LevelLearned);
            Assert.Equal(1, result.Data.VocationId);
            vocationSpellRepo.Verify(x => x.DeleteVocationSpell(It.IsAny<VocationSpell>()), Times.Once);
            vocationSpellRepo.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public void DeleteVocationUnsuccessfulDoesntExist()
        {
            //Arrange
            var vocationSpellRepo = new Mock<IVocationSpellRepo>();
            var vocationRepo = new Mock<IVocationRepo>();
            var spellRepo = new Mock<ISpellRepo>();
            var vocationToDelete = CreateTestVocationSpellResponseDto(1);

            vocationSpellRepo.Setup(x => x.GetVocationSpell(1, 1));

            var vocationSpellService = new VocationSpellService(vocationSpellRepo.Object, vocationRepo.Object, spellRepo.Object, _mapper);

            //Act
            var result = vocationSpellService.DeleteVocationSpell(1, 1);

            //Assert
            Assert.Null(result.Data);
            vocationSpellRepo.Verify(x => x.DeleteVocationSpell(It.IsAny<VocationSpell>()), Times.Never);
            vocationSpellRepo.Verify(x => x.SaveChanges(), Times.Never);
        }
    }
}