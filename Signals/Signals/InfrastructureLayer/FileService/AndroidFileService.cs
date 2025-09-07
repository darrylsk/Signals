using System;
using System.IO;
using Signals.InfrastructureLayer.Abstract;

namespace Signals.InfrastructureLayer.FileService;

/// <summary>
/// This is the code that Grok generates for app-specific storage on Android, but I don't think it's
/// necessary because the LocalAppDataFolder may already be accessible to the user.
/// </summary>
public class AndroidFileService : FileService, IFileService
{
    public override string GetUserAccessibleFolder()
    {
        // Use app-specific external storage (no permissions needed on Android 10+)
        // var extPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).Path;
        var dcimPath = "/storage/emulated/0/DCIM";
        // var externalPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        // var appPath = Path.Combine(externalPath, "SignalsApp");
        Console.WriteLine($"AndroidUserAccessiblePath: {dcimPath}");
        return dcimPath;
    }

    public string CreateFolder(string path)
    {
        try
        {
            Directory.CreateDirectory(path);
            Console.WriteLine($"Folder created: {path}");
            return path;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Folder creation failed: {ex.Message}");
            throw;
        }
    }
}