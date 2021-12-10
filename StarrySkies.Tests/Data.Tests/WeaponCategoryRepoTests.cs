using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StarrySkies.Data.Data;
using StarrySkies.Data.Models;
using StarrySkies.Data.Repositories.WeaponCategoryRepo;
using Xunit;

namespace StarrySkies.Tests.Data.Tests
{
    public class WeaponCategoryRepoTests
    {
        private IWeaponCategoryRepo GetInMemoryWeaponCategoryRepository()
        {
            DbContextOptions<ApplicationDbContext> options;
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(databaseName: "StarrySkies");
            options = builder.Options;
            ApplicationDbContext applicationDbContext = new ApplicationDbContext(options);
            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Database.EnsureCreated();
            return new WeaponCategoryRepo(applicationDbContext);
        }

        [Fact]
        public void CreateWeaponCategory()
        {
            //Arrange
            var weaponCategoryRepo = GetInMemoryWeaponCategoryRepository();
            WeaponCategory weaponCategory = new WeaponCategory();
            weaponCategory.Id = 1;
            weaponCategory.Name = "Sword";

            //Act
            weaponCategoryRepo.CreateWeaponCategory(weaponCategory);
            weaponCategoryRepo.SaveChanges();
            var results = weaponCategoryRepo.GetWeaponCategoryById(1);

            //Arrange
            Assert.Equal(1, results.Id);
            Assert.Equal("Sword", results.Name);
        }

        [Fact]
        public void GetAllWeaponCategories()
        {
            //Arrange
            var weaponCategoryRepo = GetInMemoryWeaponCategoryRepository();
            WeaponCategory weaponCategoryOne = new WeaponCategory();
            weaponCategoryOne.Id = 1;
            weaponCategoryOne.Name = "Sword";
            WeaponCategory weaponCategoryTwo = new WeaponCategory();
            weaponCategoryTwo.Id = 2;
            weaponCategoryTwo.Name = "Axe";
            weaponCategoryRepo.CreateWeaponCategory(weaponCategoryOne);
            weaponCategoryRepo.CreateWeaponCategory(weaponCategoryTwo);
            weaponCategoryRepo.SaveChanges();

            //Act
            ICollection<WeaponCategory> results = weaponCategoryRepo.GetAllWeaponCategories();

            //Assert
            Assert.Equal(2, results.Count);
        }

        [Fact]
        public void DeleteLocation()
        {
            //Arrange
            var weaponCategoryRepo = GetInMemoryWeaponCategoryRepository();
            WeaponCategory weaponCategory = new WeaponCategory();
            weaponCategory.Id = 1;
            weaponCategory.Name = "Lance";
            weaponCategoryRepo.CreateWeaponCategory(weaponCategory);
            weaponCategoryRepo.SaveChanges();
            var createdWeaponCategory = weaponCategoryRepo.GetWeaponCategoryById(1);
            Assert.Equal(1, createdWeaponCategory.Id);

            //Act
            weaponCategoryRepo.DeleteWeaponCategory(weaponCategory);
            weaponCategoryRepo.SaveChanges();
            var result = weaponCategoryRepo.GetWeaponCategoryById(1);

            //Assert
            Assert.Null(result);
        }
    }
}