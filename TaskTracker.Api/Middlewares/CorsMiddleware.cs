namespace TaskTracker.Api.Middlewares
{
    public class CorsMiddleware
    {
        private readonly RequestDelegate _next;

        public CorsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Method == "OPTIONS")
            {
                httpContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { (string)httpContext.Request.Headers["Origin"] });
                httpContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Origin, Authorization, X-Requested-With, Content-Type, Accept" });
                httpContext.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "GET, POST, PUT, DELETE, OPTIONS" });
                httpContext.Response.Headers.Add("Access-Control-Allow-Credentials", new[] { "true" });
                httpContext.Response.StatusCode = 200;
                return httpContext.Response.WriteAsync("OK");
            }

            httpContext.Response.Headers.Add("Access-Control-Allow-Origin", "https://task-track.ru");
            httpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            httpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Authorization, Content-Type, X-CSRF-Token, X-Requested-With, Accept, Accept-Version, Content-Length, Content-MD5, Date, X-Api-Version, X-File-Name");
            httpContext.Response.Headers.Add("Access-Control-Allow-Methods", "POST, GET, PUT, PATCH, DELETE, OPTIONS");

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CorsMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorsMiddleware>();
        }
    }
}
