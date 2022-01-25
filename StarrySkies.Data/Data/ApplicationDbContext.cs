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
        public DbSet<Vocation> Vocations { get; set; }
        public DbSet<Spell> Spells { get; set; }
        public DbSet<VocationSpell> VocationSpells { get; set; }
    
    // protected override void OnModelCreating(ModelBuilder modelBuilder){

    //         //Create Composite key for VocationSpell
    //         modelBuilder.Entity<VocationSpell>()
    //             .HasKey(vs => new { vs.VocationId, vs.SpellId });
    //     }
}
}