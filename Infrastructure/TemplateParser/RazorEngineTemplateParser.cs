using System.Collections.Concurrent;
using Application.Abstractions;
using Common.DependencyInjection.Interfaces;
using Common.Logging;
using Microsoft.AspNetCore.Routing.Template;
using RazorEngineCore;


namespace Infrastructure.TemplateParser;

/// <summary>
/// This class is used to parse razor templates
/// It should be register as singleton in the DI container 
/// </summary>
public class RazorEngineTemplateParser : ITemplateParser, ISingleton
{
    private readonly ILoggerAdapter<RazorEngineTemplateParser> _logger;
    private readonly ConcurrentDictionary<string, IRazorEngineCompiledTemplate> _templateCache = new();
    public RazorEngineTemplateParser(ILoggerAdapter<RazorEngineTemplateParser> logger)
    {
        _logger = logger;
    }

    public async Task<string> ParseAsync( string key,string content, object model)=>
        await RenderTemplate(key, content, model);


    public async Task<string> LoadAndParseAsync( string key,string filePath, object model)
    {
        var templateContent =await File.ReadAllTextAsync(filePath);
        return await ParseAsync(key,templateContent,model);
    }
    private async Task<string> RenderTemplate(string key,string template, object model)
    {
        var compiledTemplate = _templateCache.GetOrAdd(key, i =>
        {
            _logger.LogDebug("Compiling template {TemplateKey}", key);
            var razorEngine = new RazorEngine();
            return razorEngine.Compile(template);
        });
        return await compiledTemplate.RunAsync(model);
    }
}