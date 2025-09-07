using System;
using SQLite;

namespace Signals.CoreLayer.Entities;

public class Holding : StockItem
{
    private double _trailingStop;

    public Holding()
    {
        if (IsTransient())
        {
            WhenLastPurchased = DateTime.UtcNow;
        }
    }

    public Holding(string symbol, string exchange, string name, string currency) : base(symbol, exchange, name,
        currency)
    {
        WhenLastPurchased = DateTime.UtcNow;
    }

    public int? UnitsHeld { get; set; }
    public int? CumulativeUnitsPurchased { get; set; }
    public DateTime WhenLastPurchased { get; set; }
    public decimal? PeakPriceSincePurchase { get; set; }
    public decimal? HighTargetPrice { get; set; }
    public decimal? LowTargetPrice { get; set; }
    public decimal? AveragePurchasePrice { get; set; }
    
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