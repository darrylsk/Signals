namespace Signals.CoreLayer.Entities;

public class IndexItem : StockItem
{
    /// <summary>
    /// EF Core requires a parameterless constructor
    /// </summary>
    public IndexItem() { }
    public IndexItem(string symbol, string exchange, string name, string currency) : base(symbol, exchange, name, currency)
    {
    }
}