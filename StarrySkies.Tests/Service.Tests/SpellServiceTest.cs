using AutoMapper;
using Moq;
using StarrySkies.Data.Models;
using StarrySkies.Data.Repositories.SpellRepo;
using StarrySkies.Services.DTOs.SpellDtos;
using StarrySkies.Services.Mapping;
using StarrySkies.Services.Services.Spells;
using Xunit;

namespace StarrySkies.Tests.Service.Tests
{
    public class SpellServiceTest
    {
        private readonly IMapper _mapper;

        public SpellServiceTest()
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
        public void CreateSpellSuccessTest()
        {
            //Arrange
            var spellRepo = new Mock<ISpellRepo>();
            Spell spell = new Spell();
            spell.Id = 1;
            spell.Name = "Zoom";
            spell.Description = "Test";
            spell.MpCost = 2;
            spell.SpellTarget = "Ally";
            CreateSpellDto spellDto = new CreateSpellDto();
            spellDto.Name = "Zoom";
            spellDto.Description = "Test";
            spellDto.MPCost = 2;
            spellDto.SpellTarget = "Ally";

            spellRepo.Setup(x => x.CreateSpell(spell));
            spellRepo.Setup(x => x.SaveChanges());
            var spellService = new SpellService(spellRepo.Object, _mapper);

            //Act
            var result = spellService.CreateSpell(spellDto);

            //Assert
            Assert.Equal("Zoom", result.Name);
            Assert.Equal("Test", result.Description);
            Assert.Equal(2, result.MPCost);
            Assert.Equal("Ally", result.SpellTarget);
            spellRepo.Verify(x => x.CreateSpell(It.IsAny<Spell>()), Times.Once);
            spellRepo.Verify(x => x.SaveChanges(), Times.Once);
        } 
        [Fact]
         public void CreateSpellSuccessMpInRangeTest()
        {
            //Arrange
            var spellRepo = new Mock<ISpellRepo>();
            Spell spell = new Spell();
            spell.Id = 1;
            spell.Name = "Zoom";
            spell.Description = "Test";
            spell.SpellTarget = "Ally";
            CreateSpellDto spellDto = new CreateSpellDto();
            spellDto.Name = "Zoom";
            spellDto.Description = "Test";
            spellDto.SpellTarget = "Ally";

            spellRepo.Setup(x => x.CreateSpell(spell));
            spellRepo.Setup(x => x.SaveChanges());
            var spellService = new SpellService(spellRepo.Object, _mapper);

            //Act
            var result = spellService.CreateSpell(spellDto);

            //Assert
            Assert.Equal("Zoom", result.Name);
            Assert.Equal("Test", result.Description);
            Assert.Equal(0, result.MPCost);
            Assert.Equal("Ally", result.SpellTarget);
            spellRepo.Verify(x => x.CreateSpell(It.IsAny<Spell>()), Times.Once);
            spellRepo.Verify(x => x.SaveChanges(), Times.Once);
        } 
                [Fact]
         public void CreateSpellSuccessMpOutOfRangeTest()
        {
            //Arrange
            var spellRepo = new Mock<ISpellRepo>();
            Spell spell = new Spell();
            spell.Id = 1;
            spell.Name = "Zoom";
            spell.Description = "Test";
            spell.MpCost = 100;
            spell.SpellTarget = "Ally";
            CreateSpellDto spellDto = new CreateSpellDto();
            spellDto.Name = "Zoom";
            spellDto.MPCost = 100;
            spellDto.Description = "Test";
            spellDto.SpellTarget = "Ally";

            var spellService = new SpellService(spellRepo.Object, _mapper);

            //Act
            var result = spellService.CreateSpell(spellDto);

            //Assert
            Assert.Null(result.Name);
        } 
        [Fact]
        public void CreateSpellBlankNameTest()
        {
            //Arrange
            var spellRepo = new Mock<ISpellRepo>();
            CreateSpellDto spellDto = new CreateSpellDto();
            spellDto.Name = "";
            spellDto.Description = "Test";
            spellDto.MPCost = 2;
            spellDto.SpellTarget = "Ally";

            var spellService = new SpellService(spellRepo.Object, _mapper);

            //Act
            var result = spellService.CreateSpell(spellDto);

            //Assert
            Assert.Null(result.Name);
        } 
        [Fact]
        public void CreateSpellNullTest()
        {
            //Arrange
            var spellRepo = new Mock<ISpellRepo>();
            CreateSpellDto spellDto = new CreateSpellDto();

            var spellService = new SpellService(spellRepo.Object, _mapper);

            //Act
            var result = spellService.CreateSpell(spellDto);

            //Assert
            Assert.Null(result.Name);
        } 

        [Fact]
        public void DeleteSpellSuccessTest()
        {
            //Arrange
            var spellRepo = new Mock<ISpellRepo>();
            Spell spellToDelete = new Spell();
            spellToDelete.Id = 1;
            spellToDelete.Name = "Kafrizzle";

            spellRepo.Setup(x => x.GetSpell(1)).Returns(spellToDelete);
            spellRepo.Setup(x => x.DeleteSpell(spellToDelete));

            var spellService = new SpellService(spellRepo.Object, _mapper);

            //Act
            var result = spellService.DeleteSpell(1);

            //Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Kafrizzle", result.Name);
        }

        [Fact]
        public void DeleteSpellDoesNotExistTest()
        {
            //Arrange
            var spellRepo = new Mock<ISpellRepo>();
            Spell spellToDelete = new Spell();

            spellRepo.Setup(x => x.GetSpell(1)).Returns(spellToDelete);

            var spellService = new SpellService(spellRepo.Object, _mapper);

            //Act
            var result = spellService.DeleteSpell(1);

            //Assert
            Assert.Null(result.Name);
            spellRepo.Verify(x => x.DeleteSpell(It.IsAny<Spell>()), Times.Never);
        }

        [Fact]
        public void GetSpellSuccessTest()
        {
            //Arrange
            var spellRepo = new Mock<ISpellRepo>();
            Spell spell = new Spell();
            spell.Id = 1;
            spell.Name = "Kafrizzle";

            spellRepo.Setup(x => x.GetSpell(1)).Returns(spell);
            var spellService = new SpellService(spellRepo.Object, _mapper);

            //Act
            var result = spellService.GetSpell(1);

            //Arrange
            Assert.Equal(1, result.Id);
            Assert.Equal("Kafrizzle", result.Name);
            spellRepo.Verify(x => x.GetSpell(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void GetSpellDoesntExistReturnIdZeroTest()
        {
            //Arrange
            var spellRepo = new Mock<ISpellRepo>();
            Spell spell = new Spell();

            spellRepo.Setup(x => x.GetSpell(1)).Returns(spell);
            var spellService = new SpellService(spellRepo.Object, _mapper);

            //Act
            var result = spellService.GetSpell(1);

            //Arrange
            Assert.Equal(0, result.Id);
            Assert.Null(result.Name);
        }

        [Fact]
        public void UpdateSpellSuccessTest()
        {
            //Arrange
            var spellRepo = new Mock<ISpellRepo>();
            Spell spell = new Spell();
            spell.Id = 1;
            spell.Name = "Zoom";
            spell.MpCost = 1;
            spell.SpellTarget = "Ally";
            spell.Description = "Fly";

            CreateSpellDto spellToUpdate = new CreateSpellDto();
            spellToUpdate.Name = "KaFrizzle";
            spellToUpdate.MPCost = 6;
            spellToUpdate.Description = "Burn";
            spellToUpdate.SpellTarget = "One Enemy";

            Spell updatedSpell = new Spell();
            updatedSpell.Id = 1;
            updatedSpell.Name = "KaFrizzle";
            updatedSpell.MpCost = 6;
            updatedSpell.Description = "Burn";
            updatedSpell.SpellTarget = "One Enemy";

            spellRepo.Setup(x=>x.GetSpell(1)).Returns(spell);
            spellRepo.Setup(x => x.UpdateSpell(updatedSpell));
            spellRepo.Setup(x => x.SaveChanges());
            var spellService = new SpellService(spellRepo.Object, _mapper);

            //Act
            var result = spellService.UpdateSpell(1, spellToUpdate);

            //Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("KaFrizzle", result.Name);
            Assert.Equal(6, result.MPCost);
            Assert.Equal("Burn", result.Description);
            Assert.Equal("One Enemy", result.SpellTarget);
        }

        [Fact]
        public void UpdateSpellNameIsVoidTest()
        {
            //Arrange
            var spellRepo = new Mock<ISpellRepo>();
            Spell spell = new Spell();
            spell.Id = 1;
            spell.Name = "Zoom";

            CreateSpellDto updatedSpell = new CreateSpellDto();

            spellRepo.Setup(x => x.GetSpell(1)).Returns(spell);
            var spellService = new SpellService(spellRepo.Object, _mapper);

            //Act
            var result = spellService.UpdateSpell(1, updatedSpell);

            //Arrange
            Assert.Equal(0, result.Id);
            Assert.Null(result.Name);
        }

        [Fact]
        public void UpdateSpellNameIsEmptyTest()
        {
            //Arrange
            var spellRepo = new Mock<ISpellRepo>();
            Spell spell = new Spell();
            spell.Id = 1;
            spell.Name = "Zoom";

            CreateSpellDto updatedSpell = new CreateSpellDto();
            updatedSpell.Name = " ";

            spellRepo.Setup(x => x.GetSpell(1)).Returns(spell);
            var spellService = new SpellService(spellRepo.Object, _mapper);

            //Act
            var result = spellService.UpdateSpell(1, updatedSpell);

            //Arrange
            Assert.Equal(0, result.Id);
            Assert.Null(result.Name);
        }

        [Fact]
        public void UpdateSpellDoesntExistTest()
        {
            //Arrange
            var spellRepo = new Mock<ISpellRepo>();
            Spell spell = new Spell();

            CreateSpellDto updatedSpell = new CreateSpellDto();
            updatedSpell.Name = "Zoom";

            spellRepo.Setup(x => x.GetSpell(1)).Returns(spell);
            var spellService = new SpellService(spellRepo.Object, _mapper);

            //Act
            var result = spellService.UpdateSpell(1, updatedSpell);

            //Arrange
            Assert.Equal(0, result.Id);
            Assert.Null(result.Name);
        }
    }
}