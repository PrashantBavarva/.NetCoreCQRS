using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Persistence.DependencyInjection;

namespace MSSql.DependencyInjection;

public static class MSSqlPersistenceExtension
{
    public static IServiceCollection AddMsSqlDependency(this IServiceCollection services,
        IConfiguration configuration)
    {
        var database = configuration.GetSection("Database").Get<string>();
        if (database != "MSSQL") return services;
        services
            .AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("MSSqlConnection"),
                    b => b.MigrationsAssembly(typeof(MSSqlPersistenceExtension).Assembly.FullName)));

        // services.AddHostedService<ApplyMigrationHost>();
        return services;
    }
}