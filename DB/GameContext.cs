using System;
using Microsoft.EntityFrameworkCore;
using JobAPI.Entities;
namespace JobAPI.DB;

public class GameContext(DbContextOptions<GameContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Explicitly set the precision and scale for the Price property
        modelBuilder.Entity<Game>()
            .Property(g => g.Price)
            .HasPrecision(18, 2); 

        base.OnModelCreating(modelBuilder);
        Genre.Seed(modelBuilder);
    }

}
