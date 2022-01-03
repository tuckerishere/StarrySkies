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
    }
}