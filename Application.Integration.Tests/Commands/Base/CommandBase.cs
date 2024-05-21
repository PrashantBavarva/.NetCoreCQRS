using Application.Integration.Tests.Factories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Integration.Tests.Commands.Base
{
    public class CommandBase : IClassFixture<BHFPOTrackingSolutionApiFactory>, IDisposable
    {
        private readonly IServiceScope _scope;
        protected ISender Sender => GetRequiredService<ISender>();

        public CommandBase(BHFPOTrackingSolutionApiFactory factory)
        {
            _scope = factory.Services.CreateScope();
        }

        public TService GetRequiredService<TService>() where TService : notnull =>
            _scope.ServiceProvider.GetRequiredService<TService>();

        public TService? GetService<TService>() => _scope.ServiceProvider.GetService<TService>();

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
