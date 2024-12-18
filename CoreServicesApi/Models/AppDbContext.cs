﻿using Microsoft.EntityFrameworkCore;
namespace Models;

public class AppDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<Product> Products { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bill>()
            .HasOne(i => i.User)
            .WithMany(u => u.Bills)
            .HasForeignKey(i => i.UserId);
    }
}
