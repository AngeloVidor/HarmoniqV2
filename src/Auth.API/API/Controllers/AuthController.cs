using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.API.Application.Commands;
using Auth.API.Application.Commands.Login;
using Auth.API.Application.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IJwtConfiguration _jwtConfiguration;
        public AuthController(IMediator mediator, IJwtConfiguration jwtConfiguration)
        {
            _mediator = mediator;
            _jwtConfiguration = jwtConfiguration;
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

        [HttpPost("v2/login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            if (command == null)
            {
                return BadRequest("Invalid login data.");
            }

            try
            {
                var isValid = await _mediator.Send(command);
                if (!isValid)
                {
                    return Unauthorized("Invalid email or password.");
                }
                var token = await _jwtConfiguration.GenerateTokenAsync(command.Email);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}