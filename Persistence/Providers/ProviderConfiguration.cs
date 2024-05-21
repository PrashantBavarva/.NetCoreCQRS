using System.Collections.Generic;
using Common.Extensions;
using Domain.Providers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Configuration.Base;

namespace Persistence.Providers;

public class ProviderConfiguration:BaseEntityConfiguration<Provider,string>
{
    public override void EntityConfigure(EntityTypeBuilder<Provider> builder)
    {
        builder.Property(p=>p.WebhookURL).IsRequired(false);
        builder.Property(p=>p.Data).IsRequired(false);
        builder.Property(p=>p.Headers).IsRequired(false);
        builder.Property(p=>p.ApplicationKey).IsRequired(false);
        builder.Property(p=>p.ApplictionSecert).IsRequired(false);
        
        
        builder.Property(e => e.Headers)
            .HasConversion(
                v => v.Serialize(),
                v => v.Deserialize<Dictionary<string,string>>()
            );
        builder.Property(e => e.Data)
            .HasConversion(
                v => v.Serialize(),
                v => v.Deserialize<Dictionary<string,object>>()
            );
    }
}