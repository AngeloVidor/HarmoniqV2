using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Music.API.Application.Commands.SingleMusic;
using Music.API.Domain.Exceptions;

namespace Music.API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SingleMusicController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SingleMusicController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("v2/add")]
        public async Task<IActionResult> AddSingleMusic([FromForm] AddSingleMusicCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Guid userId = HttpContext.Items["userId"] as Guid? ?? Guid.Empty;

            if (userId == Guid.Empty)
            {
                return Unauthorized("User ID is required.");
            }

            var commandWithUser = command with { UserId = userId };

            try
            {
                var musicId = await _mediator.Send(commandWithUser);
                return Created("", new { id = musicId });
            }
            catch (ProducerNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
    }

}