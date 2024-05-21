using System;
using System.Net.Mime;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Configuration.Base;

namespace Persistence.Configuration;

public class ClientConfiguration : BaseEntityConfiguration<Client, string>
{
    public override void EntityConfigure(EntityTypeBuilder<Client> builder)
    {
        builder.Property(a => a.Name).IsRequired();
        builder.HasIndex(a => a.Name);
        builder.HasMany(e=>e.Senders)
            .WithOne(e=>e.Client)
            .HasForeignKey(e => e.ClientId);
        
        builder.HasMany(e=>e.SMSs)
            .WithOne(e=>e.Client)
            .HasForeignKey(e => e.ClientId);
    }
}
