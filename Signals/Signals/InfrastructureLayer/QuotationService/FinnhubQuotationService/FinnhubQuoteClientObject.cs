using System.Text.Json.Serialization;

namespace Signals.InfrastructureLayer.QuotationService.FinnhubQuotationService;

public class FinnhubQuoteClientObject
{
    // Attempting to use System.Text.JSON instead ot Newtonsoft Json for efficiency purposes
    [JsonPropertyName("pc")] public decimal? PreviousDayClosingPrice { get; set; }
    [JsonPropertyName("o")] public decimal? CurrentDayOpeningPrice { get; set; }
    [JsonPropertyName("h")] public decimal? CurrentDayHighPrice { get; set; }
    [JsonPropertyName("l")] public decimal? CurrentDayLowPrice { get; set; }
    [JsonPropertyName("c")] public decimal? LatestQuotedPrice { get; set; }
    [JsonPropertyName("t")] public long Timestamp { get; set; }
}