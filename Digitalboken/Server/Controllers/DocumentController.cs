using Digitalboken.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Digitalboken.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : Controller
    {
        private readonly ILogger<DocumentController> _logger;

        public DocumentController(ILogger<DocumentController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetDocumentAsync(string guid)
        {
            _logger.LogInformation("Got document request! " + guid);

            if (guid != null)
                return Ok("Hejsvejs");
            else
                return Created("Nysökning", null);
        }
    }
}
