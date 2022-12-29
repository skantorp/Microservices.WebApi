using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Items.DataAccessLayer.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
    }
}
