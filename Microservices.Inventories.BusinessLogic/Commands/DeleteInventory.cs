using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microservices.Common.Interfaces;
using Microservices.Inventories.DataAccessLayer.Entities;
using Microservices.Inventories.DataAccessLayer.Interfaces.Repositories;

namespace Microservices.Inventories.BusinessLogic.Commands
{
    public class DeleteInventory: IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteInventoryHandler : IRequestHandler<DeleteInventory, int>
    {
        private readonly IInventoryRepository _inventoryRepository;

        public DeleteInventoryHandler(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<int> Handle(DeleteInventory request, CancellationToken cancellationToken)
        {
            var inventory = await _inventoryRepository.GetOne(x => x.Id == request.Id);

            if (inventory != null)
            {
                await _inventoryRepository.Remove(inventory);
            }

            return request.Id;
        }
    }
}
