using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StarrySkies.API.Controllers;
using StarrySkies.Services.DTOs.WeaponCategoryDTOs;
using StarrySkies.Services.ResponseModels;
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
            var serviceResponse = new ServiceResponse<ICollection<WeaponCategoryResponseDto>>();
            serviceResponse.Data = categoryList;

            weaponCategoryService.Setup(x => x.GetWeaponCategories()).Returns(serviceResponse);

            var categoryController = new WeaponCategoryController(weaponCategoryService.Object);

            //Act
            var results = categoryController.GetAllWeaponCategories();
            var listResults = (results.Result as OkObjectResult).Value as ServiceResponse<List<WeaponCategoryResponseDto>>;

            //Assert
            Assert.Equal(2, listResults.Data.Count);
            Assert.Equal("Dagger", listResults.Data[1].Name);
        }

        [Fact]
        public void GetAllWeaponCategoriesEmptyList(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            var categoryList = new List<WeaponCategoryResponseDto>();
            var serviceResponse = new ServiceResponse<ICollection<WeaponCategoryResponseDto>>();
            serviceResponse.Data = categoryList;

            categoryService.Setup(x => x.GetWeaponCategories()).Returns(serviceResponse);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var result = categoryController.GetAllWeaponCategories();
            var resultList = (result.Result as OkObjectResult).Value as ServiceResponse<List<WeaponCategoryResponseDto>>;

            //Assert
            Assert.Empty(resultList.Data);
        }

            [Fact]
            public void GetAllWeaponCategoriesStatusCode(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            var categoryList = new List<WeaponCategoryResponseDto>();
            var serviceResponse = new ServiceResponse<ICollection<WeaponCategoryResponseDto>>();
            serviceResponse.Data = categoryList;

            categoryService.Setup(x => x.GetWeaponCategories()).Returns(serviceResponse);
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
            var weaponCategory = new ServiceResponse<WeaponCategoryResponseDto>();
            var weaponCategoryDto = new WeaponCategoryResponseDto();
            weaponCategoryDto.Id = 1;
            weaponCategoryDto.Name = "Sword";
            weaponCategory.Data = weaponCategoryDto;

            categoryService.Setup(x => x.GetWeaponCategoryById(1)).Returns(weaponCategory);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var initialReturn = categoryController.GetWeaponCategory(1);
            var result = (initialReturn.Result as OkObjectResult).Value as ServiceResponse<WeaponCategoryResponseDto>;

            //Assert
            Assert.Equal(1, result.Data.Id);
            Assert.Equal("Sword", result.Data.Name);
        }

        [Fact]
        public void GetWeaponCategoryWithNoResult(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            var serviceResponse = new ServiceResponse<WeaponCategoryResponseDto>();
            serviceResponse.Success = false;

            categoryService.Setup(x => x.GetWeaponCategoryById(1)).Returns(serviceResponse);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var initalReults = categoryController.GetWeaponCategory(1);
            var results = (initalReults.Result as NotFoundObjectResult).StatusCode;

            //Arrange
            Assert.Equal(404, results);
        }

        [Fact]
        public void DeleteCategoryByIDTest(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            var weaponCategory = new WeaponCategoryResponseDto();
            weaponCategory.Id = 1;
            weaponCategory.Name = "Dagger";
            var serviceResponse = new ServiceResponse<WeaponCategoryResponseDto>();
            serviceResponse.Data = weaponCategory;

            categoryService.Setup(x => x.DeleteWeaponCategory(1)).Returns(serviceResponse);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var initialResult = categoryController.DeleteWeaponCategory(1);
            var result = (initialResult.Result as OkObjectResult).Value as ServiceResponse<WeaponCategoryResponseDto>;

            //Assert
            Assert.Equal(1, result.Data.Id);
            Assert.Equal("Dagger", result.Data.Name);
        }

        [Fact]
        public void DeleteCategoryByIDStatusCodeTest(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            var weaponCategory = new WeaponCategoryResponseDto();
            weaponCategory.Id = 1;
            weaponCategory.Name = "Dagger";
            var serviceResponse = new ServiceResponse<WeaponCategoryResponseDto>();
            serviceResponse.Data = weaponCategory;

            categoryService.Setup(x => x.DeleteWeaponCategory(1)).Returns(serviceResponse);
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
            var serviceResponse = new ServiceResponse<WeaponCategoryResponseDto>();
            serviceResponse.Success = false;

            categoryService.Setup(x => x.DeleteWeaponCategory(1)).Returns(serviceResponse);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var initialResults = categoryController.DeleteWeaponCategory(1);
            var results = (initialResults.Result as NotFoundObjectResult).StatusCode;

            //Arrange
            Assert.Equal(404, results);
        }

        [Fact]
        public void CreateWeaponCategoryTest(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            CreateWeaponCategoryDto weaponCategory = new CreateWeaponCategoryDto();
            weaponCategory.Name = "Dagger";
            var returnedCategory = new WeaponCategoryResponseDto();
            returnedCategory.Id = 1;
            returnedCategory.Name = "Dagger";
            var serviceResponse = new ServiceResponse<WeaponCategoryResponseDto>();
            serviceResponse.Data = returnedCategory;

            categoryService.Setup(x => x.CreateWeaponCategory(weaponCategory)).Returns(serviceResponse);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var initialResult = categoryController.CreateWewaponCategory(weaponCategory);
            var results = (initialResult.Result as CreatedAtActionResult).Value as ServiceResponse<WeaponCategoryResponseDto>;

            //Assert
            Assert.Equal(1, results.Data.Id);
            Assert.Equal("Dagger", results.Data.Name);
        }

        [Fact]
        public void CreateWeaponCategoryStatusCodeTest(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            CreateWeaponCategoryDto weaponCategory = new CreateWeaponCategoryDto();
            weaponCategory.Name = "Dagger";
            var returnedCategory = new WeaponCategoryResponseDto();
            returnedCategory.Id = 1;
            returnedCategory.Name = "Dagger";
            var serviceResponse = new ServiceResponse<WeaponCategoryResponseDto>();
            serviceResponse.Data = returnedCategory;

            categoryService.Setup(x => x.CreateWeaponCategory(weaponCategory)).Returns(serviceResponse);
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
            var serviceResponse = new ServiceResponse<WeaponCategoryResponseDto>();
            serviceResponse.Success = false;

            categoryService.Setup(x => x.CreateWeaponCategory(createdCategory)).Returns(serviceResponse);
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
            var categoryToUpdate = new WeaponCategoryResponseDto();
            categoryToUpdate.Id = 1;
            categoryToUpdate.Name = "Sword";
            var serviceResponse = new ServiceResponse<WeaponCategoryResponseDto>();
            serviceResponse.Data = categoryToUpdate;

            categoryService.Setup(x => x.UpdateWeaponCategory(1, updatedCategory)).Returns(serviceResponse);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var initialResult = categoryController.UpdateWeaponCategory(1, updatedCategory);
            var result = (initialResult.Result as OkObjectResult).Value as ServiceResponse<WeaponCategoryResponseDto>;

            //Assert
            Assert.Equal(1, result.Data.Id);
            Assert.Equal("Sword", result.Data.Name);
        }
        [Fact]
        public void UpdateWeaponCategoryStatusCodeTest(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            CreateWeaponCategoryDto updatedCategory = new CreateWeaponCategoryDto();
            updatedCategory.Name = "Sword";
            var categoryToUpdate = new WeaponCategoryResponseDto();
            categoryToUpdate.Id = 1;
            categoryToUpdate.Name = "Sword";
            var serviceResponse = new ServiceResponse<WeaponCategoryResponseDto>();
            serviceResponse.Data = categoryToUpdate;

            categoryService.Setup(x => x.UpdateWeaponCategory(1, updatedCategory)).Returns(serviceResponse);
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
            var serviceResponse = new ServiceResponse<WeaponCategoryResponseDto>();
            serviceResponse.Success = false;

            categoryService.Setup(x => x.UpdateWeaponCategory(1, updatedCategory)).Returns(serviceResponse);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var initialResult = categoryController.UpdateWeaponCategory(1, updatedCategory);
            var result = (initialResult.Result as NotFoundObjectResult).StatusCode;

            //Assert
            Assert.Equal(404, result);
        }

        [Fact]
        public void UpdateWeaponCategoryNoNameTest(){
            //Arrange
            var categoryService = new Mock<IWeaponCategoryService>();
            CreateWeaponCategoryDto updatedCategory = new CreateWeaponCategoryDto();
            var categoryToUpdate = new WeaponCategoryResponseDto();
            var serviceResponse = new ServiceResponse<WeaponCategoryResponseDto>();
            serviceResponse.Data = categoryToUpdate;

            categoryService.Setup(x => x.UpdateWeaponCategory(1, updatedCategory)).Returns(serviceResponse);
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
            var categoryToUpdate = new WeaponCategoryResponseDto();
            var serviceResponse = new ServiceResponse<WeaponCategoryResponseDto>();
            serviceResponse.Data = categoryToUpdate;

            categoryService.Setup(x => x.UpdateWeaponCategory(1, updatedCategory)).Returns(serviceResponse);
            var categoryController = new WeaponCategoryController(categoryService.Object);

            //Act
            var initialResult = categoryController.UpdateWeaponCategory(1, updatedCategory);
            var result = (initialResult.Result as BadRequestObjectResult).StatusCode;

            //Assert
            Assert.Equal(400, result);
        }
    }
}