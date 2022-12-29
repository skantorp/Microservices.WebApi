using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microservices.Common.DTOs;
using Microservices.Common.Interfaces;
using Microservices.Items.DataAccessLayer.Entities;

namespace Microservices.Items.BusinessLogic.Commands
{
    public class CreateItemsList: CreateItemsPerInventoryDTO, IRequest<CreatedItemsPerInventoryDTO>
    {
    }

    public class CreateItemsListHandler : IRequestHandler<CreateItemsList, CreatedItemsPerInventoryDTO>
    {
        private readonly IRepository<Item> _itemsRepository;
        private readonly IPublishService _publishService;

        public CreateItemsListHandler(IRepository<Item> itemsRepository, IPublishService publishService)
        {
            _itemsRepository = itemsRepository;
            _publishService = publishService;
        }

        public async Task<CreatedItemsPerInventoryDTO> Handle(CreateItemsList request, CancellationToken cancellationToken)
        {
            var newItems = request.Items.Select(x => new Item
            {
                Name = x.Name,
                Weight = x.Weight.Value
            }).ToList();

            foreach(var item in newItems)
            {
                await _itemsRepository.Add(item);
            }
            await _itemsRepository.SaveChanges();

            var addedItems =
                from item in newItems
                join reqItem in request.Items on item.Name equals reqItem.Name
                select new InventoryItemDTO
                {
                    ItemId = item.Id,
                    Quantity = reqItem.Quantity
                };

            var addedItemsResult = new CreatedItemsPerInventoryDTO
            {
                InvertoryId = request.InvertoryId,
                Items = addedItems.ToList()
            };
            _publishService.SendMessage(addedItemsResult, "addedItems");

            return addedItemsResult;
        }

    }
}
