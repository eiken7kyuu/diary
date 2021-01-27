using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ProtectedDiary.Middleware
{
    public class CustomHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            if (context.Response.StatusCode == 200)
            {
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
            }
            return _next(context);
        }
    }

    public static class CustomHeaderMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomHeader(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomHeaderMiddleware>();
        }
    }
}