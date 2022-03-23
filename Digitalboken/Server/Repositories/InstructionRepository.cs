using Digitalboken.Server.Interfaces;
using Digitalboken.Server.Models;
using MongoDB.Driver;

namespace Digitalboken.Server.Repositories
{
    public class InstructionRepository : IInstructionRepository
    {
        private readonly IMongoCollection<Instruction> _collection;
        private readonly ILogger<InstructionRepository> _logger;

        public InstructionRepository(IMongoDatabase database, ILogger<InstructionRepository> logger)
        {
            _collection = database.GetCollection<Instruction>(nameof(Instruction));
            _logger = logger;
        }

        public async Task InsertAsync(Instruction data)
        {
            try
            {
                await _collection.InsertOneAsync(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task<Instruction> GetAsync(Guid guid)
        {
            return await _collection.Find(x => x.Id == guid).FirstOrDefaultAsync();
        }
    }
}
