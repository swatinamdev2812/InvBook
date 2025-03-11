using InvBook.Application.Bookings.Command;
using InvBook.Application.Bookings.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvBook.API.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("book")]
        public async Task<IActionResult> BookItem([FromBody] BookItemCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Contains("successful") ? Ok(result) : BadRequest(result);
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> CancelBooking([FromBody] CancelBookingCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Contains("cancelled") ? Ok(result) : NotFound(result);
        }

        [HttpGet("by-date")]
        public async Task<IActionResult> GetBookingsByDate([FromQuery] DateTime? date)
        {
            var bookings = await _mediator.Send(new GetBookingsByDateQuery(date));
            return Ok(bookings);
        }

        [HttpGet("member/{memberId}")]
        public async Task<IActionResult> GetBookingsByMemberId(int memberId)
        {
            var result = await _mediator.Send(new GetBookingsByMemberIdQuery(memberId));
            return Ok(result);
        }
    }
}