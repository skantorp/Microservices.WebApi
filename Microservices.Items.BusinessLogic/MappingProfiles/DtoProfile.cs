using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microservices.Common.DTOs;
using Microservices.Items.DataAccessLayer.Entities;

namespace Microservices.Items.BusinessLogic.MappingProfiles
{
    public class DtoProfile: Profile
    {
        public DtoProfile()
        {
            CreateMap<Item, ItemDTO>();
        }
    }
}
