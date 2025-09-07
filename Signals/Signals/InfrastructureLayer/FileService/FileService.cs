using Signals.InfrastructureLayer.Abstract;
using System;
using System.IO;

namespace Signals.InfrastructureLayer.FileService;

public class FileService : IFileService
{
    /// <summary>
    /// Retrieves the app data folder for a user in a corporate network environment, and it not likely
    /// to be useful for this purpose.  Todo: remove this function from the interface.
    /// </summary>
    /// <returns></returns>
    public string GetRoamingAppDataFolder()
    {
        var appFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        return appFolder;
    }

    /// <summary>
    /// Retrieves the folder used to store data for a certain user on a single machine, that will not be
    /// replicated to other machines on a network.
    /// </summary>
    /// <returns></returns>
    public string GetLocalAppDataFolder()
    {
        // var appLocalFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var appLocalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        return appLocalFolder;
    }

    /// <summary>
    /// Retrieves the folder used to store program data that is available to all users on a single machine.
    /// Writing to this folder requires elevated privileges, so this is probably not suitable for mobile
    /// applications.
    /// </summary>
    /// <returns></returns>
    public string GetCommonDataFolder()
    {
        var folder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        return folder;
    }

    public string CreateFolder(string folderPath)
    {
        var folder = Directory.CreateDirectory(folderPath);
        return folder.FullName;
    }

    public virtual string GetUserAccessibleFolder()
    {
        return GetLocalAppDataFolder();
    }
}