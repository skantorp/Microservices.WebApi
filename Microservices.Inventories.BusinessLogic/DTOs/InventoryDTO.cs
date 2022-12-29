using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservices.Common.DTOs;

namespace Microservices.Inventories.BusinessLogic.DTOs
{
    public class InventoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<InventoryItemDTO> Items { get; set; } = new List<InventoryItemDTO>();
    }
}
