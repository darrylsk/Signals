using System;
using System.Text.Json.Serialization;

namespace Signals.InfrastructureLayer.QuotationService.FinnhubQuotationService;

public class FinnhubCompanyProfileClientObject
{
    [JsonPropertyName("country")]public string Country { get; set; }
    [JsonPropertyName("currency")]public string Currency { get; set; }
    [JsonPropertyName("exchange")]public string Exchange { get; set; }
    [JsonPropertyName("name")]public string Name { get; set; }
    [JsonPropertyName("ticker")]public string Symbol { get; set; }
    [JsonPropertyName("ipo")]public DateTime IpoDate { get; set; }
    [JsonPropertyName("marketCapitalization")]public decimal MarketCapitalization { get; set; }
    [JsonPropertyName("shareOutstanding")]public decimal SharesOutstanding { get; set; }
    [JsonPropertyName("logo")]public string Logo { get; set; }
    [JsonPropertyName("phone")]public string Phone { get; set; }
    [JsonPropertyName("weburl")]public string WebUrl { get; set; }
    [JsonPropertyName("finnhubIndustry")]public string Industry { get; set; }
}