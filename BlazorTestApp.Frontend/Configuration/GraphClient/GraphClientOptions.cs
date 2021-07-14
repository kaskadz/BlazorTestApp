using System.Collections.Generic;

namespace BlazorTestApp.Frontend.Configuration.GraphClient
{
    public class GraphClientOptions
    {
        public const string Section = "GraphClient";

        public IEnumerable<string> Scopes { get; set; }
    }
}