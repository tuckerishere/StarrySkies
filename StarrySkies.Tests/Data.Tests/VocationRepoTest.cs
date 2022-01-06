using Microsoft.EntityFrameworkCore;
using StarrySkies.Data.Data;
using StarrySkies.Data.Models;
using StarrySkies.Data.Repositories.VocationRepo;
using Xunit;

namespace StarrySkies.Tests.Data.Tests
{
    public class VocationRepoTest
    {
        private IVocationRepo GetInMemoryVocationRepository()
        {
            DbContextOptions<ApplicationDbContext> options;
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(databaseName: "Vocations");
            options = builder.Options;
            ApplicationDbContext applicationDbContext = new ApplicationDbContext(options);
            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Database.EnsureCreated();
            return new VocationRepo(applicationDbContext);
        }

        [Fact]
        public void CreateVocationTest()
        {
            //Arrange
            IVocationRepo repo = GetInMemoryVocationRepository();
            Vocation vocation = new Vocation()
            {
                Id = 1,
                Name = "Warrior"
            };

            //Act
            repo.CreateVocation(vocation);
            repo.SaveChanges();

            var result = repo.GetVocationById(1);

            //Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Warrior", result.Name);
        }

        [Fact]
        public void GetAllVocationsTest()
        {
            //Arrange
            var repo = GetInMemoryVocationRepository();
            Vocation vocationOne = new Vocation()
            {
                Id = 1,
                Name = "Sage"
            };

            Vocation vocationTwo = new Vocation()
            {
                Id = 2,
                Name = "Warrior"
            };

            repo.CreateVocation(vocationOne);
            repo.CreateVocation(vocationTwo);
            repo.SaveChanges();

            //Act
            var result = repo.GetVocations();

            //Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void DeleteFromRepoTest()
        {
            //Arrange
            var repo = GetInMemoryVocationRepository();
            Vocation vocation = new Vocation()
            {
                Id = 1,
                Name = "Sage"
            };

            repo.CreateVocation(vocation);
            repo.SaveChanges();

            var initialResult = repo.GetVocationById(1);
            Assert.Equal(1, initialResult.Id);

            //Act
            repo.DeleteVocation(vocation);
            repo.SaveChanges();

            var deletedResults = repo.GetVocations();

            //Assert
            Assert.Equal(0, deletedResults.Count);
        }

        [Fact]
        public void GetVocationById()
        {
            //Arrange
            var repo = GetInMemoryVocationRepository();
            Vocation vocation = new Vocation()
            {
                Id = 1,
                Name = "Sage"
            };

            repo.CreateVocation(vocation);
            repo.SaveChanges();

            //Act
            var result = repo.GetVocationById(1);

            //Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Sage", result.Name);
        }

        [Fact]
        public void UpdateVocationRepoTest()
        {
            //Arrange
            var repo = GetInMemoryVocationRepository();
            Vocation vocation = new Vocation()
            {
                Id = 1,
                Name = "Warrior"
            };

            repo.CreateVocation(vocation);
            repo.SaveChanges();
            vocation.Name = "Sage";

            //Act
            repo.UpdateVocation(vocation);
            var result = repo.GetVocationById(1);

            //Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Sage", result.Name);
        }
    }
}