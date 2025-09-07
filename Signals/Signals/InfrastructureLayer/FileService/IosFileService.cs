using System;
using System.IO;
using Signals.InfrastructureLayer.Abstract;

namespace Signals.InfrastructureLayer.FileService;

public class IosFileService : FileService, IFileService
{
    public override string GetUserAccessibleFolder()
    {
        // Use Documents directory, accessible via Files app
        var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var appPath = Path.Combine(documentsPath, "SignalsApp");
        Console.WriteLine($"IosUserAccessiblePath: {appPath}");
        return appPath;
    }

    /*public string CreateFolder(string path)
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
    }*/
}