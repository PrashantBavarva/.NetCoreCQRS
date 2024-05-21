using System.Reflection;

namespace ValidationEngine.Builders;

public interface IValidationEngineBuilder
{
    IValidationEngineBuilder AddAssemblies(Assembly[] assemblies);
    IValidationEngineBuilder AddAssembly(Assembly assembly);
    void Build();
}