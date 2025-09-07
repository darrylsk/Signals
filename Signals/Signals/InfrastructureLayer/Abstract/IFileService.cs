namespace Signals.InfrastructureLayer.Abstract;

public interface IFileService
{
    string GetRoamingAppDataFolder();
    string GetLocalAppDataFolder();
    string GetCommonDataFolder();
    string CreateFolder(string folderPath);
    string GetUserAccessibleFolder();
}
