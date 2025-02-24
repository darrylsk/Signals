using System;

namespace Signals.CoreLayer.Entities;

public class WatchlistItem : StockItem
{
    public WatchlistItem()
    {

    }
    public WatchlistItem(string symbol, string exchange, string name, string currency) : base(symbol, exchange, name, currency)
    {
    }
}