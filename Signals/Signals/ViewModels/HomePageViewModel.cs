using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Signals.ApplicationLayer.Abstract;
using Signals.ApplicationLayer.Services;
using Signals.CoreLayer.Entities;
using Signals.InfrastructureLayer.Abstract;

namespace Signals.ViewModels;

public partial class HomePageViewModel : PageViewModel
{
    public HomePageViewModel() : base("Home",
        "Main entry page (not sure if this will ever be useful - might remove)")
    {
    }

    public HomePageViewModel(
        IFileService fileService,
        IIndexItemService indexItemService,
        IQuotationServiceAdapter quotationService,
        IMapper mapper)
        : base("Home",
            "Main entry page (not sure if this will ever be useful - might remove)")
    {
        FileService = fileService;
        IndexItemService = indexItemService;
        QuotationService = quotationService;
        Mapper = mapper;
        PopulateFolderNames();
        LoadData();
    }

    [ObservableProperty] private string _applicationDataFolderName;
    [ObservableProperty] private string _applicationLocalDataFolderName;
    [ObservableProperty] private string _applicationCommonDataFolderName;
    [ObservableProperty] private IEnumerable<IndexItem> _indexEtfList;
    [ObservableProperty] private IndexItem _indexEtfItem;
    [ObservableProperty] private bool _isItemFound;
    [ObservableProperty] private string _symbol;
    public IFileService FileService { get; }
    public IIndexItemService IndexItemService { get; }
    public IQuotationServiceAdapter QuotationService { get; }
    public IMapper Mapper { get; }

    private void PopulateFolderNames()
    {
        ApplicationDataFolderName = $@"{FileService.GetRoamingAppDataFolder()}\Signals";
        ApplicationLocalDataFolderName = $@"{FileService.GetLocalAppDataFolder()}\Signals";
        ApplicationCommonDataFolderName = $@"{FileService.GetCommonDataFolder()}\Signals";
    }

    private async Task LoadData()
    {
        IndexEtfList = await IndexItemService.GetAll();
    }

    [RelayCommand]
    private async Task LookupSymbol(string symbol)
    {
        var profile = await QuotationService.GetProfileAsync(symbol);
        var quote = await QuotationService.GetQuoteAsync(symbol);
        IndexEtfItem = Mapper.Map<IndexItem>(quote);
        IndexEtfItem.Name = profile?.Name;
        IndexEtfItem.Symbol = profile?.Symbol;

        IsItemFound = true;
    }
}