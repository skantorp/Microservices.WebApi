using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Common.DTOs
{
    public class UpdateItemDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? ItemId { get; set; }
        public int Quantity { get; set; }
        public double? Weight { get; set; }
    }
}
