using Domain.Abstractions;

namespace Domain.Providers.Events;

public record ProviderUpdatedEvent(string ProviderId):IDomainEvent;