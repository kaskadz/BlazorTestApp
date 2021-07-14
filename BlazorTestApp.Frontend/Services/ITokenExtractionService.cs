using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorTestApp.Frontend.Domain;

namespace BlazorTestApp.Frontend.Services
{
    public interface ITokenExtractionService
    {
        public Task<IEnumerable<TokenDisplayData>> GetTokensAsync();
    }
}