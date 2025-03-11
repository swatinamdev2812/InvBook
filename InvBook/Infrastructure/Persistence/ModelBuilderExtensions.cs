using InvBook.Domain;
using Microsoft.EntityFrameworkCore;

namespace InvBook.Infrastructure.Persistence
{
    public static class ModelBuilderExtensions
    {
        public static void ConfigureEntities(this ModelBuilder modelBuilder)
        {
            // Configure Member entity
            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Name).IsRequired().HasMaxLength(100);
                entity.Property(m => m.Surname).IsRequired().HasMaxLength(100);
                entity.Property(m => m.DateOfJoining).IsRequired();
            });

            // Configure InventoryItem entity
            modelBuilder.Entity<InventoryItem>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.Property(i => i.Title).IsRequired().HasMaxLength(200);
                entity.Property(i => i.Description).HasMaxLength(500);
                entity.Property(i => i.RemainingCount).HasDefaultValue(0);
                entity.Property(i => i.ExpirationDate).IsRequired();
            });

            // Configure Booking entity
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.HasOne(x => x.Member)
                      .WithMany(m => m.Bookings)
                      .HasForeignKey(b => b.MemberId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.InventoryItem)
                      .WithMany(m => m.Bookings)
                      .HasForeignKey(b => b.InventoryItemId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(b => b.BookingDate).IsRequired();
            });
        }
    }
}
