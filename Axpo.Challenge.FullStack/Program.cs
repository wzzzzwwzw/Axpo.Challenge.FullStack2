using Axpo.Challenge.Balancing.Service;
using Axpo.Challenge.FullStack.Data;
using Axpo.Challenge.FullStack.Extensions;
using Microsoft.EntityFrameworkCore;  // Ensure this is included for EF Core
using Microsoft.Extensions.Options;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);

// Set the invariant culture
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowAnyOrigin();
                      });
});

builder.Services.AddControllers();

// Use AddDbContext (singular) instead of AddDbContexts
builder.Services.AddDbContext<EnergyBalanceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EnergyConnectionString")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

// Register the BalancingService
builder.Services.AddSingleton<IBalancingService, BalancingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
