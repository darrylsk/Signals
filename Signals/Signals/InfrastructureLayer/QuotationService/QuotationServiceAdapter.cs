using System;
using System.Threading.Tasks;
using AutoMapper;
using Signals.CoreLayer.Entities;
using Signals.InfrastructureLayer.Abstract;
using Signals.InfrastructureLayer.QuotationService.FinnhubQuotationService;
using Signals.InfrastructureLayer.QuotationService.TiingoQuotationService;

namespace Signals.InfrastructureLayer.QuotationService;

public class QuotationServiceAdapter : IQuotationServiceAdapter
{
    public IFinnhubQuotationService FinnhubQuotationService { get; }
    public ITiingoQuotationService TiingoQuotationService { get; }
    public IMapper QuoteServiceMapper { get; }

    public QuotationServiceAdapter(
        IFinnhubQuotationService finnhubQuotationService,
        ITiingoQuotationService tiingoQuotationService,
        IMapper quoteServiceMapper
    )
    {
        FinnhubQuotationService = finnhubQuotationService;
        TiingoQuotationService = tiingoQuotationService;
        QuoteServiceMapper = quoteServiceMapper;
    }

    public QuotationServiceOptions ServiceOptionType { get; set; }

    public async Task<WatchlistItem?> GetQuoteAsync(string symbol)
    {
        switch (ServiceOptionType)
        {
            case QuotationServiceOptions.Finnhub:
                if (FinnhubQuotationService.ServiceSuspended)
                    return null;
                var quote = await FinnhubQuotationService.GetQuoteAsync(symbol);
                var watchlistItem =
                    QuoteServiceMapper
                        .Map<WatchlistItem>(quote); // Todo: need to verify this mapping in mapping profile
                return watchlistItem;
            case QuotationServiceOptions.Tiingo:
                throw new NotImplementedException();
            default:
                return null;
        }
    }

    public async Task<CompanyProfile?> GetProfileAsync(string symbol)
    {
        switch (ServiceOptionType)
        {
            case QuotationServiceOptions.Finnhub:
                var profile = await FinnhubQuotationService.GetProfileAsync(symbol);
                var companyProfile = QuoteServiceMapper.Map<CompanyProfile>(profile);
                return companyProfile;
            case QuotationServiceOptions.Tiingo:
                throw new NotImplementedException();
            default:
                return null;
        }
    }
}