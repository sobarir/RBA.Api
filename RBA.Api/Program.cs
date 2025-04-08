﻿global using FluentValidation;

using FastEndpoints;
using Serilog;

using RBA.Repository;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();

logger.Information("Starting RBA.Api");
builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

var connectionString = config.GetConnectionString("DefaultConnection");
Func<IServiceProvider, IFreeSql> fsqlFactory = r =>
{
  IFreeSql fsql = new FreeSql.FreeSqlBuilder()
    .UseConnectionString(FreeSql.DataType.PostgreSQL, connectionString)
    .UseMonitorCommand(cmd => logger.Information($"Sql：{cmd.CommandText}"))
    .UseAutoSyncStructure(false)
    .Build();
  return fsql;
};
builder.Services.AddSingleton(fsqlFactory);

//Repositories
builder.Services.AddSingleton<IActionRepository, ActionRepository>();
builder.Services.AddSingleton<IApplicationRepository, ApplicationRepository>();
builder.Services.AddSingleton<IAppUserRepository, AppUserRepository>();
builder.Services.AddSingleton<IRoleActionRepository, RoleActionRepository>();
builder.Services.AddSingleton<IRolePlantRepository, RolePlantRepository>();
builder.Services.AddSingleton<IUserAllowedPlantRepository, UserAllowedPlantRepository>();
builder.Services.AddSingleton<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddSingleton<IVUserRolePlantRepository, VUserRolePlantRepository>();

builder.Services.AddFastEndpoints(o => o.IncludeAbstractValidators = true);
builder.Services.AddOpenApi();

var app = builder.Build();

//app.UseFastEndpoints();
app.UseFastEndpoints(c =>
{
  c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();

app.Run();

public partial class Program { }
