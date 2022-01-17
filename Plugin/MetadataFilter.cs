using System;
using System.Linq;


namespace SRXD.SpinSearch.Plugin;

public static class MetadataFilter
{
    private static string[] searchQueries = new string[] { };

    public static bool ShouldFilterIn(TrackInfoMetadata metadata)
    {
        foreach (var searchQuery in searchQueries)
        {
            bool fulfillsQuery = (ContainsIgnoreCase(metadata.title, searchQuery)
                || ContainsIgnoreCase(metadata.artistName, searchQuery)
                || ContainsIgnoreCase(metadata.description, searchQuery)
                || ContainsIgnoreCase(metadata.subtitle, searchQuery)
                || ContainsIgnoreCase(metadata.featArtists, searchQuery)
                || ContainsIgnoreCase(metadata.charter, searchQuery) );

            if (!fulfillsQuery)
                return false;
        }
        return true;
    }

    private static bool ContainsIgnoreCase(string haystack, string needle) =>
        haystack.IndexOf(needle, StringComparison.OrdinalIgnoreCase) >= 0;

    public static void SetSearchFilter(string searchFilter)
    {
        searchQueries = searchFilter.Split(' ')
            .Where(x => !String.IsNullOrEmpty(x))
            .Distinct()
            .ToArray();
    }
}
