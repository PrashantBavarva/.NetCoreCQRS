using Domain.Abstractions;
using Domain.Entities;

namespace Domain.SmsMessages.Events;

public record SmsCreatedEvent(SMS Sms):IDomainEvent;