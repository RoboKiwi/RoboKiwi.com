using System.Linq;
using Microsoft.AspNetCore.Http;

namespace RoboKiwi.Functions;

static class HttpHeadersExtensions
{
    public static string GetClientIp(this HttpRequest request)
    {
        return request.Headers.GetSingleHeader("X-Forwarded-For")
               ?? request.HttpContext?.Connection?.RemoteIpAddress?.ToString();
    }

    public static string GetSingleHeader(this IHeaderDictionary headers, string name)
    {
        return !headers.TryGetValue(name, out var values) ? null : values.SingleOrDefault();
    }
}