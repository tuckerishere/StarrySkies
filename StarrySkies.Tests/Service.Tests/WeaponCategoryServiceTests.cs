using System.Collections.Generic;
using AutoMapper;
using Moq;
using StarrySkies.Data.Models;
using StarrySkies.Data.Repositories.WeaponCategoryRepo;
using StarrySkies.Services.DTOs.WeaponCategoryDTOs;
using StarrySkies.Services.Mapping;
using StarrySkies.Services.Services.WeaponCategories;
using Xunit;

namespace StarrySkies.Tests.Service.Tests
{
    public class WeaponCategoryServiceTests
    {
        private readonly IMapper _mapper;
        public WeaponCategoryServiceTests()
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
        public void CreateNewWeaponCategoryTest()
        {
            //Arrange
            var categoryRepo = new Mock<IWeaponCategoryRepo>();
            WeaponCategory createdWeaponCategory = new WeaponCategory();
            CreateWeaponCategoryDto createdCategoryDto = new CreateWeaponCategoryDto();
            createdWeaponCategory.Id = 1;
            createdWeaponCategory.Name = "Axe";
            createdCategoryDto.Name = "Axe";

            categoryRepo.Setup(x =>x.CreateWeaponCategory(createdWeaponCategory));
            var categoryService = new WeaponCategoryService(_mapper, categoryRepo.Object);

            //Act
            var results = categoryService.CreateWeaponCategory(createdCategoryDto);

            //Arrange
            Assert.Equal("Axe", results.Data.Name);
        }

        [Fact]
        public void CreateNewWeaponCategoryWithNullName()
        {
            //Arrange
            var categoryRepo = new Mock<IWeaponCategoryRepo>();
            WeaponCategory category = new WeaponCategory();
            CreateWeaponCategoryDto createWeapon = new CreateWeaponCategoryDto();

            categoryRepo.Setup(x =>x.CreateWeaponCategory(category));

            var categoryService = new WeaponCategoryService(_mapper, categoryRepo.Object);

            //Act
            var results = categoryService.CreateWeaponCategory(createWeapon);

            //Assert
            Assert.Null(results.Data);
        }

        [Fact]
        public void CreateLocationWithEmptyName()
        {
            //Assert
            var weaponCategoryRepo = new Mock<IWeaponCategoryRepo>();
            WeaponCategory category = new WeaponCategory();
            category.Id = 1;
            category.Name = "";
            CreateWeaponCategoryDto categoryToCreate = new CreateWeaponCategoryDto();
            categoryToCreate.Name = "";

            weaponCategoryRepo.Setup(x => x.CreateWeaponCategory(category));
            var categoryService = new WeaponCategoryService(_mapper, weaponCategoryRepo.Object);

            //Act
            var results = categoryService.CreateWeaponCategory(categoryToCreate);

            //Arrange
            Assert.Null(results.Data);
        }

        [Fact]
        public void DeleteWeaponCategoryTest()
        {
            //Assert
            var categoryRepo = new Mock<IWeaponCategoryRepo>();
            WeaponCategory weaponCategory = new WeaponCategory()
            {
                Id = 1,
                Name = "Axe"
            };

            categoryRepo.Setup(x => x.GetWeaponCategoryById(1)).Returns(weaponCategory);
            categoryRepo.Setup(x => x.DeleteWeaponCategory(weaponCategory));
            var categoryService = new WeaponCategoryService(_mapper, categoryRepo.Object);

            //Act
            var results = categoryService.DeleteWeaponCategory(1);

            //Assert
            Assert.Equal(1, results.Data.Id);
            Assert.Equal("Axe", results.Data.Name);
        }

        [Fact]
        public void DeleteWeaponCategoryNoResults()
        {
            //Arrange
            var categoryRepo = new Mock<IWeaponCategoryRepo>();
            categoryRepo.Setup(x => x.GetWeaponCategoryById(1));

            var categoryService = new WeaponCategoryService(_mapper, categoryRepo.Object);

            //Act
            var results = categoryService.DeleteWeaponCategory(1);

            //Assert
            Assert.Null(results.Data);
        }

        [Fact]
        public void GetAllWeaponCategoriesTest()
        {
            //Arrange
            var categoryRepo = new Mock<IWeaponCategoryRepo>();
            WeaponCategory categoryOne = new WeaponCategory();
            categoryOne.Id = 1;
            categoryOne.Name = "Lance";
            WeaponCategory categoryTwo = new WeaponCategory();
            categoryTwo.Id = 2;
            categoryTwo.Name = "Axe";
            ICollection<WeaponCategory> categoryList = new List<WeaponCategory>();
            categoryList.Add(categoryOne);
            categoryList.Add(categoryTwo);

            categoryRepo.Setup(x => x.GetAllWeaponCategories()).Returns(categoryList);
            var categoryService = new WeaponCategoryService(_mapper, categoryRepo.Object);

            //Act
            var results = categoryService.GetWeaponCategories();

            //Assert
            Assert.Equal(2, results.Data.Count);
        }

        [Fact]
        public void AllWeaponCategoriesEmptyList()
        {
            //Arrange
            var categoryRepo = new Mock<IWeaponCategoryRepo>();
            ICollection<WeaponCategory> categoryList = new List<WeaponCategory>();
            categoryRepo.Setup(x => x.GetAllWeaponCategories()).Returns(categoryList);

            var categoryService = new WeaponCategoryService(_mapper, categoryRepo.Object);

            //Act
            var results = categoryService.GetWeaponCategories();

            //Assert
            Assert.Empty(results.Data);
        }

        [Fact]
        public void GetWeaponCategoryById()
        {
            //Arrange
            var categoryRepo = new Mock<IWeaponCategoryRepo>();
            WeaponCategory category = new WeaponCategory();
            category.Id = 1;
            category.Name = "Axe";

            categoryRepo.Setup(x => x.GetWeaponCategoryById(1)).Returns(category);
            var categoryService = new WeaponCategoryService(_mapper, categoryRepo.Object);

            //Act
            var results = categoryService.GetWeaponCategoryById(1);

            //Assert
            Assert.Equal(1, results.Data.Id);
            Assert.Equal("Axe", results.Data.Name);
        }

        [Fact]
        public void GetCategoryByIDCategoryDoesntExist()
        {
            //Arrange
            var categoryRepo = new Mock<IWeaponCategoryRepo>();
            categoryRepo.Setup(x => x.GetWeaponCategoryById(1));
            var categoryService = new WeaponCategoryService(_mapper, categoryRepo.Object);

            //Act
            var results = categoryService.GetWeaponCategoryById(1);

            //Arrange
            Assert.Null(results.Data);
        }

        [Fact]
        public void UpdateWeaponCategoryTest()
        {
            //Arrange
            var categoryRepo = new Mock<IWeaponCategoryRepo>();
            WeaponCategory category = new WeaponCategory();
            category.Id = 1;
            category.Name = "Axe";
            CreateWeaponCategoryDto updatedName = new CreateWeaponCategoryDto();
            updatedName.Name = "Lance";

            categoryRepo.Setup(x=>x.GetWeaponCategoryById(category.Id)).Returns(category);

            var categoryService = new WeaponCategoryService(_mapper, categoryRepo.Object);

            //Act
            var results = categoryService.UpdateWeaponCategory(1, updatedName);

            //Assert
            Assert.Equal("Lance", results.Data.Name);
        }

        [Fact]
        public void UpdateNameWithEmptyString()
        {
            //Arrange
            var categoryRepo = new Mock<IWeaponCategoryRepo>();
            WeaponCategory category = new WeaponCategory();
            category.Id = 1;
            category.Name = "Sword";
            CreateWeaponCategoryDto updateName = new CreateWeaponCategoryDto();
            updateName.Name = "";

            categoryRepo.Setup(x => x.GetWeaponCategoryById(1)).Returns(category);
            var categoryService = new WeaponCategoryService(_mapper, categoryRepo.Object);

            //Act
            var results = categoryService.UpdateWeaponCategory(1, updateName);

            //Assert
            Assert.Null(results.Data);
        }
        [Fact]
        public void UpdateNameWithIdNotExisting()
        {
            //Arrange
            var categoryRepo = new Mock<IWeaponCategoryRepo>();
            WeaponCategory category = new WeaponCategory();
            category.Id = 1;
            category.Name = "Sword";
            CreateWeaponCategoryDto updateName = new CreateWeaponCategoryDto();
            updateName.Name = "";

            categoryRepo.Setup(x => x.GetWeaponCategoryById(1));
            var categoryService = new WeaponCategoryService(_mapper, categoryRepo.Object);

            //Act
            var results = categoryService.UpdateWeaponCategory(1, updateName);

            //Assert
            Assert.Null(results.Data);
        }

        [Fact]
        public void UpdateNameWithNullName()
        {
            //Arrange
            var categoryRepo = new Mock<IWeaponCategoryRepo>();
            WeaponCategory category = new WeaponCategory();
            category.Id = 1;
            category.Name = "Sword";
            CreateWeaponCategoryDto updateName = new CreateWeaponCategoryDto();
            updateName.Name = null;

            categoryRepo.Setup(x => x.GetWeaponCategoryById(1));
            var categoryService = new WeaponCategoryService(_mapper, categoryRepo.Object);

            //Act
            var results = categoryService.UpdateWeaponCategory(1, updateName);

            //Assert
            Assert.Null(results.Data);
        }
    }
}