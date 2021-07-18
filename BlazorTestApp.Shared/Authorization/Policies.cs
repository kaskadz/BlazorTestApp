namespace BlazorTestApp.Shared.Authorization
{
    public static class Policies
    {
        public static readonly NamedAuthorizationPolicy Admin = NamedAuthorizationPolicy.Create(
            "Admin", policy => policy.RequireRole("7a188f9b-cbcc-4997-82cb-4923601282de"));

        public static readonly NamedAuthorizationPolicy Weather = NamedAuthorizationPolicy.Create(
            "Weather", policy => policy.RequireRole("195d4a45-4de4-4600-89f3-72cae83e4d7b"));
    }
}