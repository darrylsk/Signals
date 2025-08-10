namespace Signals.InfrastructureLayer.Abstract;

public interface IQuoteClientObject
{
    public decimal? PreviousDayClosingPrice { get; set; }
    public decimal? CurrentDayOpeningPrice { get; set; }
    public decimal? CurrentDayHighPrice { get; set; }
    public decimal? CurrentDayLowPrice { get; set; }
    public decimal? LatestQuotedPrice { get; set; }
    long Timestamp { get; set; }
}