namespace BlazorTestApp.Shared.Authorization
{
    public static class Policies
    {
        public static class Admin
        {
            public const string PolicyName = "Admin";

            public static readonly NamedAuthorizationPolicy NamedPolicy = NamedAuthorizationPolicy.Create(
                PolicyName, policy => policy.RequireRole("7a188f9b-cbcc-4997-82cb-4923601282de"));
        }

        public static class Weather
        {
            public const string PolicyName = "Weather";

            public static readonly NamedAuthorizationPolicy NamedPolicy = NamedAuthorizationPolicy.Create(
                PolicyName, policy => policy.RequireRole("195d4a45-4de4-4600-89f3-72cae83e4d7b"));
        }

        public static class Player
        {
            public const string PolicyName = "Player";

            public static readonly NamedAuthorizationPolicy NamedPolicy = NamedAuthorizationPolicy.Create(
                PolicyName, policy => policy.RequireRole("dfd2c19f-5ed9-4d74-b02c-1e13dd13602f"));
        }

        public static class Host
        {
            public const string PolicyName = "Host";

            public static readonly NamedAuthorizationPolicy NamedPolicy = NamedAuthorizationPolicy.Create(
                PolicyName, policy => policy.RequireRole("54ec2745-bc5d-4c3c-9b0b-c152f25828f0"));
        }

        public static class Editor
        {
            public const string PolicyName = "Editor";

            public static readonly NamedAuthorizationPolicy NamedPolicy = NamedAuthorizationPolicy.Create(
                PolicyName, policy => policy.RequireRole("90fb28fc-c328-41b9-a8b5-a1dc63c00679"));
        }
    }
}