using Application;
using Common.DependencyInjection;
using Common.Extensions;
using Domain;
using Hangfire;
using Infrastructure;
using Infrastructure.Options;
using Persistence;
using ValidationEngine;
using ValidationEngine.DependencyInjection;
using Infrastructure.DependencyInjection;
using Application.DependencyInjection;
using FastEndpoints;
using FastEndpoints.Swagger;
using Irock.POTrackingSolution.Api.DependencyInjection;
using Irock.POTrackingSolution.Api.Extensions;
using Irock.POTrackingSolution.Api.Endpoints.Base;
using Irock.POTrackingSolution.Api.Filters;

var assemblies = new[]
{
    typeof(Program).Assembly, PersistenceProj.Assembly, DomainProj.Assembly, ApplicationProj.Assembly,
    InfrastructureProj.Assembly, CommonProj.Assembly, ValidationBuilderProj.Assembly
};

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Host.UseCommon();
builder.Services
    .AddMapster(assemblies)
    .AddAutoRegister(assemblies)
    .AddHangFire(builder.Configuration)
    .AddInfrastructureDependency(builder.Configuration)
    .AddApplicationDependency(builder.Configuration)
    .AddPersistenceDependency(builder.Configuration)
    .AddValidationEngineDependency()
    .AddApiAppDependency(builder.Configuration)
    .AddFastEndpoints(c => { })
    .AddEndpointsApiExplorer()
    .AddSwaggerDoc()
    .AddCors()
    .Configure<AzureOptions>(builder.Configuration.GetSection("AzureOptions"))
    .AddSwaggerGen(c => c.SchemaFilter<SmartEnumSchemaFilter>())
;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseHangfireDashboard("/hangfire", new DashboardOptions()
//{
//    Authorization = new[] { new DashboardNoAuthorizationFilter() }
//});

app
    .UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints(c => c.Endpoints.RoutePrefix = "api")
    .UseSwaggerGen()
    .UseCors(opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
;
app.AddValidationEngineEndpoints();
app.MapGet("/api/client", () => new
{
    Id = Guid.NewGuid(),
    Name = "Client 1"
});

try
{
    app.Run();
}
catch (Exception exception)
{
    Console.WriteLine(exception);
    throw;
}