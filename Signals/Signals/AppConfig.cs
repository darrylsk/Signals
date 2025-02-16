using System.Text.Json.Serialization;

namespace Signals;

public class AppConfig
{
    [JsonPropertyName("quoteServiceToken")]
    public string Token { get; set; } = "";
}
