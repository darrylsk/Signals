using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Signals.ApplicationLayer.Abstract;
using Signals.CoreLayer.Entities;
using Signals.InfrastructureLayer.Abstract;

namespace Signals.ViewModels;

public partial class SettingsPageViewModel : PageViewModel
{
    private ISignalsConfigurationService SignalsConfigurationService { get; }
    public ISettingsService SettingsService { get; }
    public IMapper WatchlistMapper { get; }

    /// <summary>
    /// Design-time view model
    /// </summary>
    public SettingsPageViewModel() : base("Settings",
        "Configure settings and preferences")
    { }

    public SettingsPageViewModel(
        ISignalsConfigurationService signalsConfigurationService,
        ISettingsService settingsService,
        IMapper watchlistMapper
    ) : base("Settings",
        "Configure settings and preferences")
    {
        SignalsConfigurationService = signalsConfigurationService;
        SettingsService = settingsService;
        WatchlistMapper = watchlistMapper;
        InitializeConfiguration();
    }

    private AppConfig SignalsConfiguration { get; set; }

    private void InitializeConfiguration()
    {
        SignalsConfiguration = SignalsConfigurationService.LoadConfig();
        Key = SignalsConfiguration.Token;
    }

    public void SaveConfiguration()
    {
        SignalsConfigurationService.SaveConfig(SignalsConfiguration);
    }

    #region Settings Methods and Properties

    public async Task LoadSettings()
    {
        var settings = (await SettingsService.GetAll()).FirstOrDefault();
        if (settings! == null!) return;
        WatchlistMapper.Map(settings, this);
    }

    [RelayCommand]
    public async Task SaveSettings()
    {
        var settings = WatchlistMapper.Map<Settings>(this);
        if (settings! == null!) return;
        await SettingsService.Update(settings);
    }

    [ObservableProperty] private Guid _id;
    [ObservableProperty] private int _metadataVersion;
    [ObservableProperty] private bool _defaultUseHighGainMultiplier;
    [ObservableProperty] private double _defaultHighGainMultiplier;
    [ObservableProperty] private bool _defaultUseTrailingStop;
    [ObservableProperty] private double _defaultTrailingStop;
    [ObservableProperty] private bool _keyIsInEditMode;
    
    public string Key { get; set; }
    
    [RelayCommand]
    public void EditKey()
    {
        KeyIsInEditMode = true;
    }

    [RelayCommand]
    public async Task SaveKey()
    {
        SignalsConfiguration.Token = Key;
        SignalsConfigurationService.SaveConfig(SignalsConfiguration);
        KeyIsInEditMode = false;
    }

    [RelayCommand]
    public async Task CancelSaveKey()
    {
        KeyIsInEditMode = false;
    }

    #endregion
}