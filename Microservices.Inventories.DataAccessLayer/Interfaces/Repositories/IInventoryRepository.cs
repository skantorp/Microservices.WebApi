using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservices.Common.Interfaces;
using Microservices.Inventories.DataAccessLayer.Entities;

namespace Microservices.Inventories.DataAccessLayer.Interfaces.Repositories
{
    public interface IInventoryRepository : IRepository<Inventory>
    {
        Task RemoveItem(InventoryItem inventoryItem);
    }
}
