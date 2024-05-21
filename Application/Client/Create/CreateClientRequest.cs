using FluentValidation;

namespace Application.Client.Create;

public class CreateClientRequest : ICommand<CreateClientResponse>
{
    public string Name { get; set; }
    public string AppKey { get; set; }
    public List<string> Senders { get; set; }
    
}
public class CreateClientRequestValidator : AbstractValidator<CreateClientRequest>
{
    public CreateClientRequestValidator(IClientRepository clientRepository)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.AppKey).NotEmpty().WithMessage("AppKey is required")
            .MustAsync(async (appKey, cancellationToken) =>
                !(await clientRepository.AppKeyTakenAsync(appKey, cancellationToken)))
            .WithMessage("AppKey is taken");
            ;
    }
}