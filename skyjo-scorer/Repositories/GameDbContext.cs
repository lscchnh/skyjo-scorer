using Microsoft.EntityFrameworkCore;
using Models.Domain;

namespace Repositories;

public class GameDbContext(DbContextOptions<GameDbContext> options) : DbContext(options)
{
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<PlayerScore> PlayerScores { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // game
        modelBuilder.Entity<Game>()
            .HasKey(g => g.Number);

        modelBuilder.Entity<Game>()
            .Property(g => g.Number)
            .ValueGeneratedOnAdd();

        // playerScore
        modelBuilder.Entity<PlayerScore>()
            .HasKey(ps => new { ps.PlayerName, ps.GameNumber });

        modelBuilder.Entity<PlayerScore>()
            .HasOne(ps => ps.Player)
            .WithMany(p => p.PlayerScores)
            .HasForeignKey(ps => ps.PlayerName)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PlayerScore>()
            .HasOne(ps => ps.Game)
            .WithMany(g => g.PlayerScores)
            .HasForeignKey(ps => ps.GameNumber)
            .OnDelete(DeleteBehavior.Cascade);

        // player
        modelBuilder.Entity<Player>()
            .HasKey(g => g.Name);
    }
}

