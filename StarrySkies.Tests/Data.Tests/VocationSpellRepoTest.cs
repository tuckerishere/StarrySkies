using Microsoft.EntityFrameworkCore;
using StarrySkies.Data.Data;
using StarrySkies.Data.Models;
using StarrySkies.Data.Repositories.VocationSpellRepo;
using Xunit;

namespace StarrySkies.Tests.Data.Tests
{
    public class VocationSpellRepoTest
    {
        private IVocationSpellRepo GetInMemoryVocationSpellRepository()
        {
            DbContextOptions<ApplicationDbContext> options;
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(databaseName: "VocationSpellTest");
            options = builder.Options;
            ApplicationDbContext applicationDbContext = new ApplicationDbContext(options);
            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Database.EnsureCreated();
            return new VocationSpellRepo(applicationDbContext);
        }

        private VocationSpell CreateVocationSpell(int id)
        {
            Spell spell = new Spell();
            spell.Id = id;
            spell.Name = "Frizz";
            Vocation vocation = new Vocation();
            vocation.Id = id;
            vocation.Name = "Mage";
            VocationSpell vocationSpell = new VocationSpell();
            vocationSpell.Vocation = vocation;
            vocationSpell.Spell = spell;
            vocationSpell.SpellId = id;
            vocationSpell.VocationId = id;
            vocationSpell.Id = id;

            return vocationSpell;
        }

        [Fact]
        public void CreateAndGetVocationSpellTest()
        {
            //Arrange
            var vocationSpellRepo = GetInMemoryVocationSpellRepository();
            VocationSpell vocationSpell = CreateVocationSpell(1);

            //Act
            vocationSpellRepo.CreateVocationSpell(vocationSpell);
            vocationSpellRepo.SaveChanges();
            var result = vocationSpellRepo.GetVocationSpell(1,1);

            //Assert
            Assert.Equal(1, result.SpellId);
            Assert.Equal(1, result.VocationId);
            Assert.Equal("Frizz", result.Spell.Name);
            Assert.Equal("Mage", result.Vocation.Name);
        }

        [Fact]
        public void GetAllVocationSpells()
        {
            //Arrange
            var vocationSpellRepo = GetInMemoryVocationSpellRepository();
            VocationSpell vocationSpellOne = CreateVocationSpell(1);
            VocationSpell vocationSpellTwo = CreateVocationSpell(2);

            vocationSpellRepo.CreateVocationSpell(vocationSpellOne);
            vocationSpellRepo.CreateVocationSpell(vocationSpellTwo);
            vocationSpellRepo.SaveChanges();

            //Act
            var results = vocationSpellRepo.GetVocationSpells();

            //Assert
            Assert.Equal(2, results.Count);
        }

        [Fact]
        public void DeleteVocationSpellRepoTest()
        {
            //Arrange
            var vocationSpellRepo = GetInMemoryVocationSpellRepository();
            var vocationSpell = CreateVocationSpell(1);
            vocationSpellRepo.CreateVocationSpell(vocationSpell);
            vocationSpellRepo.SaveChanges();

            var initialResult = vocationSpellRepo.GetVocationSpell(1, 1);
            Assert.Equal(1, initialResult.SpellId);


            //Act
            vocationSpellRepo.DeleteVocationSpell(vocationSpell);
            vocationSpellRepo.SaveChanges();
            var result = vocationSpellRepo.GetVocationSpell(1, 1);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void UpdateVocationSpellRepoTest()
        {
            //Arrange
            var vocationSpellRepo = GetInMemoryVocationSpellRepository();
            VocationSpell vocationSpell = CreateVocationSpell(1);
            vocationSpellRepo.CreateVocationSpell(vocationSpell);
            vocationSpellRepo.SaveChanges();
            vocationSpell.Spell.Name = "Zoom";
            vocationSpellRepo.UpdateVocationSpell(vocationSpell);
            vocationSpellRepo.SaveChanges();
            var result = vocationSpellRepo.GetVocationSpell(1, 1);

            //Assert
            Assert.Equal("Zoom", result.Spell.Name);
        }
    }
}