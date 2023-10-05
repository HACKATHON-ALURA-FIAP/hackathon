using CidadaoAlerta.Interfaces;
using CidadaoAlerta.Services;
using Domain.Interfaces;
using Domain.Services;
using Infra.Context;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IAddressesService, AddressesService>();
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<ApiCadastroContext>(options => options.UseOracle(connectionString).EnableSensitiveDataLogging(true));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IAddressesRepository, AddressesRepository>();
builder.Services.AddScoped<IAddressesService, AddressesService>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

void ConfigureServices(IServiceCollection services)
{
    services.AddRazorPages();
    services.AddCors();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

