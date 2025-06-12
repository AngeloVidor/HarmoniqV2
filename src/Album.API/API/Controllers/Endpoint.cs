using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Album.API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Endpoint : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            Guid userId = HttpContext.Items["userId"] as Guid? ?? Guid.Empty;
            return Ok(userId);
        }
    }
}