using Microservices.Items.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Items.DataAccessLayer
{
    public class ItemContext: DbContext
    {
        public ItemContext(DbContextOptions<ItemContext> dbOptions) : base(dbOptions)
        {
            Database.EnsureCreated();
        }

        public DbSet<Item> Inventories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Item>()
                .HasKey(x => x.Id);

            builder.Seed();
        }
    }
}