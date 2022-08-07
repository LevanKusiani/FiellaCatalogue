using Catalogue.Application.Commands.ItemAggregate;
using Catalogue.Application.Queries.Common;
using Catalogue.Application.Queries.ItemQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Catalogue.API.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IItemQueries _itemQueries;

        public ItemsController(IMediator mediator,
            IItemQueries itemQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _itemQueries = itemQueries ?? throw new ArgumentNullException(nameof(IItemQueries));
        }

        [HttpGet("test")]
        public IActionResult TestMethod()
        {
            return Ok("testing");
        }

        [HttpGet]
        public async Task<IActionResult> Items([Required] string sortField,
            [Required] SortOrder sortOrder,
            int? skip,
            int? take,
            string? itemName)
        {
            return Ok(await _itemQueries.GetItemsAsync(sortField, sortOrder, skip, take, itemName));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> ItemDetails([Required] Guid id)
        {
            return Ok(await _itemQueries.GetItemByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] CreateItemCommand command)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var commandResult = await _mediator.Send(command);

            if (!commandResult)
                return BadRequest();

            return Ok();
        }
    }
}