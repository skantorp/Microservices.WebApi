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
    public class GetInventoryById: IRequest<InventoryDTO>
    {
        public int Id { get; set; }
    }

    public class GetInventoryByIdHandler : IRequestHandler<GetInventoryById, InventoryDTO>
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;

        public GetInventoryByIdHandler(IInventoryRepository inventoryRepository, IMapper mapper)
        {
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
        }

        public async Task<InventoryDTO> Handle(GetInventoryById request, CancellationToken cancellationToken)
        {
            var inventory = await _inventoryRepository
                .GetOne(x => x.Id == request.Id);

            return _mapper.Map<InventoryDTO>(inventory);
        }
    }
}
