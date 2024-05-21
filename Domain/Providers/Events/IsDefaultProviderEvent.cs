using Domain.Abstractions;

namespace Domain.Providers.Events;

public record IsDefaultProviderEvent(string ProviderId):IDomainEvent;