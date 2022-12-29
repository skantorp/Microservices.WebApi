using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Common.DTOs
{
    public class CreatedItemsPerInventoryDTO
    {
        public int InvertoryId { get; set; }
        public List<InventoryItemDTO> Items { get; set; } = new List<InventoryItemDTO>();
    }
}
