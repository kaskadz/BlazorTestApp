using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace BlazorTestApp.Frontend.Configuration.Authentication
{
    public class CustomUserAccount : RemoteUserAccount
    {
        [JsonPropertyName("groups")]
        public IEnumerable<string> Groups { get; set; } = Enumerable.Empty<string>();

        [JsonPropertyName("oid")]
        public string Oid { get; set; }
    }
}