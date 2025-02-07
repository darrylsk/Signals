using System;

namespace Signals.Data;

public class Holding : StockItem
{
    public Holding()
    { }

    public Holding(string symbol, string exchange, string name, string currency) : base(symbol, exchange, name, currency)
    { }

    public int QuantityHeld { get; set; }
    public DateTime? WhenPurchased { get; set; }
    public decimal? PeakPriceSincePurchase { get; set; }
    public decimal? HighTargetPrice { get; set; }
    public decimal? LowTargetPrice { get; set; }
}