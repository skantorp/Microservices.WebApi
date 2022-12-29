using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Inventories.DataAccessLayer.Entities
{
    public class Inventory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<InventoryItem> Items { get; set; } = new List<InventoryItem>();
    }
}
