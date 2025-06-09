using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Producer.API.Application.Queries;

namespace Producer.API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProducersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProducersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("v2/all")]
        public async Task<ActionResult<IEnumerable<Domain.Aggregates.Producer>>> GetAll()
        {
            var result = await _mediator.Send(new GetProducersQuery());

            if (result == null || !result.Any())
                return NoContent();

            return Ok(result);
        }
    }
}