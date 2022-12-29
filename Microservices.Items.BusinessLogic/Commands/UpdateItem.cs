using MediatR;
using Microservices.Common.Interfaces;
using Microservices.Items.DataAccessLayer.Entities;

namespace Microservices.Items.BusinessLogic.Commands
{
    public class UpdateItem : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
    }

    public class UpdateItemHandler : IRequestHandler<UpdateItem, int>
    {
        private readonly IRepository<Item> _itemsRepository;

        public UpdateItemHandler(IRepository<Item> itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }
        public async Task<int> Handle(UpdateItem request, CancellationToken cancellationToken)
        {
            var item = await _itemsRepository.GetOne(x => x.Id == request.Id);

            if (item != null)
            {
                item.Name = request.Name;
                item.Weight = request.Weight;

                await _itemsRepository.Update(item);
                await _itemsRepository.SaveChanges();
            }

            return request.Id;
        }
    }
}
