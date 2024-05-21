using Common.Logging;
using LanguageExt.Common;

namespace Application.Client.Create;

public class CreateClientCommandHandler : ICommandHandler<CreateClientRequest, CreateClientResponse>
{
    private readonly ILoggerAdapter<CreateClientCommandHandler> _loggerAdapter;
    private readonly IClientRepository _clientRepository;
    private readonly IAppUnitOfWork _unitOfWork;

    public CreateClientCommandHandler(ILoggerAdapter<CreateClientCommandHandler> loggerAdapter,
        IClientRepository clientRepository, IAppUnitOfWork unitOfWork)
    {
        _loggerAdapter = loggerAdapter;
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateClientResponse>> Handle(CreateClientRequest request,
        CancellationToken cancellationToken)
    {
        var client =new Domain.Entities.Client(request.Name,request.AppKey);
        
        client.AddSenders(request.Senders);
        await _clientRepository.AddAsync(client, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new CreateClientResponse(client);
    }
}