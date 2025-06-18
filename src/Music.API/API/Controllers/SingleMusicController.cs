using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Music.API.Application.Commands.SingleMusic;
using Music.API.Application.Queries.SingleMusic;
using Music.API.Domain.Exceptions;
using Music.API.Domain.Interfaces;

namespace Music.API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SingleMusicController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IImageUploaderService _imageUploader;

        public SingleMusicController(IMediator mediator, IImageUploaderService imageUploader)
        {
            _mediator = mediator;
            _imageUploader = imageUploader;
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

            string url = await _imageUploader.UploadAsync(command.Image);

            var commandWithUser = command with { UserId = userId, ImageUrl = url };

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

        [HttpGet("v2/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSingleMusicById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Music ID cannot be empty.");
            }

            try
            {
                var singleMusic = await _mediator.Send(new GetSingleMusicByIdQuery(id));
                return Ok(singleMusic);
            }
            catch (MusicNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}

