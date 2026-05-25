using Hindsight.Application.interfaces;
using Hindsight.Application.Interfaces;
using Hindsight.Application.Services;
using Hindsight.Infrastructure.Data;
using Hindsight.Infrastructure.Repositories;
using Hindsight.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 🚀 REMOVED: Manual Kestrel port binding has been completely deleted.
// .NET will now naturally listen to Render's default routing variables.

// 1. Configure the Database Context (Using SQLite for an easy sandbox setup)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    var dbPath = Path.Combine(AppContext.BaseDirectory, "hindsight.db");
    connectionString = $"Data Source={dbPath}";
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

// Configure CORS to trust your active Cloudflare production network and local testing
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendApp", policy =>
    {
        policy.WithOrigins(
                "http://localhost:8080", 
                "https://time-fortune.ronitrj1234.workers.dev"
              ) 
              .AllowAnyHeader()                     
              .AllowAnyMethod();                    
    });
});

// 2. Register Repositories and Domain Services
builder.Services.AddScoped<IAssetPriceSnapshot, AssetPriceSnapshotRepository>();
builder.Services.AddScoped<IMetadataLookup, MetadataLookupRepository>();
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
builder.Services.AddSwaggerGen(); 

var app = builder.Build();

// 5. Configure the HTTP Request Execution Pipeline
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hindsight API v1"));

// Keep CORS right at the beginning of the middleware stack
app.UseCors("AllowFrontendApp");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// 6. Automatically create the SQLite file, generate tables, and run migrations on boot
using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        Console.WriteLine("⏳ Running database migration scripts inside the Docker sandbox...");
        await context.Database.MigrateAsync();
        Console.WriteLine("🚀 SQLite Database successfully initialized, tables built, and historical data seeded!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ CRITICAL DATABASE INITIALIZATION ERROR: {ex.Message}");
    }
}

app.Run();