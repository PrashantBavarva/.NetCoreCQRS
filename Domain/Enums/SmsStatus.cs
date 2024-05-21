using Ardalis.SmartEnum.SystemTextJson;
using Ardalis.SmartEnum;
using System.Text.Json.Serialization;

namespace Domain.Enums;

//public enum SmsStatus
//{
//    New,
//    InProgress,
//    Success,
//    Failed
//}

[JsonConverter(typeof(SmartEnumNameConverter<SmsStatus, int>))]
public class SmsStatus : SmartEnum<SmsStatus>
{
    public static readonly SmsStatus New = new(nameof(New), 0);
    public static readonly SmsStatus InProgress = new(nameof(InProgress), 1);
    public static readonly SmsStatus Success = new(nameof(Success), 2);
    public static readonly SmsStatus Failed = new(nameof(Failed), 3);
    public static readonly SmsStatus SentToProvider = new(nameof(SentToProvider), 4);
    public static readonly SmsStatus UnDelivered = new(nameof(UnDelivered), 5);

    private SmsStatus(string name, int value) : base(name, value)
    {
    }
}

