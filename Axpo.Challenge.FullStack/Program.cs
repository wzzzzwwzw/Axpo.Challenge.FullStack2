using Axpo.Challenge.FullStack.Services;
using Axpo.Challenge.FullStack.Data;
using Axpo.Challenge.FullStack.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using Axpo.Challenge.FullStack.SeedData;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization; // Add this using directive

var builder = WebApplication.CreateBuilder(args);

// Set the invariant culture
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:7150")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

// Configure DbContext
builder.Services.AddDbContext<EnergyBalanceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EnergyConnectionString")));
// Register BalancingService
builder.Services.AddScoped<IBalancingService, BalancingService>();
// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Energy API", Version = "v1" });
});

// Register other services if necessary
// Example: builder.Services.AddScoped<IOtherService, OtherService>();

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<EnergyBalanceDbContext>();
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        await DataSeeder.SeedDataAsync(context, loggerFactory);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while seeding the database.");
        throw; // Consider re-throwing to prevent app from starting in a bad state
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();

// Enable Swagger UI in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Energy API v1"));
}

app.Run();