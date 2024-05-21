using Domain.Entities.Base;
using Domain.SmsMessages;

namespace Domain.Entities;

public class Sender : BaseEntityAudit<string>
{
    public string Name { get; set; }
    public HashSet<ClientSender> Clients { get; set; } = new();
    public HashSet<SMS> SMSs { get; set; } = new();
}