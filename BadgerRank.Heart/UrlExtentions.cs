using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace BadgerRank.Heart
{
    public static class UrlExtentions
    {
        public static Uri BuildWithQueryParameters(this string baseValue, IDictionary<string, string> parameters)
        {
            var urlBuilder = new StringBuilder(baseValue);
            urlBuilder.Append("?");

            foreach (var parameter in parameters)
            {
                urlBuilder.Append(HttpUtility.UrlEncode(parameter.Key));
                urlBuilder.Append("=");
                urlBuilder.Append(HttpUtility.UrlEncode(parameter.Value));
                urlBuilder.Append("&");
            }

            return new Uri(urlBuilder.ToString().TrimEnd('&', '?'), UriKind.Relative);
        }
    }
}
