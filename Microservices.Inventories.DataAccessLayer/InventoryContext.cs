using Books.DataAccessLayer;
using Microservices.Inventories.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Inventories.DataAccessLayer
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> dbOptions) : base(dbOptions)
        {
            Database.EnsureCreated();
        }

        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Inventory>()
                .HasKey(x => x.Id);

            builder.Entity<InventoryItem>()
                .HasKey(x => new { x.InventoryId, x.ItemId });

            builder.Entity<InventoryItem>()
                .HasOne(x => x.Inventory)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.InventoryId);

            builder.Seed();
        }
    }
}