var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting(); 
app.UseEndpoints(endpoints => { 
    endpoints.Map("/", async context => 
    {
        await context.Response.WriteAsync
        (app.Configuration["MyKey"]);
          
        await context.Response.WriteAsync
        (app.Configuration.GetValue<string>("MyKey"));
    });
});
app.MapControllers();

app.Run();
