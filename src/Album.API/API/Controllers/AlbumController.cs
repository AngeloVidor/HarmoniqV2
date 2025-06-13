using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Album.API.Application.Commands;
using Album.API.Application.Queries;
using Album.API.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Album.API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProducerService _producerService;
        private readonly IImageStorageService _imageStorage;

        public AlbumController(IMediator mediator, IProducerService producerService, IImageStorageService imageStorage)
        {
            _mediator = mediator;
            _producerService = producerService;
            _imageStorage = imageStorage;
        }

        [HttpPost("v2/add")]
        public async Task<IActionResult> AddAlbum([FromForm] AddAlbumCommand command)
        {
            if (command == null)
                return BadRequest("Invalid album data.");

            if (!HttpContext.Items.TryGetValue("userId", out var userIdObj) || userIdObj is not Guid userId || userId == Guid.Empty)
                return BadRequest("User is not authenticated.");

            try
            {
                var producer = await _producerService.GetProducerByUserId(userId);

                string url = await _imageStorage.UploadImageAsync(command.image);

                var commandWithProducer = command with { ProducerId = producer.ProducerId, ImageUrl = url };
                var response = await _mediator.Send(commandWithProducer);

                return Ok(new { id = response });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("v2/albums")]
        public async Task<IActionResult> GetAlbums()
        {
            try
            {
                var result = await _mediator.Send(new GetAlbumsQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}