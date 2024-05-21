namespace Application.Abstractions.Smss;

public class SmsRequest
{
    public string Id { get; set; }
    public string SenderId { get; set; }
    public string Body { get; set; }
    public string Recipient { get; set; }
    public long MessageId { get; set; }
}