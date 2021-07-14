using System.Collections.Generic;

namespace BlazorTestApp.Frontend.Domain
{
    public class UserInfoDisplayData
    {
        public string Login { get; set; }
        public string Identifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<string> Groups { get; set; }
        public IEnumerable<TokenDisplayData> Tokens { get; set; }
    }
}