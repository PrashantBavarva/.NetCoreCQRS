using Bogus;
using Domain.Providers;

namespace Application.Integration.Tests.Fakers.Providers
{
    public class ProviderFaker : Faker<Provider>
    {
        public ProviderFaker()
        {
            // RuleFor(p=>p.Id, f=>f.Random.Guid().ToString());
            RuleFor(p => p.Name, f => f.Name.Prefix());
            RuleFor(p => p.IsDefault, f => false);
            RuleFor(p => p.BaseUrl, f => f.Internet.Url());
            RuleFor(p => p.CreatedBy, f => "system");
            RuleFor(p => p.CreatedDate, f => f.Date.Past());

        }
    }
}
