using System.Data.SqlClient;
using WACesi_Maisons.repository;
using Microsoft.Extensions.Configuration;
using WACesi_Maisons.Models;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);

var configuration = configurationBuilder.Build();
AppSettings appSettings = configuration.Get<AppSettings>();
string connectionString = appSettings.ConnectionStrings.SQLServerConnexion;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

//Dependency injection
builder.Services.AddScoped<IPromoRepository, PromoRepository>();
builder.Services.AddTransient(provider =>
{
    return new MySqlConnection(connectionString);
});

var app = builder.Build();

app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
