using Domain.Entities.Base;

namespace Domain.Emails;

public class EmailTemplate : BaseEntity<string>
{
    public string TemplateName { get; set; }
    public string Template { get; set; }
    public string Lang { get; set; }
    public string ApplicationName { get; set; }
}