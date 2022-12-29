using MediatR;
using Microservices.Common.DTOs;
using Microservices.Items.BusinessLogic.Commands;
using Microservices.Items.BusinessLogic.Queries;
using Microservices.Items.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Items.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ItemDTO>>> GetAll()
        {
            var items = await _mediator.Send(new GetItems());
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateItem createItem)
        {
            var itemId = await _mediator.Send(createItem);
            return Ok(itemId);
        }

        [HttpPut]
        public async Task<ActionResult<int>> Update(UpdateItem updateItem)
        {
            var itemId = await _mediator.Send(updateItem);
            return Ok(itemId);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> Delete([FromRoute] int id)
        {
            await _mediator.Send(new DeleteItem
            {
                Id = id
            });
            return Ok(id);
        }
    }
}
