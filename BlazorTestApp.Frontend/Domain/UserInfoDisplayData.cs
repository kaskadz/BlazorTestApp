using System.Collections.Generic;

namespace BlazorTestApp.Frontend.Domain
{
    public class UserInfoDisplayData
    {
        public string Login { get; set; }
        public string AzureAdObjectId { get; set; }
        public string UserIdClaim { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<string> RoleClaims { get; set; }
        public IEnumerable<string> AzureAdGroups { get; set; }
        public IEnumerable<TokenDisplayData> Tokens { get; set; }
    }
}