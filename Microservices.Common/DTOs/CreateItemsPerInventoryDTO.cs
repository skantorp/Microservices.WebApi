using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Common.DTOs
{
    public class CreateItemsPerInventoryDTO
    {
        public int InvertoryId { get; set; }
        public List<UpdateItemDTO> Items { get; set; } = new List<UpdateItemDTO>();
    }
}
