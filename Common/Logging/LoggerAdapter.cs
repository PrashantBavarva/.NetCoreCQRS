using System;
using Common.DependencyInjection.Interfaces;
using Microsoft.Extensions.Logging;

namespace Common.Logging;

public class LoggerAdapter<TType>:ILoggerAdapter<TType>,ITransient
{
    private readonly ILogger<TType> _logger;
    private string _serviceName;
    public LoggerAdapter(ILogger<TType> logger)
    {
        _logger = logger;
        _serviceName = $"[{typeof(TType).Name}]";
    }

    private string SetMessageWithServiceName(string message)
    {
        if (string.IsNullOrEmpty(_serviceName))
            return message;
        return string.Format("{0} {1}", _serviceName, message);
    }

    public void LogInformation(string message, params object?[] args)
    {
        _logger.LogInformation(SetMessageWithServiceName(message), args);
    }

    public void LogError(Exception? exception, string? message, params object?[] args)
    {
        _logger.LogError(exception, SetMessageWithServiceName(message), args);
    }    
    public void LogError( string? message, params object?[] args)
    {
        _logger.LogError( SetMessageWithServiceName(message), args);
    }

    public void LogWarning(Exception? exception, string? message, params object?[] args) {
        _logger.LogWarning(exception, SetMessageWithServiceName(message), args);
    }

    public void LogWarning(string? message, params object?[] args) {
        _logger.LogWarning( SetMessageWithServiceName(message), args);
    }

    public void LogDebug(string message) {
        _logger.LogDebug( SetMessageWithServiceName(message));
    }    
    public void LogDebug(string message, params object[] args) {
        _logger.LogDebug( SetMessageWithServiceName(message),args);
    }
}