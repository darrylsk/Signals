using Signals.InfrastructureLayer.Abstract;
using System;
using System.IO;
using System.Text.Json;

namespace Signals.InfrastructureLayer.FileService;

public class ConfigurationService : FileService, IConfigurationService
{
    private readonly string _configPath;

    public ConfigurationService()
    {
        // Get the LocalApplicationData folder (cross-platform)
        string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        // Create a subfolder for your app
        string appFolder = Path.Combine(localAppData, "Signals");
        Directory.CreateDirectory(appFolder); // Ensure the folder exists
                                              // Define the config file path
        _configPath = Path.Combine(appFolder, "config.json");
    }

    public AppConfig LoadConfig()
    {
        try
        {
            if (File.Exists(_configPath))
            {
                var json = File.ReadAllText(_configPath);
                return JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
            }
        }
        catch (Exception ex)
        {
            // Log the error (in a real app, use a logging framework)
            Console.WriteLine($"Failed to load config: {ex.Message}");
        }
        // Return default config if file doesn’t exist or fails to load
        return new AppConfig();
    }

    public void SaveConfig(AppConfig config)
    {
        try
        {
            string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_configPath, json);
        }
        catch (Exception ex)
        {
            // Log the error
            Console.WriteLine($"Failed to save config: {ex.Message}");
        }
    }
}
