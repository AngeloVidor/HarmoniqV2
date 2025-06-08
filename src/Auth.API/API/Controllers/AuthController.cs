using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.API.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("v2/register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            if (command == null)
            {
                return BadRequest("Invalid registration data.");
            }

            try
            {
                var userId = await _mediator.Send(command);
                return CreatedAtAction(nameof(Register), new { id = userId }, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}