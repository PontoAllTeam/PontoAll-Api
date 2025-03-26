using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Data;
using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Data.Repositories;
using PontoAll.WebAPI.Services.Entities;
using PontoAll.WebAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (env == "Production")
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
}
builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Scoped Services and Interfaces
builder.Services.AddScoped<IScaleService, ScaleService>();

//Scoped Repositories and Interfaces
builder.Services.AddScoped<IScaleRepository, ScaleRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
