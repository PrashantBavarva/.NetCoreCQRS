using System.Net.Http.Headers;
using Common;
using Common.Extensions;
using Common.Options;
using Infrastructure.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructureDependency(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMemoryCache();
        
        //services.AddHttpClient<DeewanSmsSender>((client) =>
        //{
        //    var option = configuration.GetSection("DeewanSmsOptions").Get<DeewanSmsOptions>();
        //    client.BaseAddress = new Uri(option.BaseUrl);
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", option.Token);
        //});
        
        return services;
    }
}