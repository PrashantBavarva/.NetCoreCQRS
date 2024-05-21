namespace Application.Abstractions;

public interface ITemplateParser
{
    Task<string> ParseAsync(string key,string content, object model);
    Task<string> LoadAndParseAsync(string key, string filePath, object model);
}