using System.Text.Json.Serialization;
using Ardalis.SmartEnum;
using Ardalis.SmartEnum.SystemTextJson;

namespace Domain.Enums;

[JsonConverter(typeof(SmartEnumValueConverter<Status, int>))]
public class Status : SmartEnum<Status>
{
    public SmsStatus SmsStatus { get; }
    
    public static readonly Status Delivered = new(nameof(Delivered), 1, SmsStatus.Success);
    public static readonly Status Undelivered = new(nameof(Undelivered), 2, SmsStatus.UnDelivered);
    public static readonly Status Sent = new(nameof(Sent), 3, SmsStatus.InProgress);
    public static readonly Status Buffered = new(nameof(Buffered), 4, SmsStatus.SentToProvider);
    public static readonly Status Blocked = new(nameof(Blocked), 5, SmsStatus.Failed);
    public static readonly Status RejectedBySMSCenter = new(nameof(RejectedBySMSCenter), 16, SmsStatus.Failed);
    public static readonly Status InProgress = new(nameof(InProgress), 17, SmsStatus.InProgress);
    public static readonly Status Expired = new(nameof(Expired), 34, SmsStatus.Failed);
    public static readonly Status Unknown = new(nameof(Unknown), 1200, SmsStatus.Failed);

    private Status(string name, int value, SmsStatus smsStatus) : base(name, value)
    {
        SmsStatus = smsStatus;
    }
    
}