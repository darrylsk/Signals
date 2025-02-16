namespace Signals.InfrastructureLayer.Abstract;

public interface IFileService
{
    string GetRoamingAppDataFolder();
    string GetLocalAppDataFolder();
    string GetCommonDataFolder();
    string GetBackupFile();
    string CreateFolder(string folderPath);
}
