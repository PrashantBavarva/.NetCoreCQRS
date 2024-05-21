using Domain.Entities;
using Domain.Providers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Configuration.Base;

namespace Persistence.Configuration;

public class ProviderConfiguration : BaseEntityConfiguration<Provider, string>
{
    public override void EntityConfigure(EntityTypeBuilder<Provider> builder)
    {
        builder.Property(a => a.Name).IsRequired();
        builder.HasIndex(a => a.Name);
    }
}