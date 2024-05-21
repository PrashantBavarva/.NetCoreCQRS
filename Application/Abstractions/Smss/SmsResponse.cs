using Domain.Enums;

namespace Application.Abstractions.Smss;

public class SmsResponse
{
    public bool Success { get; set; }
    public string Ref { get; set; }
    public string Provider { get; set; }
    public SmsStatus Status { get; set; }
}