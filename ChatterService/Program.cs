using ChatterService;
using ChatterService.Hubs;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, option =>
    {
        option.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});
builder.Services.AddSignalR();
builder.Services.AddSingleton<IDictionary<string, UserConnection>>(options => new Dictionary<string, UserConnection>());

var app = builder.Build();

app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

app.MapGet("/", () => "Hello World!");
app.MapHub<ChatHub>("/chat");

app.Run();
