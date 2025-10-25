var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
    {
        if (1 == 1)
        {
            context.Response.StatusCode = 200;
        }
        else
        {
            context.Response.StatusCode = 400;
        }
        // Response headers
        context.Response.Headers["MyKey"] = "My value";
        context.Response.Headers["Server"] = "My server";
        context.Response.Headers["Content-Type"] = "text/html";
        await context.Response.WriteAsync("<h1>Response Headers set</h1>");
        await context.Response.WriteAsync("<h2>Status Code set</h2>");

        await context.Response.WriteAsync("Hello");
        await context.Response.WriteAsync("World!");
    });

app.Run();
