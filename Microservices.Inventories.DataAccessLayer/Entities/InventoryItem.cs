using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Inventories.DataAccessLayer.Entities
{
    public class InventoryItem
    {
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
