using Domain.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Providers.Update
{
    public class UpdateProviderResponse
    {
        public UpdateProviderResponse()
        {
        }
        public UpdateProviderResponse(Provider provider)
        {
            Id = provider.Id;
            Name = provider.Name;
            BaseURL = provider.BaseUrl;
            IsDefault = provider.IsDefault;
            IsEnableWebhook = provider.IsEnableWebhook;
            WebhookURL = provider.WebhookURL;
            IsCheckManualStatus = provider.IsCheckManualStatus;
            CheckSMSStatusTime = provider.CheckSMSStatusTime;
            IsUndeliveredMessageRetry = provider.IsUndeliveredMessageRetry;
            UndeliveredMessageRetiresCount = provider.UndeliveredMessageRetiresCount;
            CreatedDate = provider.CreatedDate;
            CreatedBy = provider.CreatedBy;
            ModifiedDate = provider.ModifiedDate;
            ModifiedBy = provider.ModifiedBy;
        }
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
}
