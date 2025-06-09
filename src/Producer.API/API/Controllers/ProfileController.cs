using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Producer.API.Domain.Interfaces;

namespace Producer.API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IGetProducer _getProducer;

        public ProfileController(IGetProducer getProducer)
        {
            _getProducer = getProducer;
        }

        [HttpGet("v2/profile")]
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
    }
}