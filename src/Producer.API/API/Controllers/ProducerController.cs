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

        public ProducerController(IMediator mediator, IGetProducer getProducer)
        {
            _mediator = mediator;
            _getProducer = getProducer;
        }

        [HttpPost("v2/add")]
        [Authorize(Roles = "Producer")]
        public async Task<IActionResult> AddProducer([FromBody] AddProducerCommand command)
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
            var commandWithUser = command with { UserId = userId };

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

        [HttpGet("v2/get")]
        public async Task<IActionResult> GetProducer()
        {
            try
            {
                var producer = await _getProducer.GetCurrentProducerAsync();
                if (producer == null)
                {
                    return NotFound("Producer not found.");
                }
                return Ok(producer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("v2/getall")]
        public async Task<ActionResult<IEnumerable<Domain.Aggregates.Producer>>> GetAll()
        {
            var result = await _mediator.Send(new GetProducersQuery());

            if (result == null || !result.Any())
                return NoContent();

            return Ok(result);
        }
    }
}