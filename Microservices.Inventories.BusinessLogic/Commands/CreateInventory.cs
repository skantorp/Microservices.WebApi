using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microservices.Common.DTOs;
using Microservices.Common.Interfaces;
using Microservices.Inventories.BusinessLogic.DTOs;
using Microservices.Inventories.BusinessLogic.Queries;
using Microservices.Inventories.DataAccessLayer.Entities;
using Microservices.Inventories.DataAccessLayer.Interfaces.Repositories;

namespace Microservices.Inventories.BusinessLogic.Commands
{
    public class CreateInventory: IRequest<int>
    {
        public string Name { get; set; }
        public List<UpdateItemDTO> Items { get; set; } = new List<UpdateItemDTO>();
    }

    public class CreateInventoryHandler : IRequestHandler<CreateInventory, int>
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IPublishService _publishService;

        public CreateInventoryHandler(IInventoryRepository inventoryRepository, IPublishService publishService)
        {
            _inventoryRepository = inventoryRepository;
            _publishService = publishService;
        }

        public async Task<int> Handle(CreateInventory request, CancellationToken cancellationToken)
        {
            var newInventory = new Inventory
            {
                Name = request.Name
            };

            var itemsToAdd = request.Items.Where(x => !x.Id.HasValue).ToList();
            var itemsToUpdate = request.Items.Where(x => x.Id.HasValue).ToList();

            foreach (var item in itemsToUpdate)
            {
                newInventory.Items.Add(new InventoryItem
                {
                    ItemId = item.ItemId.Value,
                    Quantity = item.Quantity
                });
            }

            await _inventoryRepository.Add(newInventory);
            await _inventoryRepository.SaveChanges();

            var itemsToAddMessage = new CreateItemsPerInventoryDTO
            {
                InvertoryId = newInventory.Id,
                Items = itemsToAdd.ToList()
            };

            _publishService.SendMessage(itemsToAddMessage, "itemsToAdd");

            return newInventory.Id;
        }
    }
}
