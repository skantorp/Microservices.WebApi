using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservices.Items.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Items.DataAccessLayer
{
    public static class InitialDataSeed
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Item>().HasData(
                new Item
                {
                    Id = 1,
                    Name = "Knife",
                    Weight = 0.2
                },
                new Item
                {
                    Id = 2,
                    Name = "Scissors",
                    Weight = 0.1
                }
            );
        }
    }
}
