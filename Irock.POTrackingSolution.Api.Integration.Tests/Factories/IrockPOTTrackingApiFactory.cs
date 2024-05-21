using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;

namespace Irock.POTrackingSolution.Api.Integration.Tests.Factories
{
    public class IrockPOTTrackingApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
    {
        private readonly MsSqlTestcontainer _mssqlContainer;
        // protected readonly RefundApi _refundApi = new ();
        public IrockPOTTrackingApiFactory()
        {
            // _refundApi.Start();
            SetEnvironment();
            _mssqlContainer = new ContainerBuilder<MsSqlTestcontainer>()
                .WithName($"refund_sys_test-{Guid.NewGuid()}")
                .WithDatabase(new MsSqlTestcontainerConfiguration()
                {
                    Password = "456000Moh@",
                    Database = $"refund_sys_test_{Guid.NewGuid()}"
                })
                .Build();
        }

        private void SetEnvironment()
        {
            // if (AppMachine.MachineName.ToLower().Contains("mac"))
            //     Environment.SetEnvironmentVariable("DOCKER_HOST", "tcp://185.198.27.42:2377");
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureHostConfiguration((configBuilder) =>
            {
                // configBuilder.Sources.Clear();

                configBuilder.AddInMemoryCollection(new Dictionary<string, string>
            {
                {
                    "ConnectionStrings:MSSqlConnection",
                    $"{_mssqlContainer.ConnectionString};TrustServerCertificate=True"
                },
                {
                    "ConnectionStrings:NavitaireConnection",
                    $"{_mssqlContainer.ConnectionString};TrustServerCertificate=True"
                },               
                // {
                //     "AzureOptions:Url",
                //     $"{_refundApi.Url}"
                // },
                { "Database", "MSSQL" }
            });
            });
            return base.CreateHost(builder);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureLogging(logging => logging.ClearProviders());
            builder.ConfigureTestServices(services =>
            {
                services.Configure<JsonOptions>(options =>
                {
                    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });
                services.AddMassTransitTestHarness(cfg => { });
            });
        }

        public async Task InitializeAsync()
        {
            if (_mssqlContainer is not null)
                await _mssqlContainer.StartAsync();
        }

        public async Task DisposeAsync()
        {
            await _mssqlContainer.StopAsync();
            foreach (var factory in this.Factories)
                await factory.DisposeAsync();
        }
    }
}
