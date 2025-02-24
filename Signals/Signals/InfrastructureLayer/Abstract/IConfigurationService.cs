namespace Signals.InfrastructureLayer.Abstract;

public interface IConfigurationService : IFileService
{
    public AppConfig LoadConfig();

    public void SaveConfig(AppConfig appConfig);
}
