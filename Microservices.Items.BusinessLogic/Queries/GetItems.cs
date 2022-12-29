using AutoMapper;
using MediatR;
using Microservices.Common.DTOs;
using Microservices.Common.Interfaces;
using Microservices.Items.DataAccessLayer.Entities;

namespace Microservices.Items.BusinessLogic.Queries
{
    public class GetItems: IRequest<List<ItemDTO>>
    {
    }

    public class GetItemsHandler : IRequestHandler<GetItems, List<ItemDTO>>
    {
        private readonly IRepository<Item> _itemsRepository;
        private readonly IMapper _mapper;

        public GetItemsHandler(IRepository<Item> itemsRepository, IMapper mapper)
        {
            _itemsRepository = itemsRepository;
            _mapper = mapper;
        }

        public async Task<List<ItemDTO>> Handle(GetItems request, CancellationToken cancellationToken)
        {
            var items = await _itemsRepository
                 .GetAll();

            return _mapper.Map<List<ItemDTO>>(items);
        }
    }
}
