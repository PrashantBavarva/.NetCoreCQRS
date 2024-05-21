using Domain.Abstractions;
using Domain.Entities;

namespace Domain.SmsMessages.Events;

public record SmsStatusIsInprogressEvent(SMS Sms):IDomainEvent;