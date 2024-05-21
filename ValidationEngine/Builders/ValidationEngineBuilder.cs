using System.Reflection;
using Microsoft.AspNetCore.Builder;
using ValidationEngine.Interfaces;
using ValidationEngine.Services;

namespace ValidationEngine.Builders;

internal class ValidationEngineBuilder: IValidationEngineBuilder
{
    private readonly WebApplication _app;
    private readonly List<Assembly> _assemblies=new();
    public IReadOnlyCollection<Assembly> Assemblies => _assemblies.AsReadOnly();
    public string BaseRoute { get;private set; }
    public ValidationEngineBuilder(WebApplication app, string baseRoute="/api/validation")
    {
        _app = app;
        BaseRoute = baseRoute;
    }
    
    public IValidationEngineBuilder AddAssemblies(Assembly[] assemblies)
    {
        _assemblies.AddRange(assemblies);
        return this;
    }
    public IValidationEngineBuilder AddAssembly(Assembly assembly)
    {
        _assemblies.Add(assembly);
        return this;
    }

    public void Build()
    {
        _app.MapGet($"{BaseRoute}/operators", (IOperatorService service) => service.GetOperators());
        _app.MapGet($"{BaseRoute}/operators/{{type}}", (IOperatorService service, string type) => service.GetOperators(type));
        _app.MapGet($"{BaseRoute}/types", (IOperatorService service) => service.GetTypes());
        _app.MapGet($"{BaseRoute}/entities", (IEntityService service) => service.GetEntities());
        _app.MapGet($"{BaseRoute}/entities/{{entity}}", (IEntityService service, string entity) => service.GetFields(entity));
        
    }
}