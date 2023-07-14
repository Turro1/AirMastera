using AirMastera.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AirMastera.Infrastructure.Data;

/// <summary>
/// Класс контекста базы данных
/// </summary>
public class AirMasteraDbContext : DbContext
{
    public DbSet<PersonDb> Persons { get; set; }
    public DbSet<CarDb> Cars { get; set; }
    public DbSet<RepairDb> Repairs { get; set; }

    public AirMasteraDbContext(DbContextOptions<AirMasteraDbContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
            .Entity<PersonDb>()
            .HasKey(person => person.Id);
        modelBuilder
            .Entity<PersonDb>()
            .HasIndex(person => person.Id)
            .IsUnique();
        modelBuilder
            .Entity<PersonDb>()
            .HasIndex(person => person.Phone)
            .IsUnique();

        modelBuilder
            .Entity<CarDb>()
            .HasOne(p => p.PersonDb)
            .WithMany(t => t.Cars)
            .HasForeignKey(p => p.PersonDbId);
        modelBuilder
            .Entity<CarDb>()
            .HasKey(experience => experience.Id);
        modelBuilder
            .Entity<CarDb>()
            .HasIndex(experience => experience.Id)
            .IsUnique();
        modelBuilder
            .Entity<CarDb>()
            .HasIndex(experience => experience.Number)
            .IsUnique();
        modelBuilder
            .Entity<CarDb>()
            .Property(experience => experience.Name)
            .HasMaxLength(250);
        modelBuilder
            .Entity<CarDb>()
            .Property(experience => experience.Model)
            .HasMaxLength(250);

        modelBuilder
            .Entity<RepairDb>()
            .HasOne(p => p.CarDb)
            .WithMany(t => t.Repairs)
            .HasForeignKey(p => p.CarDbId);
        modelBuilder
            .Entity<RepairDb>()
            .HasKey(experience => experience.Id);
        modelBuilder
            .Entity<RepairDb>()
            .HasIndex(experience => experience.Id)
            .IsUnique();
        modelBuilder
            .Entity<RepairDb>()
            .Property(experience => experience.PartType)
            .HasMaxLength(250);
        modelBuilder
            .Entity<RepairDb>()
            .Property(experience => experience.PartName)
            .HasMaxLength(250);
    }
}