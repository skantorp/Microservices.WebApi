using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microservices.Common.Interfaces;
using Microservices.Inventories.BusinessLogic.DTOs;
using Microservices.Inventories.DataAccessLayer.Entities;
using Microservices.Inventories.DataAccessLayer.Interfaces.Repositories;

namespace Microservices.Inventories.BusinessLogic.Queries
{
    public class GetInventories: IRequest<List<InventoryDTO>>
    {
    }

    public class GetInventoriesHandler : IRequestHandler<GetInventories, List<InventoryDTO>>
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;

        public GetInventoriesHandler(IInventoryRepository inventoryRepository, IMapper mapper)
        {
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
        }

        public async Task<List<InventoryDTO>> Handle(GetInventories request, CancellationToken cancellationToken)
        {
            var inventories = await _inventoryRepository
                .GetAll();

            return _mapper.Map<List<InventoryDTO>>(inventories);
        }
    }
}
