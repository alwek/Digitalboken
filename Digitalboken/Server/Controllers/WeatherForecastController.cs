using Digitalboken.Server.Interfaces;
using Digitalboken.Server.Models;
using Digitalboken.Server.Models.Search;
using Microsoft.AspNetCore.Mvc;

namespace Digitalboken.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IInstructionRepository _instructionRepository;
        private readonly IRedisCacheService _redisCacheService;
        private readonly ISearchRepository _searchRepository;
        private readonly IGoogleSearchService _googleSearchService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IInstructionRepository repository,
            ISearchRepository searchRepository,
            IRedisCacheService redisService,
            IGoogleSearchService googleSearchService)
        {
            _logger = logger;
            _redisCacheService = redisService;
            _searchRepository = searchRepository;
            _instructionRepository = repository;
            _googleSearchService = googleSearchService;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            _logger.LogInformation("Recieved GET request");
            Instruction instruction = new() { Text = "alican" };
            Search searchResult = await _googleSearchService.Search("alican");
            await _searchRepository.InsertAsync(searchResult);
            await _redisCacheService.InsertAsync("query", "alican");
            await _instructionRepository.InsertAsync(instruction);
            
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}