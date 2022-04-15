using Digitalboken.Server.Interfaces;
using Digitalboken.Server.Models;
using Digitalboken.Server.Models.Search;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Digitalboken.Server.Controllers
{
    [ApiController]
    [Route("api")]
    public class IndexController : ControllerBase
    {
        private readonly ILogger<IndexController> _logger;
        private readonly IDocumentRepository _documentRepository;
        private readonly IRedisCacheService _redisCacheService;
        private readonly ISearchRepository _searchRepository;
        private readonly IGoogleSearchService _googleSearchService;

        public IndexController(ILogger<IndexController> logger,
            IDocumentRepository documentRepository,
            IRedisCacheService redisCacheService,
            ISearchRepository searchRepository,
            IGoogleSearchService googleSearchService)
        {
            _logger = logger;
            _documentRepository = documentRepository;
            _redisCacheService = redisCacheService;
            _searchRepository = searchRepository;
            _googleSearchService = googleSearchService;
        }

        [HttpGet("{query}")]
        public async Task<IActionResult> Search(string query)
        {
            try
            {
                Document doc = await FindDocumentInCacheAsync(query);
                Search srch = await FindSearchInCacheAsync(query);

                if (doc != null)
                    return Ok(doc.Id);
                else if(srch != null)
                    return Created("", srch.Id);

                doc = await FindDocumentInCollectionAsync(query);

                if (doc != null)
                    return Ok(doc.Id);

                srch = await SearchForQueryWithGoogle(query);

                if (srch != null)
                    return Created("", srch.Id);
                else
                    return NotFound(query);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong when processing search request");
                return StatusCode(500, "Something went wrong when processing search request");
            }
        }

        private async Task<Document> FindDocumentInCacheAsync(string key)
        {
            try
            {
                string cached = await _redisCacheService.GetAsync("document" + key);

                if (cached != null)
                {
                    await _redisCacheService.RefreshAsync(key);
                    return JsonConvert.DeserializeObject<Document>(cached);
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when searching for document in cache.");
                return null;
            }
        }

        private async Task<Search> FindSearchInCacheAsync(string key)
        {
            try
            {
                string cached = await _redisCacheService.GetAsync("search" + key);

                if (cached != null)
                {
                    await _redisCacheService.RefreshAsync(key);
                    return JsonConvert.DeserializeObject<Search>(cached);
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when searching for search in cache.");
                return null;
            }
        }

        private async Task<Document> FindDocumentInCollectionAsync(string name)
        {
            try
            {
                Document doc = await _documentRepository.GetByNameAsync(name);

                if (doc != null)
                {
                    string json = JsonConvert.SerializeObject(doc);
                    await _redisCacheService.InsertAsync("document" + doc.Id, json);
                    await _redisCacheService.InsertAsync("document" + doc.Name, json);
                }

                return doc;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when searching for document in database.");
                return null;
            }
        }

        private async Task<Search> SearchForQueryWithGoogle(string query)
        {
            try
            {
                Search search = await _searchRepository.GetBySearchTermAsync(query);

                if (search == null)
                {
                    search = await _googleSearchService.Search(query);

                    if(search != null)
                        await _searchRepository.InsertAsync(search);
                    else 
                        return null;
                }
                
                await _redisCacheService.InsertAsync("search" + search.Id, JsonConvert.SerializeObject(search));
                await _redisCacheService.InsertAsync("search" + query, JsonConvert.SerializeObject(search));
                
                return search;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when searching for query in google.");
                return null;
            }
        }
    }
}
