using Microsoft.EntityFrameworkCore;
using BlSpent.Application.Tests.Models;

namespace BlSpent.Application.Tests.InMemoryDb;

public class AppDbContext : DbContext
{
    public DbSet<UserDbModel> Users { get; set; } = null!;
    public DbSet<PageDbModel> Pages { get; set; } = null!;
    public DbSet<RolePageDbModel> RolesPages { get; set; } = null!;
    public DbSet<CostDbModel> Costs { get; set; } = null!;
    public DbSet<EarningDbModel> Earnings { get; set; } = null!;
    public DbSet<GoalDbModel> Goals { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "Test");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserDbModel>()
            .HasIndex(p => new { p.UserName }).IsUnique();
        modelBuilder.Entity<RolePageDbModel>()
            .HasIndex(p => new { p.UserId, p.PageId }).IsUnique();
        modelBuilder.Entity<CostDbModel>()
            .Property(x => x.Value).HasPrecision(10,2);
        modelBuilder.Entity<EarningDbModel>()
            .Property(x => x.Value).HasPrecision(10,2);
        modelBuilder.Entity<GoalDbModel>()
            .Property(x => x.Value).HasPrecision(10,2);
    }
}