using System;
using Signals.CoreLayer.Entities.Base;

namespace Signals.Data;

public class StockItem: Entity
{
    public StockItem()
    {
        
    }
    protected StockItem(string symbol, string exchange, string name, string currency)
    {
        WhenCreated = DateTime.UtcNow;
        ExchangeName = exchange;
        Symbol = symbol;
        Name = name;
        CurrencyCode = currency;
        WhenCreated = DateTime.UtcNow;
        PreviousDayClosingPrice = (decimal)Random.Shared.NextDouble() * 1_000M;
        CurrentDayOpeningPrice = (decimal)Random.Shared.NextDouble() * 1_000M;
        CurrentDayHighPrice = CurrentDayOpeningPrice + (decimal)Random.Shared.NextDouble() * 100M;
        CurrentDayLowPrice = CurrentDayOpeningPrice - (decimal)Random.Shared.NextDouble() * 100M;
        var togler = Random.Shared.NextDouble();
        var switcher = togler > 0.5 ? 1 : -1;
        var variation = (decimal)(Random.Shared.NextDouble() * 50.0D * switcher);
        LatestQuotedPrice = CurrentDayOpeningPrice + variation;
        WhenLatestQuoteReceived = DateTime.UtcNow;
    }

    public string Symbol { get; set; }
    public string Name { get; set; }
    public string ExchangeName { get; set; }
    public DateTime WhenCreated { get; set; }
    public string CurrencyCode { get; set; }
    public decimal? PreviousDayClosingPrice { get; set; }
    public decimal? CurrentDayOpeningPrice { get; set; }
    public decimal? CurrentDayHighPrice { get; set; }
    public decimal? CurrentDayLowPrice { get; set; }
    public decimal? LatestQuotedPrice { get; set; }
    public DateTime? WhenLatestQuoteReceived { get; set; }


    #region Non data members

    public decimal? CurrentDayPriceChange => LatestQuotedPrice - CurrentDayOpeningPrice;
    public decimal? CurrentDayPercentChange => CurrentDayPriceChange / CurrentDayOpeningPrice;
    public string UpDownIndicator => CurrentDayPriceChange > 0 ? "\ue030" : "\ue028";
    public string ColourByTrend => CurrentDayPriceChange > 0 ? "Green" : "Red";

    #endregion Non data members
}