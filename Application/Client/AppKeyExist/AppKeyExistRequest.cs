using LanguageExt.Common;

namespace Application.Client.AppKeyExist;

public record AppKeyExistRequest (string AppKey): ICommand<AppKeyExistResponse>;

public class AppKeyExistHandler : ICommandHandler<AppKeyExistRequest, AppKeyExistResponse>
{
    private readonly IClientRepository _clientRepository;

    public AppKeyExistHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }
    public async Task<Result<AppKeyExistResponse>> Handle(AppKeyExistRequest request, CancellationToken cancellationToken)
    {
        var result = await _clientRepository.AppKeyTakenAsync(request.AppKey, cancellationToken);
        return new AppKeyExistResponse(result);
    }
}
public class AppKeyExistResponse
{
    public AppKeyExistResponse(bool exist)
    {
        Exist= exist;
        Message= exist ? "AppKey is taken" : "AppKey is not taken";
    }
    public bool Exist { get; set; }
    public string Message { get; set; }
    
}
