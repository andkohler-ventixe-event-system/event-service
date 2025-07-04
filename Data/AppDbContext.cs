﻿using EventService.Models;
using Microsoft.EntityFrameworkCore;

namespace EventService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Event> Events => Set<Event>();
    public DbSet<Package> Packages => Set<Package>();
    public DbSet<Perk> Perks => Set<Perk>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>()
            .HasMany(e => e.Packages)
            .WithOne(p => p.Event)
            .HasForeignKey(p => p.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Package>()
            .HasMany(p => p.Perks)
            .WithOne(perk => perk.Package)
            .HasForeignKey(perk => perk.PackageId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Event>()
            .Property(e => e.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Package>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");
    }
}
