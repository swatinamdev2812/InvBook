using InvBook.Application.Bookings.Queries;
using InvBook.Application.Members.Command;
using InvBook.Application.Members.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvBook.API.Controllers
{
    [Route("api/members")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MemberController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateMemberCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { id, message = "Member created successfully" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteMemberCommand { Id = id });
            if (!result) return NotFound("Member not found");
            return Ok("Member deleted successfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var member = await _mediator.Send(new GetMemberByIdQuery { Id = id });
            if (member == null) return NotFound();
            return Ok(member);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var members = await _mediator.Send(new GetAllMembersQuery());
            return Ok(members);
        }
    }

}
