var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// Routing is automaticaly enabled no need for app.UseRouting() anymore
// ENdpoints are defined directly on the app object

// This will match any HTTP method to /map1
app.Map("map1", async (context) =>
{
    await context.Response.WriteAsync("In Map1");
});
// This will match any HTTP method to /map2
app.MapGet("map2", async (context) =>
{
    await context.Response.WriteAsync("In Map2");
});
// This will only match POST requests to /map3
app.MapPost("map3", async (context) =>
{
    await context.Response.WriteAsync("In Map3");
});
//Fallback for any other route
app.MapFallback(async (context) =>
{
    await context.Response.WriteAsync("Fallback route");
});
app.Run();
