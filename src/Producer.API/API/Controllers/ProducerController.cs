using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Producer.API.Application.Commands;

namespace Producer.API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProducerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProducerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("v2/add")]
        [Authorize(Roles = "Producer")]
        public async Task<IActionResult> AddProducer([FromBody] AddProducerCommand command)
        {
            if (command == null)
            {
                return BadRequest("Invalid producer data.");
            }

            Guid userId = HttpContext.Items["userId"] as Guid? ?? Guid.Empty;
            Console.WriteLine($"User ID from context: {userId}");

            if (userId == Guid.Empty)
            {
                return Unauthorized("User ID is required.");
            }
            var commandWithUser = command with { UserId = userId };

            try
            {
                var producerId = await _mediator.Send(commandWithUser);
                return CreatedAtAction(nameof(AddProducer), new { id = producerId }, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}