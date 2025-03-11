using System;
using SQLite;

namespace Signals.CoreLayer.Entities;

public class Holding : StockItem
{
    private double _trailingStop;

    public Holding()
    { }

    public Holding(string symbol, string exchange, string name, string currency) : base(symbol, exchange, name, currency)
    { }
    
    public int QuantityHeld { get; set; }
    public DateTime? WhenPurchased { get; set; }
    public decimal? PeakPriceSincePurchase { get; set; }
    public decimal? HighTargetPrice { get; set; }
    public decimal? LowTargetPrice { get; set; }

    public bool UseTrailingStop { get; set; }

    /// <summary>
    /// Default value for a trailing stop, if used
    /// </summary>
    public double TrailingStop
    {
        get => _trailingStop;
        set
        {
            _trailingStop = value switch
            {
                < 0.05 => 0.05,
                > 0.95 => 0.95,
                _ => value
            };
        }
    }
}