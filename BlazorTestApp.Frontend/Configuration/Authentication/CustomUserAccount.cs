using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace BlazorTestApp.Frontend.Configuration.Authentication
{
    public class CustomUserAccount : RemoteUserAccount
    {
        [JsonPropertyName("groups")]
        public string[] Groups { get; set; } = Array.Empty<string>();

        [JsonPropertyName("oid")]
        public string Oid { get; set; }
    }
}