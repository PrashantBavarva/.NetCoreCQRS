using System.Configuration;
using Common.Files;
using Common.Logging;
using Common.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Common.DependencyInjection;

public static class CommonExtension
{
    public static IServiceCollection AddCommon(this IServiceCollection services,IConfiguration configuration)
    {
        
        services
            //Register Options
            .Configure<GeneratorOptions>(configuration.GetSection(nameof(GeneratorOptions)))
            .Configure<RabbitMqOption>(configuration.GetSection(nameof(RabbitMqOption)))
            
            ;
        return services;
    }   
    public static IHostBuilder UseCommon(this IHostBuilder services)
    {
        
        //services
        //    //Using serilog
        //    .UseSerilog((context, configuration) =>
        //    {
        //        var env = context.HostingEnvironment.EnvironmentName;
        //        var config = new ConfigurationBuilder()
        //            .AddJsonFile("appsettings.json", true)
        //            .AddJsonFile($"appsettings.{env}.json", true)
        //            .Build();
        //         configuration
        //            .ReadFrom.Configuration(config)
        //            .WriteToElastic(context)
        //            .Enrich.FromLogContext();
        //        Log.Logger.Information("current env is {env}", env);
        //    })
        //    .ConfigureAppConfiguration((context, builder) =>
        //    {
        //        builder.AddEnvironmentVariables(prefix: "App_");
        //    })
        //    ;
        return services;
    }
    private static LoggerConfiguration WriteToElastic(
        this LoggerConfiguration loggerConfiguration,
        HostBuilderContext context
    )
    {
        var elsUri = context.Configuration
            .GetSection("ElasticConfiguration:Uri")?.Value??string.Empty;
        var elsIndex = context.Configuration
            .GetSection("ElasticConfiguration:DefaultIndex")?.Value??string.Empty;
        var elsEnable = context.Configuration
            .GetSection("ElasticConfiguration:Enable")
            ?.Get<bool>()??false;
        if(elsEnable)
        {
            loggerConfiguration.WriteTo.Elasticsearch(
                new ElasticsearchSinkOptions(new Uri(elsUri))
                {
                    IndexFormat = elsIndex
                });
        }
        return loggerConfiguration;
    }
}