using System.Text.Json;
using System.Text.Json.Serialization;
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
using Microsoft.Extensions.Logging;
using Irock.POTrackingSolution.Api;
namespace Application.Integration.Tests.Factories
{
    public class BHFPOTrackingSolutionApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
    {
        private readonly MsSqlTestcontainer _mssqlContainer;

        public BHFPOTrackingSolutionApiFactory()
        {
            var containerName = $"bhfpot-{Guid.NewGuid()}";
            _mssqlContainer = new ContainerBuilder<MsSqlTestcontainer>()
                .WithName($"bhfpot-{Guid.NewGuid()}")
                .WithDatabase(new MsSqlTestcontainerConfiguration()
                {
                    Password = "1234567ALEX@",
                    Database = $"refund_sys_test_{Guid.NewGuid()}"
                })
                .Build();
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureHostConfiguration((configBuilder) =>
            {
                configBuilder.AddInMemoryCollection(new Dictionary<string, string>
            {
                {
                    "ConnectionStrings:MSSqlConnection",
                    $"{_mssqlContainer.ConnectionString};TrustServerCertificate=True"
                },
                { "Database", "MSSQL" }
            }!);
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
