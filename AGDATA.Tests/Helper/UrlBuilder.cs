using System;

namespace AGDATA.ApiTests.Helper
{
    internal static class UrlBuilder
    {
        private const string urlDelimiter = "/";

        public static Uri BuildBaseUrl(string baseUrl)
        {
            if (!baseUrl.EndsWith(urlDelimiter, StringComparison.OrdinalIgnoreCase))
            {
                baseUrl += urlDelimiter;
            }

            return new Uri(baseUrl);
        }

        public static Uri BuildBaseUrl(Uri baseUrl)
        {
            return BuildBaseUrl(baseUrl.ToString());
        }

        public static Uri BuildUrl(string baseUrl, params string[] paths)
        {
            if (paths.Length > 0)
            {
                return new Uri(BuildBaseUrl(baseUrl), BuildUrlPath(paths));
            }

            return new Uri(baseUrl);
        }

        public static string BuildUrlPath(params string[] paths)
        {
            return string.Join(urlDelimiter, paths);
        }

        public static Uri AddQueryParam(this Uri uri, string query)
        {
            if (uri == null || string.IsNullOrWhiteSpace(query))
                return uri;
            var existingQuery = uri.Query;
            var uriBuilder = new UriBuilder(uri)
            {
                Query = query
            };
            if (!string.IsNullOrWhiteSpace(existingQuery))
            {
                uriBuilder.Query = $"{existingQuery}&{query}";
            }

            return uriBuilder.Uri;
        }
    }
}
