using MassTransit;

namespace Domain.Contracts;

[EntityName( "sms-created")]
public record SmsMessageContract :ISmsContract
{
    public SmsMessageContract()
    {
    }

    public SmsMessageContract(string senderId, string body, string recipient)
    {
        SenderId = senderId;
        Body = body;
        Recipient = recipient;
    }

    public SmsMessageContract(string senderId, string body, string recipient, string clientId, string createdBy)
        : this(senderId, body, recipient)
    {
        ClientId = clientId;
        CreatedBy = createdBy;
    }

    public SmsMessageContract(string senderId, string body, string recipient, string clientId, string createdBy,
        string dlr)
        : this(senderId, body, recipient, clientId, createdBy) => Dlr = dlr;

    public string Dlr { get; set; }
    public string ClientId { get; set; }
    public string CreatedBy { get; set; }

    public string Id { get; set; }
    public string SenderId { get; set; }
    public string Body { get; set; }
    public string Recipient { get; set; }
    public long MessageId { get; set; }
    
    public void SetDlr(string dlr)=> Dlr = dlr;

    public void SetId(string messageId)=> Id = messageId;
}

// public record SmsMUnDeliveredContract(string SmsId);