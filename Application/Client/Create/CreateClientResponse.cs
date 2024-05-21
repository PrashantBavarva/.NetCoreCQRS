namespace Application.Client.Create;

public class CreateClientResponse
{
    public CreateClientResponse()
    {
        
    }
    public CreateClientResponse(Domain.Entities.Client client)
    {
        Name = client.Name;
        AppKey = client.AppKey;
        Senders = client.Senders.Select(s => s.SenderId).ToList();
    }
    
    public string Name { get; set; }
    public string AppKey { get; set; }
    public List<string> Senders { get; set; }
}