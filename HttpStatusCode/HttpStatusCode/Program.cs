var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
    {
        if (1 == 1)
        {
            context.Response.StatusCode = StatusCodes.Status200OK;
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
        await context.Response.WriteAsync("Hello");
        await context.Response.WriteAsync("World!");
    });

app.Run();
