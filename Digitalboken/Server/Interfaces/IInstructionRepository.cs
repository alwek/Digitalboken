using Digitalboken.Server.Models;

namespace Digitalboken.Server.Interfaces
{
    public interface IInstructionRepository
    {
        public Task InsertAsync(Instruction instruction);
        public Task<Instruction> GetAsync(Guid guid);
    }
}
