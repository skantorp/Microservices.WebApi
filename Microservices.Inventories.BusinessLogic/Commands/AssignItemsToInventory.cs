using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microservices.Common.DTOs;
using Microservices.Inventories.DataAccessLayer.Entities;
using Microservices.Inventories.DataAccessLayer.Interfaces.Repositories;

namespace Microservices.Inventories.BusinessLogic.Commands
{
    public class AssignItemsToInventory: CreatedItemsPerInventoryDTO, IRequest<int>
    {
    }

    public class AssignItemsToInventoryHandler : IRequestHandler<AssignItemsToInventory, int>
    {
        private readonly IInventoryRepository _inventoryRepository;

        public AssignItemsToInventoryHandler(IInventoryRepository inventoryRepository, IMapper mapper)
        {
            _inventoryRepository = inventoryRepository;
        }
        public async Task<int> Handle(AssignItemsToInventory request, CancellationToken cancellationToken)
        {
            var inventory = await _inventoryRepository.GetOne(x => x.Id == request.InvertoryId);

            foreach(var item in request.Items)
            {
                inventory.Items.Add(new InventoryItem
                {
                    ItemId = item.ItemId,
                    Quantity = item.Quantity
                });
            }

            await _inventoryRepository.Update(inventory);
            await _inventoryRepository.SaveChanges();

            return request.InvertoryId;
        }
    }
}
