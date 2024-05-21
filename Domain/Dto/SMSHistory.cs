using Domain.Entities.Base;
using Domain.Enums;
using Domain.SmsMessages;

namespace Domain.Entities;

public class SMSHistory:BaseEntity<string>
{
    private SMSHistory()
    {
        CreatedDate = DateTime.UtcNow;
    }

    public SMSHistory(SmsStatus status) : this()
    {
        Status = status;
    }

    public SMSHistory(SmsStatus status, string error, string provider) : this(status)
    {
        Error = error;
    }

    public DateTime CreatedDate { get; private set; }
    public SmsStatus Status { get; private set; }
    public string Error { get; private set; }
    public string SMSId { get; private set; }
    public SMS SMS { get; private set; }
}