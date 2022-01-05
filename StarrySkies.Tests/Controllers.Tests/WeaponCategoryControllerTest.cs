using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StarrySkies.API.Controllers;
using StarrySkies.Services.DTOs.WeaponCategoryDTOs;
using StarrySkies.Services.Services.WeaponCategories;
using Xunit;

namespace StarrySkies.Tests.Controllers.Tests
{
    public class WeaponCategoryControllerTest
    {
        [Fact]
        public void GetAllWeaponCategoriesTest()
        {
            //Arrange
            var weaponCategoryService = new Mock<IWeaponCategoryService>();
            WeaponCategoryResponseDto categoryDto = new WeaponCategoryResponseDto();
            categoryDto.Id = 1;
            categoryDto.Name = "Sword";
            WeaponCategoryResponseDto categoryDtoTwo = new WeaponCategoryResponseDto();
            categoryDtoTwo.Id = 2;
            categoryDtoTwo.Name = "Dagger";
            ICollection<WeaponCategoryResponseDto> categoryList = new List<WeaponCategoryResponseDto>();
            categoryList.Add(categoryDto);
            categoryList.Add(categoryDtoTwo);

            weaponCategoryService.Setup(x => x.GetWeaponCategories()).Returns(categoryList);

            var categoryController = new WeaponCategoryController(weaponCategoryService.Object);

            //Act
            var results = categoryController.GetAllWeaponCategories();
            var listResults = (results.Result as OkObjectResult).Value as List<WeaponCategoryResponseDto>;

            //Assert
            Assert.Equal(2, listResults.Count);
            Assert.Equal("Dagger", listResults[1].Name);
        }

        [Fact]
        public void GetAllWeaponCategoriesEmptyList(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            var categoryList = new List<WeaponCategoryResponseDto>();

            categoryService.Setup(x => x.GetWeaponCategories()).Returns(categoryList);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var result = categoryController.GetAllWeaponCategories();
            var resultList = (result.Result as OkObjectResult).Value as List<WeaponCategoryResponseDto>;

            //Assert
            Assert.Empty(resultList);
        }

            [Fact]
            public void GetAllWeaponCategoriesStatusCode(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            var categoryList = new List<WeaponCategoryResponseDto>();

            categoryService.Setup(x => x.GetWeaponCategories()).Returns(categoryList);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var result = categoryController.GetAllWeaponCategories();
            var results = (result.Result as OkObjectResult).StatusCode;

            //Assert
            Assert.Equal(200,results);
        }

        [Fact]
        public void GetWeaponCategoryByIDTest(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            var weaponCategory = new WeaponCategoryResponseDto();
            weaponCategory.Id = 1;
            weaponCategory.Name = "Sword";

            categoryService.Setup(x => x.GetWeaponCategoryById(1)).Returns(weaponCategory);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var initialReturn = categoryController.GetWeaponCategory(1);
            var result = (initialReturn.Result as OkObjectResult).Value as WeaponCategoryResponseDto;

            //Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Sword", result.Name);
        }

        [Fact]
        public void GetWeaponCategoryWithNoResult(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            WeaponCategoryResponseDto category = new WeaponCategoryResponseDto();

            categoryService.Setup(x => x.GetWeaponCategoryById(1)).Returns(category);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var initalReults = categoryController.GetWeaponCategory(1);
            var results = (initalReults.Result as NotFoundResult).StatusCode;

            //Arrange
            Assert.Equal(404, results);
        }

        [Fact]
        public void DeleteCategoryByIDTest(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            WeaponCategoryResponseDto weaponCategory = new WeaponCategoryResponseDto();
            weaponCategory.Id = 1;
            weaponCategory.Name = "Dagger";

            categoryService.Setup(x => x.DeleteWeaponCategory(1)).Returns(weaponCategory);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var initialResult = categoryController.DeleteWeaponCategory(1);
            var result = (initialResult.Result as OkObjectResult).Value as WeaponCategoryResponseDto;

            //Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Dagger", result.Name);
        }

        [Fact]
        public void DeleteCategoryByIDStatusCodeTest(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            WeaponCategoryResponseDto weaponCategory = new WeaponCategoryResponseDto();
            weaponCategory.Id = 1;
            weaponCategory.Name = "Dagger";

            categoryService.Setup(x => x.DeleteWeaponCategory(1)).Returns(weaponCategory);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var initialResult = categoryController.DeleteWeaponCategory(1);
            var result = (initialResult.Result as OkObjectResult).StatusCode;

            //Assert
            Assert.Equal(200, result);
        }

        [Fact]
        public void DeleteCategoryDoesNotExist(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            WeaponCategoryResponseDto weaponCategory = new WeaponCategoryResponseDto();

            categoryService.Setup(x => x.DeleteWeaponCategory(1)).Returns(weaponCategory);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var initialResults = categoryController.DeleteWeaponCategory(1);
            var results = (initialResults.Result as NotFoundResult).StatusCode;

            //Arrange
            Assert.Equal(404, results);
        }

        [Fact]
        public void CreateWeaponCategoryTest(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            CreateWeaponCategoryDto weaponCategory = new CreateWeaponCategoryDto();
            weaponCategory.Name = "Dagger";
            WeaponCategoryResponseDto returnedCategory = new WeaponCategoryResponseDto();
            returnedCategory.Id = 1;
            returnedCategory.Name = "Dagger";

            categoryService.Setup(x => x.CreateWeaponCategory(weaponCategory)).Returns(returnedCategory);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var initialResult = categoryController.CreateWewaponCategory(weaponCategory);
            var results = (initialResult.Result as CreatedAtActionResult).Value as WeaponCategoryResponseDto;

            //Assert
            Assert.Equal(1, results.Id);
            Assert.Equal("Dagger", results.Name);
        }

        [Fact]
        public void CreateWeaponCategoryStatusCodeTest(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            CreateWeaponCategoryDto weaponCategory = new CreateWeaponCategoryDto();
            weaponCategory.Name = "Dagger";
            WeaponCategoryResponseDto returnedCategory = new WeaponCategoryResponseDto();
            returnedCategory.Id = 1;
            returnedCategory.Name = "Dagger";

            categoryService.Setup(x => x.CreateWeaponCategory(weaponCategory)).Returns(returnedCategory);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var initialResult = categoryController.CreateWewaponCategory(weaponCategory);
            var results = (initialResult.Result as CreatedAtActionResult).StatusCode;

            //Assert
            Assert.Equal(201, results);
        }

        [Fact]
        public void CreateWeaponCategotyWithoutNameTest(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            CreateWeaponCategoryDto createdCategory = new CreateWeaponCategoryDto();
            WeaponCategoryResponseDto categoryResponse = new WeaponCategoryResponseDto();

            categoryService.Setup(x => x.CreateWeaponCategory(createdCategory)).Returns(categoryResponse);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var initialResult = categoryController.CreateWewaponCategory(createdCategory);
            var result = (initialResult.Result as BadRequestObjectResult).StatusCode;

            //Assert
            Assert.Equal(400, result);
        }

        [Fact]
        public void UpdateWeaponCategoryTest(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            CreateWeaponCategoryDto updatedCategory = new CreateWeaponCategoryDto();
            updatedCategory.Name = "Sword";
            WeaponCategoryResponseDto categoryToUpdate = new WeaponCategoryResponseDto();
            categoryToUpdate.Id = 1;
            categoryToUpdate.Name = "Sword";

            categoryService.Setup(x => x.UpdateWeaponCategory(1, updatedCategory)).Returns(categoryToUpdate);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var initialResult = categoryController.UpdateWeaponCategory(1, updatedCategory);
            var result = (initialResult.Result as OkObjectResult).Value as WeaponCategoryResponseDto;

            //Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Sword", result.Name);
        }
        [Fact]
        public void UpdateWeaponCategoryStatusCodeTest(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            CreateWeaponCategoryDto updatedCategory = new CreateWeaponCategoryDto();
            updatedCategory.Name = "Sword";
            WeaponCategoryResponseDto categoryToUpdate = new WeaponCategoryResponseDto();
            categoryToUpdate.Id = 1;
            categoryToUpdate.Name = "Sword";

            categoryService.Setup(x => x.UpdateWeaponCategory(1, updatedCategory)).Returns(categoryToUpdate);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var initialResult = categoryController.UpdateWeaponCategory(1, updatedCategory);
            var result = (initialResult.Result as OkObjectResult).StatusCode;

            //Assert
            Assert.Equal(200, result);
        }

        [Fact]
        public void UpdateWeaponCategoryNotFoundTest(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            CreateWeaponCategoryDto updatedCategory = new CreateWeaponCategoryDto();
            updatedCategory.Name = "Sword";
            WeaponCategoryResponseDto categoryToUpdate = new WeaponCategoryResponseDto();

            categoryService.Setup(x => x.UpdateWeaponCategory(1, updatedCategory)).Returns(categoryToUpdate);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var initialResult = categoryController.UpdateWeaponCategory(1, updatedCategory);
            var result = (initialResult.Result as NotFoundResult).StatusCode;

            //Assert
            Assert.Equal(404, result);
        }

        [Fact]
        public void UpdateWeaponCategoryNoNameTest(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            CreateWeaponCategoryDto updatedCategory = new CreateWeaponCategoryDto();
            WeaponCategoryResponseDto categoryToUpdate = new WeaponCategoryResponseDto();

            categoryService.Setup(x => x.UpdateWeaponCategory(1, updatedCategory)).Returns(categoryToUpdate);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var initialResult = categoryController.UpdateWeaponCategory(1, updatedCategory);
            var result = (initialResult.Result as BadRequestObjectResult).StatusCode;

            //Assert
            Assert.Equal(400, result);
        }

        [Fact]
        public void UpdateWeaponCategoryEmptyNameTest(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            CreateWeaponCategoryDto updatedCategory = new CreateWeaponCategoryDto();
            updatedCategory.Name = "";
            WeaponCategoryResponseDto categoryToUpdate = new WeaponCategoryResponseDto();

            categoryService.Setup(x => x.UpdateWeaponCategory(1, updatedCategory)).Returns(categoryToUpdate);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var initialResult = categoryController.UpdateWeaponCategory(1, updatedCategory);
            var result = (initialResult.Result as BadRequestObjectResult).StatusCode;

            //Assert
            Assert.Equal(400, result);
        }
    }
}