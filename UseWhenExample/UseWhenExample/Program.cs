var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWhen(
    context => context.Request.Query.ContainsKey("username"),
    app => 
    {
        app.Use(async (context, next) => 
        {
            //var username = context.Request.Query["username"];
            await context.Response.WriteAsync("Hello from middleware branch \n");
            await next();
        });
    });

app.Run(async context =>
await context.Response.WriteAsync("Hello from main pipeline")
);

app.Run();
