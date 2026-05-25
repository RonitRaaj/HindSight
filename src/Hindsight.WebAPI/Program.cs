using Hindsight.Application.interfaces;
using Hindsight.Application.Interfaces;
using Hindsight.Application.Services;
using Hindsight.Infrastructure.Data;
using Hindsight.Infrastructure.Repositories;
using Hindsight.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Force Kestrel server to bind perfectly to Render's internal dynamic porting engine
var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
builder.WebHost.ConfigureKestrel(options => options.ListenAnyIP(int.Parse(port)));

// 1. Configure the Database Context (Using SQLite for an easy sandbox setup)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? "Data Source=hindsight.db"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendApp", policy =>
    {
        policy.WithOrigins("https://time-fortune.ronitrj1234.workers.dev/") // Allows your local React build
              .AllowAnyHeader()                     // Allows content-type, accept, etc.
              .AllowAnyMethod();                    // Allows GET, POST, etc.
    });
});

// 2. Register Repositories and Domain Services
builder.Services.AddScoped<IAssetPriceSnapshot, AssetPriceSnapshotRepository>();
builder.Services.AddScoped<IMetadataLookup , MetadataLookupRepository>();
builder.Services.AddScoped<CalculatorService>();
builder.Services.AddScoped<MetadataService>();

// 3. Register Resilient Typed HttpClient for External Market API Fetching
builder.Services.AddHttpClient<IAssetPriceFetcher, LiveAssetPriceFetcher>(client =>
{
    client.DefaultRequestHeaders.Add("User-Agent", "Hindsight-Market-Engine-API");
    client.Timeout = TimeSpan.FromSeconds(10);
});

builder.Services.AddHttpClient<ICurrencyExchange, CurrencyExchange>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(10);
});

// 4. Add Web API Controller Routing Frameworks
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Generates your interactive UI web playground

var app = builder.Build();

// 5. Configure the HTTP Request Execution Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hindsight API v1"));
}

app.UseCors("AllowFrontendApp");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();