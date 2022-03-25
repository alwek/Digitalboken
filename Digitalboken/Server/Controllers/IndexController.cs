using Microsoft.AspNetCore.Mvc;

namespace Digitalboken.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IndexController : ControllerBase
    {
        private readonly ILogger<IndexController> _logger;

        public IndexController(ILogger<IndexController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{query}")]
        public async Task<IActionResult> Search(string query)
        {
            _logger.LogInformation("Got search request! " + query);

            if (query != null)
                return Created(string.Empty, query);
            else
                return Ok(query);
        }
    }
}
