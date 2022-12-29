using Microservices.Common.DTOs;

namespace Microservices.Inventories.Api.Services.Interfaces
{
    public interface IItemsHttpClient
    {
        Task<List<ItemDTO>> GetAllItems();
    }
}
