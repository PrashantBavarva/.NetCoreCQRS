using Common.Entities.Interfaces;
using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.Base;
using Domain.Enums;
using Domain.SmsMessages.Events;
using Mapster;
using ValidationEngine.Interfaces;

namespace Domain.SmsMessages;

public class SMS : BaseEntity<string>, ICreate, ISetting//, IRegister
{
    private List<SMSHistory> _histories = new();


    public SMS()
    {
        CreatedDate = DateTime.UtcNow;
        SetStatus(SmsStatus.New);
    }

    public SMS(string senderId, string message, string recipient, string clientId, string createdBy) : this()
    {
        SenderId = senderId;
        Message = message;
        Recipient = recipient;
        ClientId = clientId;
        CreatedBy = createdBy;
    }

    #region Properties

    public string? RefId { get; set; }
    public string Message { get; set; }
    public string Recipient { get; set; }
    public string SenderId { get; set; }
    public Sender Sender { get; set; }
    
    public string CreatedBy { get; set; }
    public SmsStatus Status { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string Error { get; set; }
    public string Provider { get; set; }
    public string ClientId { get; set; }
    public Client Client { get; set; }
    public IReadOnlyCollection<SMSHistory> Histories => _histories.AsReadOnly();

    #endregion

    public void SetStatus(SmsStatus status)
    {
        Status = status;
        if(status== SmsStatus.InProgress)
            this.AddDomainEvent(new SmsStatusIsInprogressEvent(this));
        _histories.Add(new(status));
    }
    public void SetFailed(string error,string provider)
    {
        Status= SmsStatus.Failed;
        _histories.Add(new(Status,error,provider));
        Error = error;
    }

    public void SetRefId(string refId)
    {
        RefId = refId;
        _histories.Add(new(SmsStatus.SentToProvider));
    }

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SMS, SmsMessageContract>()
            .TwoWays()
            .Map(dest => dest.Body, src => src.Message);
    }

    public void SetProvider(string provider)=>Provider = provider;
}