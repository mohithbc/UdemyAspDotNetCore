var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Code 1: The Consequence of Multiple app.Run Calls

//app.Run(async (HttpContext context) => {
//    await context.Response.WriteAsync("Hello");
//});

//app.Run(async (HttpContext context) => {
//    await context.Response.WriteAsync("Hello again");
//});


// Code 2: Chaining Middleware with app.Use and app.Run

//middlware 1
app.Use(async (HttpContext context,RequestDelegate next) => {
await context.Response.WriteAsync("Hello ");
await next(context);
});

//middleware 2
app.Use(async (context, next) => {
    await context.Response.WriteAsync("Hello again ");
    await next(context);
});
app.Run();
