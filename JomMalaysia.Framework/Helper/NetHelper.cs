using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Framework.Helper
{
    /// <summary>
    /// Utility class for network related operations.
    /// </summary>
    public static class NetHelper
    {
        /// <summary>
        /// Gets the base URL (Protocol + Host) from a full URL.
        /// </summary>
        /// <param name="uri">Full URI.</param>
        /// <returns>Base URL.</returns>
        public static string GetBaseUrl(Uri uri)
        {
            return uri.GetLeftPart(UriPartial.Authority);
        }

        /// <summary>
        /// Gets the base URL (Protocol + Host) from a full URL.
        /// </summary>
        /// <param name="fullUrl">Full URL.</param>
        /// <returns>Base URL.</returns>
        public static string GetBaseUrl(string fullUrl)
        {
            return GetBaseUrl(new Uri(fullUrl));
        }

        /// <summary>
        /// Gets the request path (starting after hostname) from a full URL.
        /// </summary>
        /// <param name="uri">Full URI.</param>
        /// <returns>Request path.</returns>
        public static string GetUrlPath(Uri uri)
        {
            return uri.GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);
        }

        /// <summary>
        /// Gets the request path (starting after hostname) from a full URL.
        /// </summary>
        /// <param name="uri">Full URL.</param>
        /// <returns>Request path.</returns>
        public static string GetUrlPath(string fullUrl)
        {
            return GetUrlPath(new Uri(fullUrl));
        }
    }
}
