using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cart.API.Application.Commands;
using Cart.API.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cart.API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("v2/add")]
        public async Task<IActionResult> CreateCart([FromBody] AddCartCommand command)
        {
            if (command == null)
                return BadRequest("Invalid cart data");

            if (!HttpContext.Items.TryGetValue("userId", out var userIdObj) || userIdObj is not Guid userId || userId == Guid.Empty)
                return BadRequest("User is not authenticated.");

            System.Console.WriteLine(userId);


            try
            {
                var commandWithConsumer = command with { UserId = userId };

                var response = await _mediator.Send(commandWithConsumer);
                return Ok(response);
            }
            catch (ConsumerNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (CannotCreateCartWhenAlreadyActiveException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
}