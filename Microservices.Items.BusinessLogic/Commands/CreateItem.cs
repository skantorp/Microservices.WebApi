using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microservices.Common.Interfaces;
using Microservices.Items.DataAccessLayer.Entities;

namespace Microservices.Items.BusinessLogic.Commands
{
    public class CreateItem: IRequest<int>
    {
        public string Name { get; set; }
        public double Weight { get; set; }
    }

    public class CreateItemHandler: IRequestHandler<CreateItem, int>
    {
        private readonly IRepository<Item> _itemsRepository;

        public CreateItemHandler(IRepository<Item> itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        public async Task<int> Handle(CreateItem request, CancellationToken cancellationToken)
        {
            var newItem = new Item
            {
                Name = request.Name,
                Weight = request.Weight
            };

            await _itemsRepository.Add(newItem);
            await _itemsRepository.SaveChanges();

            return newItem.Id;
        }
    }
}
