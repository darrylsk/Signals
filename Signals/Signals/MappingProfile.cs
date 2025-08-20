using AutoMapper;
using Signals.CoreLayer.Entities;
using Signals.InfrastructureLayer.Abstract;
using Signals.InfrastructureLayer.QuotationService.FinnhubQuotationService;
using Signals.ViewModels;

namespace Signals;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<WatchlistItem, WatchlistItemPageViewModel>().ReverseMap();
        CreateMap<WatchlistItem, IQuoteClientObject>().ReverseMap();
        CreateMap<WatchlistItem, BuyOrSellDialogViewModel>().ReverseMap();
        CreateMap<Holding, HoldingsItemPageViewModel>().ReverseMap();
        CreateMap<FinnhubCompanyProfileClientObject, CompanyProfile>();
        CreateMap<FinnhubQuoteClientObject, StockItem>();
        CreateMap<StockItem, IndexItem>();
        CreateMap<StockItem, WatchlistItem>();
        CreateMap<StockItem, Holding>();
        CreateMap<StockItem, BuyOrSellDialogViewModel>();
        CreateMap<Settings, SettingsPageViewModel>().ReverseMap();
    }
}