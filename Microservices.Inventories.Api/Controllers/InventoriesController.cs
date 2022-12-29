using MediatR;
using Microservices.Common.Interfaces;
using Microservices.Inventories.Api.Services;
using Microservices.Inventories.Api.Services.Interfaces;
using Microservices.Inventories.BusinessLogic.Commands;
using Microservices.Inventories.BusinessLogic.DTOs;
using Microservices.Inventories.BusinessLogic.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Inventories.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPublishService _publishService;
        private readonly IItemsHttpClient _itemsHttpClient;

        public InventoriesController(IMediator mediator, IPublishService publishService, IItemsHttpClient itemsHttpClient)
        {
            _mediator = mediator;
            _publishService = publishService;
            _itemsHttpClient = itemsHttpClient;
        }

        [HttpGet]
        public async Task<ActionResult<List<InventoryDTO>>> GetAll()
        {
            var items = await _itemsHttpClient.GetAllItems();
            var inventories = await _mediator.Send(new GetInventories());
            return Ok(inventories);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateInventory createInventory)
        {
            var inventoryId = await _mediator.Send(createInventory);
            return Ok(inventoryId);
        }

        [HttpPut]
        public async Task<ActionResult<int>> Update(UpdateInventory updateInventory)
        {
            var inventoryId = await _mediator.Send(updateInventory);
            return Ok(inventoryId);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> Delete([FromRoute]int id)
        {
            await _mediator.Send(new DeleteInventory
            {
                Id = id
            });
            return Ok(id);
        }
    }
}
