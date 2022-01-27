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
            Assert.Equal(1, result.VocationId);
            Assert.Equal(1, result.SpellId);
            Assert.Equal(1, result.LevelLearned);
        }

        [Fact]
        public void GetVocationSpellNotFoundResult()
        {
            //Arrange
            var vocationSpellRepo = new Mock<IVocationSpellRepo>();
            var spellRepo = new Mock<ISpellRepo>();
            var vocationRepo = new Mock<IVocationRepo>();

            VocationSpell vocationSpell = new VocationSpell();

            vocationSpellRepo.Setup(x => x.GetVocationSpell(1, 1)).Returns(vocationSpell);
            var vocationService = new VocationSpellService(vocationSpellRepo.Object, vocationRepo.Object, spellRepo.Object, _mapper);

            //Act
            var result = vocationService.GetVocationSpell(1, 1);

            //Assert
            Assert.Equal(0, result.SpellId);
            Assert.Equal(0, result.VocationId);
            Assert.Equal(0, result.LevelLearned);
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
            Assert.Equal(0, result.LevelLearned);
            Assert.Equal(0, result.SpellId);
            Assert.Equal(0, result.VocationId);
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
            Assert.Equal(2, result.Count);
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
            Assert.Empty(result);
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
            Assert.Equal(1, result.VocationId);
            Assert.Equal(1, result.SpellId);
            Assert.Equal(1, result.LevelLearned);
        }

        [Fact]
        public void CreateVocationSpellExistsNoSuccess()
        {
            //Arrange
            var vocationSpellRepo = new Mock<IVocationSpellRepo>();
            var vocationRepo = new Mock<IVocationRepo>();
            var spellRepo = new Mock<ISpellRepo>();
            var vocationSpell = CreateTestVocationSpell(1, "Test");
            var spell = CreateTestSpell(1, "Test");
            var vocation = CreateTestVocation(1, "Test");
            var vocationSpellDtoCreate = CreateTestVocationSpellResponseDto(1);

            vocationSpellRepo.Setup(x => x.GetVocationSpell(1, 1)).Returns(vocationSpell);
            vocationRepo.Setup(x => x.GetVocationById(1)).Returns(vocation);
            spellRepo.Setup(x => x.GetSpell(1)).Returns(spell);

            var vocationService = new VocationSpellService(vocationSpellRepo.Object, vocationRepo.Object, spellRepo.Object, _mapper);

            //Act
            var result = vocationService.CreateVocationSpell(vocationSpellDtoCreate);

            //Assert
            Assert.Equal(0, result.VocationId);
            Assert.Equal(0, result.SpellId);
            Assert.Equal(0, result.LevelLearned);
            vocationSpellRepo.Verify(x => x.CreateVocationSpell(It.IsAny<VocationSpell>()), Times.Never);
        }

        [Fact]
        public void CreateVocationSpellWhenSpellDoesntExist()
        {
            //Arrange
            var vocationSpellRepo = new Mock<IVocationSpellRepo>();
            var vocationRepo = new Mock<IVocationRepo>();
            var spellRepo = new Mock<ISpellRepo>();
            var vocation = CreateTestVocation(1, "Test");
            var vocationSpell = CreateTestVocationSpell(1, "Test");
            var vocationSpellResponseDto = CreateTestVocationSpellResponseDto(1);

            vocationSpellRepo.Setup(x => x.GetVocationSpell(1, 1));
            vocationRepo.Setup(x => x.GetVocationById(1)).Returns(vocation);
            spellRepo.Setup(x => x.GetSpell(1));

            var vocationSpellService = new VocationSpellService(vocationSpellRepo.Object, vocationRepo.Object, spellRepo.Object, _mapper);

            //Act
            var result = vocationSpellService.CreateVocationSpell(vocationSpellResponseDto);

            //Assert
            Assert.Equal(0, result.LevelLearned);
            Assert.Equal(0, result.SpellId);
            Assert.Equal(0, result.VocationId);
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
            Assert.Equal(0, result.LevelLearned);
            Assert.Equal(0, result.SpellId);
            Assert.Equal(0, result.VocationId);
            vocationSpellRepo.Verify(x => x.CreateVocationSpell(It.IsAny<VocationSpell>()), Times.Never);
        }
    }
}