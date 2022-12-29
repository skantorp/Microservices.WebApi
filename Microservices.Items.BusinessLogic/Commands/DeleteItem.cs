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
    public class DeleteItem: IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteItemHandler : IRequestHandler<DeleteItem, int>
    {
        private readonly IRepository<Item> _itemsRepository;
        public DeleteItemHandler(IRepository<Item> itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }
        public async Task<int> Handle(DeleteItem request, CancellationToken cancellationToken)
        {
            var item = await _itemsRepository.GetOne(x => x.Id == request.Id);

            if (item != null)
            {
                await _itemsRepository.Remove(item);
                await _itemsRepository.SaveChanges();
            }

            return request.Id;
        }
    }
}
