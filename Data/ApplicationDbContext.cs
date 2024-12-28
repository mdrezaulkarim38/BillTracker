using Microsoft.EntityFrameworkCore;
using BillTracker.Models;

namespace BillTracker.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .Property(p => p.BillAmount)
            .HasPrecision(18, 2);
        modelBuilder.Entity<Product>()
            .Property(p => p.ApprovedAmount)
            .HasPrecision(18, 2);
    }
}