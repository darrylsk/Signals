using System.Text.Json.Serialization;
using AutoMapper;
using Signals.CoreLayer.Abstract;

namespace Signals;

public class AppConfig : IAppConfig
{
    [JsonPropertyName("quoteServiceToken")]
    public string Token { get; set; } = "";
}
