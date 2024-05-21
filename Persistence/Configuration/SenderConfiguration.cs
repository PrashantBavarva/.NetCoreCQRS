using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Configuration.Base;

namespace Persistence.Configuration;

public class SenderConfiguration : BaseEntityConfiguration<Sender, string>
{
    public override void EntityConfigure(EntityTypeBuilder<Sender> builder)
    {
        builder.Property(a => a.Name).IsRequired();
        builder.HasIndex(a => a.Name);
        
        builder.HasMany(e=>e.Clients)
            .WithOne(e=>e.Sender)
            .HasForeignKey(e => e.SenderId);
        
        builder.HasMany(e=>e.SMSs)
            .WithOne(e=>e.Sender)
            .HasForeignKey(e => e.SenderId).OnDelete(DeleteBehavior.Restrict);
    }
}