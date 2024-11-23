using Microsoft.EntityFrameworkCore;
using BillTracker.Models;
namespace BillTracker.Data;
public class BillTrackerContext : DbContext
{
    public BillTrackerContext(DbContextOptions<BillTrackerContext> options) : base(options)
    {
    }

    // Define DbSet properties for each entity
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Bill> Bills { get; set; } = null!;
    public DbSet<PurchaseOrder> PurchaseOrders { get; set; } = null!;
    public DbSet<Challan> Challans { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User Entity Configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Name).HasMaxLength(100);
            entity.Property(u => u.Email).HasMaxLength(150);
            entity.Property(u => u.Password).HasMaxLength(255);
            entity.Property(u => u.Role).HasMaxLength(50);
        });

        // Bill Entity Configuration
        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.UniqueBillId).HasMaxLength(50).IsRequired();
            entity.Property(b => b.PoNo).HasMaxLength(50);
            entity.Property(b => b.BillNo).HasMaxLength(50);
            entity.Property(b => b.ChallanNo).HasMaxLength(50);
            entity.Property(b => b.CurrentStatus).HasMaxLength(50);
            entity.Property(b => b.QRCodeData).HasColumnType("text");

            // Foreign Key to User
            entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(b => b.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
        });

        // PurchaseOrder Entity Configuration
        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(po => po.Id);
            entity.Property(po => po.PoNo).HasMaxLength(50).IsRequired();
            entity.Property(po => po.ItemKey).HasMaxLength(50);
            entity.Property(po => po.Description).HasMaxLength(255);
            entity.Property(po => po.QtyReceived).HasColumnType("decimal(18,2)");
            entity.Property(po => po.PoAmount).HasColumnType("decimal(18,2)");
        });

        // Challan Entity Configuration
        modelBuilder.Entity<Challan>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.ChallanNo).HasMaxLength(50).IsRequired();
            entity.Property(c => c.Description).HasMaxLength(255);
            entity.Property(c => c.QtyReceived).HasColumnType("int");
        });
    }
}
