using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Producer.API.Application.Commands;
using Producer.API.Application.Commands.UpdateProducer;
using Producer.API.Application.Queries;
using Producer.API.Domain.Interfaces;

namespace Producer.API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProducerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IGetProducer _getProducer;
        private readonly IImageStorageService _imageStorageService;

        public ProducerController(IMediator mediator, IGetProducer getProducer, IImageStorageService imageStorageService)
        {
            _mediator = mediator;
            _getProducer = getProducer;
            _imageStorageService = imageStorageService;
        }

        [HttpPost("v2/add")]
        [Authorize(Roles = "Producer")]
        public async Task<IActionResult> AddProducer([FromForm] AddProducerCommand command)
        {
            if (command == null)
            {
                return BadRequest("Invalid producer data.");
            }

            Guid userId = HttpContext.Items["userId"] as Guid? ?? Guid.Empty;

            if (userId == Guid.Empty)
            {
                return Unauthorized("User ID is required.");
            }

            string url = await _imageStorageService.UploadImageAsync(command.ImageFile);
            var commandWithUser = command with { UserId = userId, ImageUrl = url };

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

        [HttpPut("v2/update")]
        public async Task<IActionResult> UpdateProducer([FromBody] UpdateProducerCommand command)
        {
            if (command == null)
            {
                return BadRequest("Invalid producer data.");
            }

            var producer = await _getProducer.GetCurrentProducerAsync();
            if (producer == null)
            {
                return Unauthorized("Producer not found.");
            }
            var commandWithUser = command with { ProducerId = producer.Id, UserId = producer.UserId };

            try
            {
                var result = await _mediator.Send(commandWithUser);
                if (result)
                {
                    return Ok();
                }
                return NotFound("Producer not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}