using Domain.Entities.Base;

namespace Domain.Entities;

public class ClientSender 
{
    public ClientSender(string senderId)
    {
        SenderId = senderId;
    }
    public string ClientId { get; set; }
    public Client Client { get; set; }
    public string SenderId { get; set; }
    public Sender Sender { get; set; }

    public string CreatedBy { get; set; } = "";
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}