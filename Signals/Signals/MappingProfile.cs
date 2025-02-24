using AutoMapper;
using Signals.CoreLayer.Entities;
using Signals.ViewModels;

namespace Signals;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<WatchlistItem, WatchlistItemDetailViewModel>().ReverseMap();
        // CreateMap<WatchlistItem, WatchListItemHeaderViewModel>().ReverseMap();
    }
    
}