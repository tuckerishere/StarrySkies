using System;
using Microsoft.EntityFrameworkCore;
using StarrySkies.Data.Data;
using Xunit;
using Microsoft.EntityFrameworkCore.InMemory;
using StarrySkies.Data.Models;
using System.Collections.Generic;
using StarrySkies.Data.Repositories.SpellRepo;

namespace StarrySkies.Tests.Data.Tests
{
    public class SpellRepoTest
    {
        private ISpellRepo GetInMemorySpellRepository()
        {
            DbContextOptions<ApplicationDbContext> options;
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(databaseName: "SpellsTest");
            options = builder.Options;
            ApplicationDbContext applicationDbContext = new ApplicationDbContext(options);
            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Database.EnsureCreated();
            return new SpellRepo(applicationDbContext);
        }

        [Fact]
        public void CreateNewSpell()
        {
            ISpellRepo repo = GetInMemorySpellRepository();
            Spell spell = new Spell()
            {
                Id = 1,
                Name = "Zoom",
                Description = "Fly",
                SpellTarget = "One Ally",
                MpCost = 2
            };

            repo.CreateSpell(spell);

            Spell response = repo.GetSpell(1);

            Assert.Equal(1, response.Id);
            Assert.Equal("Zoom", response.Name);
            Assert.Equal("Fly", response.Description);
            Assert.Equal("One Ally", response.SpellTarget);
            Assert.Equal(2, response.MpCost);
        }

        [Fact]
        public void GetAllSpellsTest()
        {
            ISpellRepo repo = GetInMemorySpellRepository();
            Spell spellOne = new Spell()
            {
                Id = 1,
                Name = "Zoom",
                Description = "Fly",
                SpellTarget = "One Ally"
            };
            Spell spellTwo = new Spell()
            {
                Id = 2,
                Name = "Accelerate",
                Description = "Speed Up",
                SpellTarget = "All Allies"
            };

            repo.CreateSpell(spellOne);
            repo.CreateSpell(spellTwo);
            repo.SaveChanges();

            ICollection<Spell> response = repo.GetSpells();

            Assert.Equal(2, response.Count);
        }

        [Fact]
        public void UpdateSpellTest()
        {
            ISpellRepo repo = GetInMemorySpellRepository();

            Spell spell = new Spell()
            {
                Id = 1,
                Name = "Zoom",
                Description = "Fly",
                SpellTarget = "One Ally"
            };

            repo.CreateSpell(spell);
            repo.SaveChanges();
            spell.Name = "Accelerate";
            repo.UpdateSpell(spell);
            repo.SaveChanges();

            Spell response = repo.GetSpell(1);

            Assert.Equal("Accelerate", response.Name);

        }

        [Fact]
        public void DeleteSpellTest()
        {
            ISpellRepo repo = GetInMemorySpellRepository();
            Spell spell = new Spell()
            {
                Id = 1,
                Name = "Zoom",
                Description = "Fly",
                SpellTarget = "One Ally"
            };

            repo.CreateSpell(spell);
            repo.SaveChanges();
            Spell response = repo.GetSpell(1);
            Assert.Equal("Zoom", spell.Name);

            repo.DeleteSpell(spell);
            repo.SaveChanges();
            response = repo.GetSpell(1);

            Assert.Null(response);
        }
    }
}