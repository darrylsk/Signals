using System.Text.Json.Serialization;
using Signals.CoreLayer.Abstract;

namespace Signals;

public class AppConfig : IAppConfig
{
    [JsonPropertyName("quoteServiceToken")]
    public string Token { get; set; } = "";
    
    [JsonPropertyName("networkDataPermission")]
    public bool UsePhoneData { get; set; }
}
