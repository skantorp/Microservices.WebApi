using Microservices.Inventories.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Books.DataAccessLayer
{
	public static class InitialDataSeed
	{
		public static void Seed(this ModelBuilder builder)
		{
			builder.Entity<Inventory>().HasData(
				new Inventory
				{
					Id = 1,
					Name = "initial"
				}
			);

			builder.Entity<InventoryItem>().HasData(
				new InventoryItem
                {
					InventoryId = 1,
					ItemId = 1,
					Quantity = 3
				},
                new InventoryItem
                {
                    InventoryId = 1,
                    ItemId = 2,
                    Quantity = 2
                }
            );
		}
	}
}