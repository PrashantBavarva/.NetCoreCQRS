using Hangfire;
using Hangfire.MAMQSqlExtension;
using Hangfire.SqlServer;

namespace Irock.POTrackingSolution.Api.Extensions
{
    public static class HangFireExtension
    {
        public static IServiceCollection AddHangFire(this IServiceCollection services, IConfiguration configuration)
        {
            var queueName = "smscampaign-execution";
            services.AddHangfire(x =>
                    x.UseMAMQSqlServerStorage(configuration.GetConnectionString("HangFireDb"),
                        new SqlServerStorageOptions()
                        {
                            UsePageLocksOnDequeue = true,
                            DisableGlobalLocks = true,
                        }, new[] { queueName, "default" })
                )
                .AddHangfireServer((options) =>
                {
                    options.ServerName = string.Format("{0}:{1}", AppMachine.MachineName, queueName);
                    options.Queues = new[] { queueName, "default" };
                    options.WorkerCount = 3;
                });

            return services;
        }
    }
}
