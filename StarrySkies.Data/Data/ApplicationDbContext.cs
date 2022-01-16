using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StarrySkies.Data.Models;

namespace StarrySkies.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Location> Locations { get; set; }
        public DbSet<WeaponCategory> WeaponCategories { get; set; }
        public DbSet<Vocation> Vocations{ get; set; }
        public DbSet<Spell> Spells { get; set; }


    }
}