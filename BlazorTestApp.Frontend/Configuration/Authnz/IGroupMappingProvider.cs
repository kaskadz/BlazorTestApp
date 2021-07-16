using System.Collections.Generic;

namespace BlazorTestApp.Frontend.Configuration.Authnz
{
    public interface IGroupMappingProvider
    {
        IReadOnlyDictionary<string, string> NameToId { get; }
        IReadOnlyDictionary<string, string> IdToName { get; }
    }
}