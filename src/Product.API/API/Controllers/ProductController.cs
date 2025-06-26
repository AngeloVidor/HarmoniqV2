using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.API.Application.Queries;
using Product.API.Domain.Exceptions;

namespace Product.API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("v2/id")]
        public async Task<IActionResult> GetProductById([FromQuery]GetProductByIdQuery query)
        {
            if (query == null)
                return BadRequest("Query cannot be null");

            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}