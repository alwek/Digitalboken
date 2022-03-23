using Digitalboken.Server.Models.Search;

namespace Digitalboken.Server.Interfaces
{
    public interface ISearchRepository
    {
        public Task InsertAsync(Search data);
    }
}
