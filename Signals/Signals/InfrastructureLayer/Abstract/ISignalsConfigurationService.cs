namespace Signals.InfrastructureLayer.Abstract;

public interface ISignalsConfigurationService
{
    public AppConfig GetConfig();

    public void SaveConfig(AppConfig appConfig);
}
