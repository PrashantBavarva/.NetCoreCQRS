using Application.Abstractions.Smss;
using Domain.Contracts;
using LanguageExt.Common;

namespace Application.Abstractions;

public interface ISmsSender
{
    Task<Result<SmsResponse>> SendAsync(string recipients, string body, string senderName, long messageId);
    Task<Result<SmsResponse>> SendAsync(SmsMessageContract smsMessage);
    Task<Result<SmsResponse>> CheckSmsStatus(string refId);
}