using DevStudy.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using DevStudy.Application.Mappers.Mapping;
using DevStudy.Domain.Interfaces;
using DevStudy.Application.Interfaces;
using DevStudy.Infrastructure.Repository;
using DevStudy.Application.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração da conexão com o banco de dados
var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

builder.Services.AddAutoMapper(typeof(AlunoMappingProfile).Assembly);
builder.Services.AddAutoMapper(typeof(TreinoMappingProfile).Assembly);
builder.Services.AddAutoMapper(typeof(TreinoExercicioMappingProfile).Assembly);

builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IAlunoService, AlunoService>();

builder.Services.AddScoped<ITreinosRepository, TreinosRepository>();
builder.Services.AddScoped<ITreinosService, TreinosService>();

builder.Services.AddScoped<IExerciciosRepository, ExerciciosRepository>();
builder.Services.AddScoped<IExerciciosService, ExerciciosService>();

builder.Services.AddScoped<ITreinoExercicioRepository, TreinoExercicioRepository>();
builder.Services.AddScoped<ITreinoExercicioService, TreinoExercicioService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "DevStudy.API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
