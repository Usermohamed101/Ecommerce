using Azure.Core;
using Microsoft.Extensions.Caching.Memory;

namespace Ecommerce.Middleware
{
    public class RateLimiterMiddleware(RequestDelegate _next)
    {
        MemoryCache cache = new MemoryCache(new MemoryCacheOptions());
   

       


        public async Task Invoke(HttpContext contxt)
        {

            var key = contxt.Connection.RemoteIpAddress?.ToString();
            if (!cache.TryGetValue(key, out int requestCount))
            {
                requestCount = 0;

            }
            if (requestCount >= 30)
            {
                contxt.Response.StatusCode = 429;
                await contxt.Response.WriteAsync("rate limit exeeded");
                return;
            }


             cache.Set(key, requestCount + 1,TimeSpan.FromMinutes(1));
            await  _next(contxt);

        }


    }
}
