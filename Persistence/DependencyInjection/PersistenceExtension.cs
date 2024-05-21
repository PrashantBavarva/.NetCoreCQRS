using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Persistence.DependencyInjection;

public static class PersistenceExtension
{
    public static IServiceCollection AddPersistenceDependency(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
}