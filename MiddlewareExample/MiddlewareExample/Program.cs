using MiddlewareExample.CustomMiddleware;// import the namespace for custom middleware

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyCustomMiddleware>();// register custom middleware
var app = builder.Build();

// Code 1: The Consequence of Multiple app.Run Calls

//app.Run(async (HttpContext context) => {
//    await context.Response.WriteAsync("Hello");
//});

//app.Run(async (HttpContext context) => {
//    await context.Response.WriteAsync("Hello again");
//});


// Code 2: Chaining Middleware with app.Use and app.Run

// if you use app.Run here, the next middleware will not be executed
// because app.Run is a terminal middleware
// if you want to chain middleware, use app.Use instead

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

app.UseMiddleware<MyCustomMiddleware>();// use custom middleware

//middleware 3
app.Run(async (HttpContext context) => {
    await context.Response.WriteAsync("Goodbye!");
});

app.Run();
