using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microservices.Common.DTOs;
using Microservices.Common.Infrastructure.MessageBrocker;
using Microservices.Common.Interfaces;
using Microservices.Inventories.BusinessLogic.DTOs;
using Microservices.Inventories.DataAccessLayer.Entities;
using Microservices.Inventories.DataAccessLayer.Interfaces.Repositories;

namespace Microservices.Inventories.BusinessLogic.Commands
{
    public class UpdateInventory : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UpdateItemDTO> Items { get; set; } = new List<UpdateItemDTO>();
    }

    public class UpdateInventoryHandler : IRequestHandler<UpdateInventory, int>
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IPublishService _publishService;

        public UpdateInventoryHandler(IInventoryRepository inventoryRepository, IPublishService publishService)
        {
            _inventoryRepository = inventoryRepository;
            _publishService = publishService;
        }

        public async Task<int> Handle(UpdateInventory request, CancellationToken cancellationToken)
        {
            var inventory = await _inventoryRepository.GetOne(x => x.Id == request.Id);

            inventory.Name = request.Name;
            var itemsToAdd = request.Items.Where(x => x.ItemId != null).ExceptBy(inventory.Items.Select(e => e.ItemId), x => x.ItemId ?? 0);
            var itemsToDelete = inventory.Items.ExceptBy(request.Items.Select(e => e.ItemId), x => x.ItemId);
            var itemsToUpdate = inventory.Items.IntersectBy(request.Items.Select(e => e.ItemId), x => x.ItemId).ToList();

            foreach (var item in itemsToAdd)
            {
                inventory.Items.Add(new InventoryItem
                {
                    ItemId = item.ItemId.Value,
                    Quantity = item.Quantity
                });
            }

            foreach (var item in itemsToUpdate)
            {
                var itemDTO = request.Items.FirstOrDefault(x => x.ItemId == item.ItemId);

                if (itemDTO != null)
                {
                    item.Quantity = itemDTO.Quantity;
                }
            }

            foreach (var item in itemsToDelete)
            {
                await _inventoryRepository.RemoveItem(item);
            }

            var newItems = request.Items.Where(x => !x.ItemId.HasValue).ToList();

            await _inventoryRepository.Update(inventory);
            await _inventoryRepository.SaveChanges();

            var itemsToAddMessage = new CreateItemsPerInventoryDTO
            {
                InvertoryId = inventory.Id,
                Items = newItems.ToList()
            };

            _publishService.SendMessage(itemsToAddMessage, "itemsToAdd");

            return inventory.Id;
        }
    }
}
