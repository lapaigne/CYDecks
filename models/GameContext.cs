using System;
using Godot;
using Microsoft.EntityFrameworkCore;

public class GameContext : DbContext
{
    private string connectionString =
        "server=localhost;user=root;password=7FfTo78Cvzql;database=cydeckdb;";
    public DbSet<CardModel> Cards { get; set; }
    public DbSet<ResourceModel> Resources { get; set; }

    public GameContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}
