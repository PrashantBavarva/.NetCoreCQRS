using System;
using Common.Options;
using Domain.Contracts;
using MassTransit;
using MSSql.DependencyInjection;


namespace Irock.POTrackingSolution.Api.DependencyInjection
{
    public static class ApiAppExtension
    {
        public static IServiceCollection AddApiAppDependency(this IServiceCollection services,
       IConfiguration configuration)
        {
            var database = configuration.GetSection("Database").Get<string>();
            if (database == "MSSQL") services.AddMsSqlDependency(configuration);
            //services.RegisterRabbitMq(configuration);
            services.AddMassTransit(ms =>
            {
                ms.SetKebabCaseEndpointNameFormatter();

                var provider = configuration.GetSection("MessageBroker:MessageBrokerType").Get<string>();
                if (provider == "RabbitMq")
                    ms.UsingRabbitMq((ctx, cfg) =>
                    {
                        cfg.Message<SmsMessageContract>(m => { });

                        var host = configuration.GetSection("MessageBroker:RabbitMqOption:ConnectionString").Get<string>();
                        if (host.IsNullOrEmpty())
                            throw new NullReferenceException("RabbitMq Host is not set");
                        cfg.Host(new Uri(host));
                        cfg.ConfigureEndpoints(ctx);
                    });
            });
            return services;
        }
    }
}
