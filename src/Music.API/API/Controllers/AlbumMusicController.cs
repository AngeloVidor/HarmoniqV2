using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Music.API.Application.Commands;
using Music.API.Application.Queries;
using Music.API.Domain.Exceptions;
using Music.API.Domain.Interfaces;

namespace Music.API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumMusicController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IImageUploaderService _imageUploader;

        public AlbumMusicController(IMediator mediator, IImageUploaderService imageUploader)
        {
            _mediator = mediator;
            _imageUploader = imageUploader;
        }

        [HttpPost("v2/add")]
        public async Task<IActionResult> AddMusic([FromForm] AddMusicCommand command)
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
            catch (AlbumNotFoundException ex)
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