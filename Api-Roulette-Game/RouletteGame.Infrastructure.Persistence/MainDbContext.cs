using Microsoft.EntityFrameworkCore;
using RouletteGame.Core.Domain.Entities;

namespace RouletteGame.Infrastructure.Persistence;

public class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

    //DbSet
    public DbSet<Player> Players { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>()
            .HasKey(x => x.Name);

        modelBuilder.Entity<Player>()
            .Property(x => x.Amount)
            .IsRequired();
    }
}
