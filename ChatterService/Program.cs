using ChatterService;
using ChatterService.Hubs;
using ChatterService.Services;
using ChatterService.Entities;
using MongoDB.Bson.Serialization;
using WeatherService.Providers;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IDictionary<string, UserConnection>>(options => new Dictionary<string, UserConnection>());
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
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MessageService>();
builder.Services.AddSingleton<WeatherProvider>();
builder.Services.AddControllers();

BsonClassMap.RegisterClassMap<Message>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

app.MapGet("/", () => "Hello World!");
app.MapHub<ChatHub>("/chat");
app.MapControllers();

app.Run();
