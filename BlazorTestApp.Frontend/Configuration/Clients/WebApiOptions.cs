using System;

namespace BlazorTestApp.Frontend.Configuration.Clients
{
    public class WebApiOptions
    {
        public const string Section = "WebApi";

        public Uri Url { get; set; }
    }
}