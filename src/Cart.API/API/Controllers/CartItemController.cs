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
    public class CartItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("v2/add")]
        public async Task<IActionResult> AddToCart(AddCartItemCommand command)
        {
            if (command == null)
                return BadRequest();

            if (!HttpContext.Items.TryGetValue("userId", out var userIdObj) || userIdObj is not Guid userId || userId == Guid.Empty)
                return BadRequest("User is not authenticated.");

            var cmdWithUser = command with { UserId = userId };

            try
            {
                var result = await _mediator.Send(cmdWithUser);
                return Ok(result);
            }
            catch (ConsumerNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (CartNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InactiveCartException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
