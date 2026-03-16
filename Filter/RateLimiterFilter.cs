using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace Ecommerce.Filter
{
    public class RateLimiterFilter(IMemoryCache cache) : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {


            var key = context.HttpContext.Connection.RemoteIpAddress.ToString();

            if(!cache.TryGetValue(key,out int requestCount))
            {
                requestCount = 0;

            }

            if (requestCount >= 3)
            {

                context.Result = new ContentResult()
                {
                    StatusCode = 429,
                    Content = "Rate Limit Exeeded"
                };
                return;

            }

            cache.Set(key, requestCount + 1, TimeSpan.FromMinutes(1));

        }

      

      
        public void OnActionExecuted(ActionExecutedContext context)
        {
           //
        }

       
    }
}
