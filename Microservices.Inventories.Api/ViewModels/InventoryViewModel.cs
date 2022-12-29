using Microservices.Inventories.BusinessLogic.DTOs;

namespace Microservices.Inventories.Api.ViewModels
{
    public class InventoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ItemViewModel> Items { get; set; } = new List<ItemViewModel>();
    }

    public class ItemViewModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
