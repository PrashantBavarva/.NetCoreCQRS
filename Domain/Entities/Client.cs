using Domain.Entities.Base;
using Domain.SmsMessages;

namespace Domain.Entities;

public class Client : BaseEntityAudit<string>
{
    private Client()
    {
        
    }

    public Client(string name,string appKey)
    {
        Name = name;
        AppKey = appKey;
    }

    public string Name { get; set; }
    public string AppKey { get; set; } 

    public HashSet<ClientSender> Senders { get; set; } = new();
    public HashSet<SMS> SMSs { get; set; } = new();

    public void AddSenders(List<string> requestSenders)
    {
        Senders = requestSenders.Select(s => new ClientSender(s)).ToHashSet();
    }
}