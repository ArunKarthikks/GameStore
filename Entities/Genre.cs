using System;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;

namespace JobAPI.Entities;

public class Genre
{
public int Id { get; set; }
public required string Name { get; set; }

public static void Seed(ModelBuilder modelBuilder)
{
modelBuilder.Entity<Genre>().HasData(
    new Genre { Id = 1, Name = "Action-adventure" },
    new Genre { Id = 2, Name = "Platformer" },
    new Genre { Id = 3, Name = "RPG" }
);   
}
}