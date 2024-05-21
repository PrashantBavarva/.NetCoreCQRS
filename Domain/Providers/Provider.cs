using Domain.Entities.Base;
using Domain.Providers.Events;
using Mapster;

namespace Domain.Providers;

public class Provider : BaseEntityAudit<string>
{
    public Provider()
    {
        
    }

    public Provider(string name,string baseUrl):this()
    {
        Name = name;
        BaseUrl = baseUrl;
    }
    public string Name { get;private set; }
    public string BaseUrl { get;private set; }
    public bool IsDefault { get; set; }
    public bool IsEnableWebhook { get; set; }
    public string WebhookURL { get; set; }
    public bool IsCheckManualStatus {  get; set; }
    public int CheckSMSStatusTime { get; set; }
    public bool IsUndeliveredMessageRetry { get; set; }
    public int UndeliveredMessageRetiresCount {  get; set; }
    
    public string ApplicationKey { get; set; }
    public string ApplictionSecert { get; set; }
    public Dictionary<string,string> Headers { get; set; }
    public Dictionary<string,object> Data { get; set; }

    
    
    public void SetBaseUrl(string baseUrl)=>BaseUrl = baseUrl;

    public void SetIsDefault(bool isDefault)
    {
        if(isDefault)
            this.AddDomainEvent(new IsDefaultProviderEvent(this.Id));
        IsDefault = isDefault;
    }
    public void SetIsEnableWebhook(bool isEnableWebhook)=>IsEnableWebhook = isEnableWebhook;

    public static Provider Update<T>(T dto,Provider source)
    {
        return dto.Adapt<T,Provider>(source);
    }
}
