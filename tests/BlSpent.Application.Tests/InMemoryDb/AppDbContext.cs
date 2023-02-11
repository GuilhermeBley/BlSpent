using Microsoft.EntityFrameworkCore;
using BlSpent.Application.Tests.Models;

namespace BlSpent.Application.Tests.InMemoryDb;

public class AppDbContext : DbContext
{
    public DbSet<UserModel> Users { get; set; } = null!;
    public DbSet<PageModel> Pages { get; set; } = null!;
    public DbSet<RolePageModel> RolesPages { get; set; } = null!;
    public DbSet<CostModel> Costs { get; set; } = null!;
    public DbSet<EarningModel> Earnigns { get; set; } = null!;
    public DbSet<GoalModel> GoalModels { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "Test");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserModel>()
            .HasIndex(p => new { p.UserName }).IsUnique();
        modelBuilder.Entity<RolePageModel>()
            .HasIndex(p => new { p.UserId, p.PageId }).IsUnique();
        modelBuilder.Entity<CostModel>()
            .Property(x => x.Value).HasPrecision(10,2);
        modelBuilder.Entity<EarningModel>()
            .Property(x => x.Value).HasPrecision(10,2);
        modelBuilder.Entity<GoalModel>()
            .Property(x => x.Value).HasPrecision(10,2);
    }
}