﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StarrySkies.Data.Data;

namespace StarrySkies.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("StarrySkies.Data.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("StarrySkies.Data.Models.Spell", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MpCost")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpellTarget")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Spells");
                });

            modelBuilder.Entity("StarrySkies.Data.Models.Vocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vocations");
                });

            modelBuilder.Entity("StarrySkies.Data.Models.VocationSpell", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("LevelLearned")
                        .HasColumnType("int");

                    b.Property<int>("SpellId")
                        .HasColumnType("int");

                    b.Property<int>("VocationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpellId");

                    b.HasIndex("VocationId");

                    b.ToTable("VocationSpells");
                });

            modelBuilder.Entity("StarrySkies.Data.Models.WeaponCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WeaponCategories");
                });

            modelBuilder.Entity("StarrySkies.Data.Models.VocationSpell", b =>
                {
                    b.HasOne("StarrySkies.Data.Models.Spell", "Spell")
                        .WithMany("VocationsSpells")
                        .HasForeignKey("SpellId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StarrySkies.Data.Models.Vocation", "Vocation")
                        .WithMany("VocationSpells")
                        .HasForeignKey("VocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Spell");

                    b.Navigation("Vocation");
                });

            modelBuilder.Entity("StarrySkies.Data.Models.Spell", b =>
                {
                    b.Navigation("VocationsSpells");
                });

            modelBuilder.Entity("StarrySkies.Data.Models.Vocation", b =>
                {
                    b.Navigation("VocationSpells");
                });
#pragma warning restore 612, 618
        }
    }
}
