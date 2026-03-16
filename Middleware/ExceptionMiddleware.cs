using System.Net;
using System.Text.Json;

namespace Ecommerce.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
     

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await next(context);
            }
            catch(Exception ex)
            {

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var msg = new
                {

                    statusCode = context.Response.StatusCode,
                    Message="unexpected error occured"
                };

                var json = JsonSerializer.Serialize(msg);
                await context.Response.WriteAsync(json);

            }




        }



    }
}
