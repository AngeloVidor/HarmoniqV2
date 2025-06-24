using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consumer.API.Application.Commands;
using Consumer.API.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Consumer.API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsumerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConsumerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("v2/add")]
        [Authorize(Roles = "Consumer")]
        public async Task<IActionResult> AddConsumer([FromForm] AddConsumerCommand command)
        {
            if (command == null)
            {
                return BadRequest("Command cannot be null.");
            }

            if (!HttpContext.Items.TryGetValue("userId", out var userIdObj) || userIdObj is not Guid userId || userId == Guid.Empty)
                return BadRequest("User is not authenticated.");

            var commandWithUserId = command with { UserId = userId };

            try
            {
                var result = await _mediator.Send(commandWithUserId);
                if (result)
                {
                    return Ok("Consumer added successfully.");
                }
                return BadRequest("Failed to add consumer.");
            }
            catch (ConsumerAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
    }
}