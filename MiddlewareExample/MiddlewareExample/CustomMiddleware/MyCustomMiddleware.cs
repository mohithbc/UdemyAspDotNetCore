
namespace MiddlewareExample.CustomMiddleware
{
    public class MyCustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Hello from MyCustomMiddleware! \n");
            await next(context);
            await context.Response.WriteAsync("Goodbye from MyCustomMiddleware! \n");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    // It must be static method inside a static class.
    // An extention method is a method that is getting injected into an object dynamically.
    public static class MyCustomMiddlewareExtensions
    {
        // This method is called in the Startup.cs file inside the Configure method.
        // instead of (this IApplicationBuilder builder) you can also use ( this IApplicationBuilder app)
        // but the convention is to use builder.
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyCustomMiddleware>();
        }
    }
}
