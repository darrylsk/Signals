using System;

namespace Signals.Data;

public class WatchlistItem : StockItem
{
    public WatchlistItem()
    {
        
    }
    public WatchlistItem(string symbol, string exchange, string name, string currency) : base(symbol, exchange, name, currency)
    {
    }
}