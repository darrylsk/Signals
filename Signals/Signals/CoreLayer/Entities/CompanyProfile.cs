using System;
using Signals.CoreLayer.Entities.Base;

namespace Signals.CoreLayer.Entities;

public class CompanyProfile : Entity
{
    public string Country { get; set; }
    public string Currency { get; set; }
    public string Exchange { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public DateTime IpoDate { get; set; }
    public decimal MarketCapitalization { get; set; }
    public decimal SharesOutstanding { get; set; }
    public string Logo { get; set; }
    public string Phone { get; set; }
    public string WebUrl { get; set; }
    public string Industry { get; set; }
}