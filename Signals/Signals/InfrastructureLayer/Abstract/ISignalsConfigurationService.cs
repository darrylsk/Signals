namespace Signals.InfrastructureLayer.Abstract;

public interface ISignalsConfigurationService
{
    public AppConfig LoadConfig();

    public void SaveConfig(AppConfig appConfig);
}
