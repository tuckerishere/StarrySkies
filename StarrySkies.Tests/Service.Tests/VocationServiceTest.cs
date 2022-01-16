using System.Collections.Generic;
using AutoMapper;
using Moq;
using StarrySkies.Data.Models;
using StarrySkies.Data.Repositories.VocationRepo;
using StarrySkies.Services.DTOs.VocationDtos;
using StarrySkies.Services.Mapping;
using StarrySkies.Services.Services.Vocations;
using Xunit;

namespace StarrySkies.Tests.Service.Tests
{
    public class VocationServiceTest
    {
        private readonly IMapper _mapper;

        public VocationServiceTest()
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
        public void GetAllVocationsTest()
        {
            //Arrange
            var vocationRepo = new Mock<IVocationRepo>();
            var vocationOne = new Vocation();
            vocationOne.Id = 1;
            vocationOne.Name = "Warrior";
            var vocationTwo = new Vocation();
            vocationTwo.Id = 2;
            vocationTwo.Name = "Sage";
            ICollection<Vocation> vocationList = new List<Vocation>();
            vocationList.Add(vocationOne);
            vocationList.Add(vocationTwo);

            vocationRepo.Setup(x => x.GetVocations()).Returns(vocationList);
            var vocationService = new VocationService(vocationRepo.Object, _mapper);

            //Act
            var result = vocationService.GetVocations();

            //Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetAllVocationsEmpty()
        {
            //Arrange
            var vocationRepo = new Mock<IVocationRepo>();
            ICollection<Vocation> vocationList = new List<Vocation>();

            vocationRepo.Setup(x => x.GetVocations()).Returns(vocationList);
            var vocationService = new VocationService(vocationRepo.Object, _mapper);

            //Act
            var results = vocationService.GetVocations();

            //Assert
            Assert.Empty(results);
        }

        [Fact]
        public void GetVocationByIdTest()
        {
            //Arrange
            var vocationRepo = new Mock<IVocationRepo>();
            var vocation = new Vocation();
            vocation.Id = 1;
            vocation.Name = "Sage";

            vocationRepo.Setup(x => x.GetVocationById(1)).Returns(vocation);
            var vocationService = new VocationService(vocationRepo.Object, _mapper);

            //Act
            var result = vocationService.GetVocationById(1);

            //Arrange
            Assert.Equal(1, result.Id);
            Assert.Equal("Sage", result.Name);
        }

        [Fact]
        public void GetVocationByIdDoesntExist()
        {
            //Arrange
            var vocationRepo = new Mock<IVocationRepo>();
            Vocation vocation = new Vocation();

            vocationRepo.Setup(x => x.GetVocationById(1)).Returns(vocation);
            var vocationService = new VocationService(vocationRepo.Object, _mapper);

            //Act
            var result = vocationService.GetVocationById(1);

            //Arrange
            Assert.Equal(0, result.Id);
            Assert.Null(result.Name);
        }

        [Fact]
        public void UpdateVocationTest()
        {
            //Arrange
            var vocationRepo = new Mock<IVocationRepo>();
            Vocation vocation = new Vocation();
            vocation.Id = 1;
            vocation.Name = "Sage";

            CreateVocationDto updatedVocation = new CreateVocationDto();
            updatedVocation.Name = "Thief";

            vocationRepo.Setup(x => x.GetVocationById(1)).Returns(vocation);
            vocationRepo.Setup(x => x.UpdateVocation(vocation));
            vocationRepo.Setup(x => x.SaveChanges());
            VocationService vocationService = new VocationService(vocationRepo.Object, _mapper);

            //Act
            var result = vocationService.UpdateVocation(1, updatedVocation);

            //Assert
            Assert.Equal("Thief", result.Name);
            Assert.Equal(1, result.Id);
            vocationRepo.Verify(x => x.SaveChanges(), Times.Once);
            vocationRepo.Verify(x => x.UpdateVocation(It.IsAny<Vocation>()), Times.Once);
        }

        [Fact]
        public void UpdateVocationDoesntExist() 
        {
            //Arrange
            var vocationRepo = new Mock<IVocationRepo>();
            Vocation vocation = new Vocation();
            CreateVocationDto createVocationDto = new CreateVocationDto();
            createVocationDto.Name = "Sage";

            vocationRepo.Setup(x => x.GetVocationById(1)).Returns(vocation);
            var vocationService = new VocationService(vocationRepo.Object, _mapper);

            //Act
            var result = vocationService.UpdateVocation(1, createVocationDto);

            //Assert
            Assert.Equal(0, result.Id);
        }

        [Fact]
        public void UpdateVocationWithBlankName()
        {
            //Arrange
            var vocationRepo = new Mock<IVocationRepo>();
            Vocation vocation = new Vocation();
            vocation.Name = "Sage";
            vocation.Id = 1;

            var updatedVocation = new CreateVocationDto();
            updatedVocation.Name = "";

            vocationRepo.Setup(x => x.GetVocationById(1)).Returns(vocation);
            var vocationService = new VocationService(vocationRepo.Object, _mapper);

            //Act
            var result = vocationService.UpdateVocation(1, updatedVocation);

            //Assert
            Assert.Equal(0, result.Id);
        }

        [Fact]
        public void UpdateVocationWithNullNameTest()
        {
            //Arrange
            var vocationRepo = new Mock<IVocationRepo>();
            Vocation vocation = new Vocation();
            vocation.Id = 1;
            vocation.Name = "Hero";

            vocationRepo.Setup(x => x.GetVocationById(1)).Returns(vocation);
            var vocationService = new VocationService(vocationRepo.Object, _mapper);

            //Act
            var result = vocationService.UpdateVocation(1, null);

            //Assert
            Assert.Equal(0, result.Id);
        }

        [Fact]
        public void DeleteVocationTest()
        {
            //Arrange
            var vocationRepo = new Mock<IVocationRepo>();
            Vocation vocation = new Vocation();
            vocation.Id = 1;
            vocation.Name = "Thief";

            vocationRepo.Setup(x => x.GetVocationById(1)).Returns(vocation);
            vocationRepo.Setup(x => x.DeleteVocation(vocation));
            vocationRepo.Setup(x => x.SaveChanges());
            var vocationService = new VocationService(vocationRepo.Object, _mapper);

            //Act
            var result = vocationService.DeleteVocation(1);

            //Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Thief", result.Name);
            vocationRepo.Verify(x => x.DeleteVocation(It.IsAny<Vocation>()), Times.Once);
            vocationRepo.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public void DeleteVocationDoesntExist()
        {
            //Arrange
            var vocationRepo = new Mock<IVocationRepo>();
            Vocation vocation = new Vocation();

            vocationRepo.Setup(x => x.GetVocationById(1)).Returns(vocation);
            var vocationService = new VocationService(vocationRepo.Object, _mapper);

            //Act
            var result = vocationService.DeleteVocation(1);

            //Assert
            Assert.Equal(0, result.Id);
            vocationRepo.Verify(x => x.DeleteVocation(It.IsAny<Vocation>()), Times.Never);
        }

        [Fact]
        public void CreateNewVocationTest()
        {
            //Arrange
            var vocationRepo = new Mock<IVocationRepo>();
            Vocation vocation = new Vocation();
            vocation.Name = "Barbarian";
            vocation.Id = 1;

            CreateVocationDto createVocation = new CreateVocationDto();
            createVocation.Name = "Barbarian";

            vocationRepo.Setup(x => x.CreateVocation(vocation));
            vocationRepo.Setup(x => x.SaveChanges());
            var vocationService = new VocationService(vocationRepo.Object, _mapper);

            //Act
            var result = vocationService.CreateVocation(createVocation);

            //Assert
            Assert.Equal("Barbarian", result.Name);
            vocationRepo.Verify(x=>x.CreateVocation(It.IsAny<Vocation>()), Times.Once);
            vocationRepo.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public void CreateNewVocationEmptyNameTest()
        {
            //Arrange
            var vocationRepo = new Mock<IVocationRepo>();
            var createdVocation = new CreateVocationDto();
            createdVocation.Name = "";

            var vocationService = new VocationService(vocationRepo.Object, _mapper);

            //Act
            var result = vocationService.CreateVocation(createdVocation);

            //Assert
            Assert.Equal(0, result.Id);
            Assert.Null(result.Name);
        }

        [Fact]
        public void CreateVocationNullTest()
        {
            //Arrange
            var vocationRepo = new Mock<IVocationRepo>();
            var vocationService = new VocationService(vocationRepo.Object, _mapper);

            //Act
            var result = vocationService.CreateVocation(null);

            //Assert
            Assert.Equal(0, result.Id);
            Assert.Null(result.Name);
        }
    }
}