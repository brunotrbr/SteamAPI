using Microsoft.AspNetCore.Mvc.Filters;

namespace SteamAPI.Filters
{
    public class CookiesFilter : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            var setCookieHeaderValues = context.HttpContext.Response.Headers;

            var cookies = setCookieHeaderValues["Set-Cookie"];
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            
        }
    }
}
