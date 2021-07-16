using System.Collections.Generic;
using System.Linq;

namespace BlazorTestApp.Frontend.Configuration.Authnz
{
    public class GroupMappingProvider : IGroupMappingProvider
    {
        private static readonly (string groupName, string groupId)[] GroupNameIdPairs =
        {
            ("Admin", "7a188f9b-cbcc-4997-82cb-4923601282de"), // administration
            ("Weather", "195d4a45-4de4-4600-89f3-72cae83e4d7b") // weather
        };

        public IReadOnlyDictionary<string, string> NameToId { get; } =
            GroupNameIdPairs.ToDictionary(
                keySelector: x => x.groupName,
                elementSelector: x => x.groupId);

        public IReadOnlyDictionary<string, string> IdToName { get; } =
            GroupNameIdPairs.ToDictionary(
                keySelector: x => x.groupId,
                elementSelector: x => x.groupName);
    }
}