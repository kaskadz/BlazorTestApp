using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using BlazorTestApp.Frontend.Domain;

namespace BlazorTestApp.Frontend.Services
{
    public class TokenExtractionService : ITokenExtractionService
    {
        private static readonly Regex TokenRegex =
            new(@"login\.microsoftonline\.com-(accesstoken|refreshtoken|idtoken)-");

        private readonly ISessionStorageService _sessionStorageService;

        public TokenExtractionService(ISessionStorageService sessionStorageService)
        {
            _sessionStorageService = sessionStorageService;
        }

        public async Task<IEnumerable<TokenDisplayData>> GetTokensAsync()
        {
            var keysWithTokenTypes = await GetRelevantKeysAsync();

            return await Task.WhenAll(keysWithTokenTypes.Select(async key =>
            {
                var dictionary = await _sessionStorageService.GetItemAsync<IDictionary<string, string>>(key);
                string tokenType = dictionary["credentialType"];
                string token = dictionary["secret"];
                string target = tokenType switch
                {
                    "AccessToken" => dictionary["target"],
                    _ => null
                };

                return new TokenDisplayData
                {
                    TokenType = tokenType,
                    Token = token,
                    Target = target
                };
            }));
        }

        private async Task<IEnumerable<string>> GetRelevantKeysAsync()
        {
            IEnumerable<string> allKeys = await GetAllKeysAsync();
            return allKeys.Where(key => TokenRegex.Match(key).Success);
        }

        private async Task<IEnumerable<string>> GetAllKeysAsync()
        {
            int count = await _sessionStorageService.LengthAsync();
            return await Task.WhenAll(Enumerable.Range(0, count)
                .Select(x => _sessionStorageService.KeyAsync(x).AsTask()));
        }
    }
}