using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microservices.Common.DTOs;
using Microservices.Inventories.BusinessLogic.DTOs;
using Microservices.Inventories.DataAccessLayer.Entities;

namespace Microservices.Inventories.BusinessLogic.MappingProfiles
{
    public class DtoProfile: Profile
    {
        public DtoProfile()
        {
            CreateMap<InventoryItem, InventoryItemDTO>();
            CreateMap<Inventory, InventoryDTO>();
        }
    }
}
