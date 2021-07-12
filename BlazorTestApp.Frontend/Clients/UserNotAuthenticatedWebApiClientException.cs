using System;

namespace BlazorTestApp.Frontend.Clients
{
    public class UserNotAuthenticatedWebApiClientException : Exception
    {
        public UserNotAuthenticatedWebApiClientException()
            : base("User is not authenticated. Client cannot be used.")
        {
        }
    }
}