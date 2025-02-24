﻿using AutoMapper;
using Signals.CoreLayer.Entities;
using Signals.InfrastructureLayer.QuotationService.FinnhubQuotationService;
using Signals.ViewModels;

namespace Signals;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<WatchlistItem, WatchlistItemPageViewModel>().ReverseMap();
        CreateMap<WatchlistItem, FinnhubQuoteClientObject>().ReverseMap();
        // CreateMap<WatchlistItem, WatchListItemHeaderViewModel>().ReverseMap();
    }
    
}