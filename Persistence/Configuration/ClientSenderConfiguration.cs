using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class ClientSenderConfiguration : IEntityTypeConfiguration<ClientSender>
{
    public void Configure(EntityTypeBuilder<ClientSender> builder)
    {
        builder.HasKey(e => new { e.SenderId, e.ClientId });
        
        builder.HasOne(e=>e.Client)
            .WithMany(e=>e.Senders)
            .HasForeignKey(e => e.ClientId);
        
        builder.HasOne(e=>e.Sender)
            .WithMany(e=>e.Clients)
            .HasForeignKey(e => e.SenderId);

    }
}