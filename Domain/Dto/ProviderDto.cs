using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto;

public class ProviderDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string BaseURL { get; set; }
    public bool IsDefault { get; set; }
    public bool IsEnableWebhook { get; set; }
    public string WebhookURL { get; set; }
    public bool IsCheckManualStatus { get; set; }
    public int CheckSMSStatusTime { get; set; }
    public bool IsUndeliveredMessageRetry { get; set; }
    public int UndeliveredMessageRetiresCount { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedBy { get; set; }
}
