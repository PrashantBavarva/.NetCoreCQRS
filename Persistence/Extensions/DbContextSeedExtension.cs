using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.DependencyInjection.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Seeds.Base;

namespace Persistence.Extensions;

public static class DbContextSeedExtension
{

    public static async  Task ApplySeeds(this  AppDbContext context,IConfiguration configuration,CancellationToken cancellationToken=default)
    {
        var seedClasses = typeof(IEntitySeed).Assembly
            .GetTypes()
            .Where(t => t is { IsAbstract: false, IsInterface: false } && 
                        t.GetInterfaces().Contains(typeof(IEntitySeed)))
            .ToList();

        var seeds = seedClasses
            .Select(s => (IEntitySeed)Activator.CreateInstance(s)!)
            .ToList();
        foreach (var seed in seeds)
        {
            await seed.SeedAsync(context,configuration, cancellationToken);
        }
    }

    public static async Task ApplyMigrations(this WebApplication app,IConfiguration configuration)
    {
       using var scope=app.Services.CreateScope();
       var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
       var startups = scope.ServiceProvider.GetServices<IAfterMigrateStartup>();
       await context.Database.MigrateAsync();
       await context.ApplySeeds(configuration);
        
       foreach (var startup in startups)
       {
           await startup.StartAsync();
       }
    }
}