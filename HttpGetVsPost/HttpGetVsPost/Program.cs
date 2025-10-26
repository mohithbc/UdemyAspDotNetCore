using System.IO;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
    { 
        StreamReader reader = new StreamReader(context.Request.Body);
        string body = await reader.ReadToEndAsync();
    });

app.Run();
