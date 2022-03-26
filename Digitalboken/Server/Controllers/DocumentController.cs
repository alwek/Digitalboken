using Digitalboken.Server.Interfaces;
using Digitalboken.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Digitalboken.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly ILogger<DocumentController> _logger;
        private readonly IDocumentRepository _documentRepository;
        private readonly IRedisCacheService _redisCacheService;

        public DocumentController(ILogger<DocumentController> logger,
            IDocumentRepository documentRepository,
            IRedisCacheService redisCacheService)
        {
            _logger = logger;
            _documentRepository = documentRepository;
            _redisCacheService = redisCacheService;
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> Get(string guid)
        {
            try
            {
                Document document = await GetDocumentFromCache(guid);

                if(document == null)
                    document = await _documentRepository.GetAsync(guid);

                if(document != null)
                    return Ok(guid);
                else
                    return NotFound(document);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong when processing document request");
                return StatusCode(500, "Something went wrong when processing document request");
            }
        }

        private async Task<Document> GetDocumentFromCache(string key)
        {
            try
            {
                string cached = await _redisCacheService.GetAsync(key);

                if (cached != null)
                    return JsonConvert.DeserializeObject<Document>(cached);
                else
                    return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when retrieving for document in cache.");
                return null;
            }
        }
    }
}
