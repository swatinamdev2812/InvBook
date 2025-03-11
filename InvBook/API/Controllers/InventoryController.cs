using InvBook.Application.Common.DTOs;
using InvBook.Application.Inventory.Commands;
using InvBook.Application.Inventory.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvBook.API.Controllers
{
    [Route("api/Inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InventoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<InventoryItemDTO>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllInventoryItemsQuery());
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateInventoryItemDTO dto)
        {
            var command = new CreateInventoryItemCommand
            {
                Title = dto.Title,
                Description = dto.Description,
                RemainingCount = dto.RemainingCount,
                ExpirationDate = dto.ExpirationDate
            };

            var itemId = await _mediator.Send(command);

            return Ok(new { itemId, message = "Item created successfully" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _mediator.Send(new DeleteInventoryItemCommand { Id = id });
            if (!success)
                return NotFound(new { message = "Item not found or already deleted" });

            return Ok(new { message = "Item deleted successfully" });
        }

        [HttpDelete("deleteall")]
        public async Task<IActionResult> DeleteAll()
        {
            await _mediator.Send(new DeleteAllInventoryItemsCommand());
            return Ok(new { message = "All items deleted successfully" });
        }
    }
}
