using Digitalboken.Server.Interfaces;
using Digitalboken.Server.Models.Search;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Digitalboken.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly IRedisCacheService _redisCacheService;
        private readonly ISearchRepository _searchRepository;

        public SearchController(ILogger<SearchController> logger,
            IRedisCacheService redisCacheService,
            ISearchRepository searchRepository)
        {
            _logger = logger;
            _redisCacheService = redisCacheService;
            _searchRepository = searchRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Search search)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(search);

                var success = await _searchRepository.InsertAsync(search);
                if (success)
                    return Ok(search.Id);
                else
                    return UnprocessableEntity(search.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong when processing search request");
                return StatusCode(500, "Something went wrong when processing search request");
            }
        }

       [HttpGet("{guid}")]
        public async Task<IActionResult> Search(string guid)
        {
            try
            {
               Search srch = await FindSearchInCacheAsync(guid);
                
                if(srch != null)
                    return Ok(srch);

                srch = await FindSearchInCollectionAsync(guid);

                if (srch != null)
                    return Ok(srch);
                else
                    return NotFound(guid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong when processing search request");
                return StatusCode(500, "Something went wrong when processing search request");
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

        private async Task<Search> FindSearchInCollectionAsync(string name)
        {
            try
            {
                Search srch = await _searchRepository.GetBySearchTermAsync(name);

                if(srch != null)
                {
                    string json = JsonConvert.SerializeObject(srch);
                    await _redisCacheService.InsertAsync("search" + srch.Id, json);
                    return srch;
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when searching for search in database.");
                return null;
            }
        }
    }
}
