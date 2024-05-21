using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Configuration.Base;

namespace Persistence.SmsHistories;

public class SMSHistoryConfiguration : BaseEntityConfiguration<SMSHistory, string>
{
    public override void EntityConfigure(EntityTypeBuilder<SMSHistory> builder)
    {
        builder.Property(h => h.Error)
            .IsRequired(false);
        builder.HasIndex(h=>h.CreatedDate);
        builder.HasOne(e=>e.SMS)
            .WithMany(e=>e.Histories)
            .HasForeignKey(e => e.SMSId);
        
        
    }
}