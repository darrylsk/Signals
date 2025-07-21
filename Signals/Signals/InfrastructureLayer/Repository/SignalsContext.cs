using System;
using System.IO;
using Signals.CoreLayer.Entities;
using Signals.InfrastructureLayer.Abstract;
using SQLite;

namespace Signals.InfrastructureLayer.Repository;

public class SignalsContext : ISignalsDbContext
{
    public SignalsContext(IFileService fileService)
    { 
        // The file service functions return the locations of the user data files on whatever device
        // the application is running on.
        // On Windows: C:\Users\[user]\AppData 
        // On Android: 
        // On iOS (iPhone): 
        var localAppData = fileService.GetLocalAppDataFolder();
        var databaseFolder = fileService.CreateFolder(Path.Combine(localAppData, "Signals"));
        var dbPath = Path.Combine(
            databaseFolder,
            "Signals.db");
        Connection = new SQLiteAsyncConnection(dbPath);

        // Create the tables if they don't already exist.
        Connection.CreateTableAsync<WatchlistItem>().Wait();
        Connection.CreateTableAsync<Holding>().Wait();
        Connection.CreateTableAsync<CompanyProfile>().Wait();
        // Connection.CreateTableAsync<TradingJournal>().Wait();
        
        var result = Connection.CreateTableAsync<Settings>().Result;
        
        var settingsDef = Connection.GetTableInfoAsync("Settings").Result;
        var watchlistItemDef = Connection.GetTableInfoAsync("WatchListItem").Result;
        var holdingDef = Connection.GetTableInfoAsync("Holding").Result;
        var companyProfileDef = Connection.GetTableInfoAsync("CompanyProfile").Result;
        
        // Guard: Continue beyond here only on database creation.
        
        if (result != CreateTableResult.Created) return;
        
        // Insert the initial settings record.
        var settings = new Settings
        {
            MetadataVersion = 1,
            DefaultUseTrailingStop = true,
            DefaultTrailingStop = .25,
            DefaultUseHighGainMultiplier = true,
            DefaultHighGainMultiplier = 1.5
        };
        Connection.InsertAsync(settings).Wait();
        Connection.UpdateAsync(settings).Wait();
    }

    public SQLiteAsyncConnection Connection { get; }
}

public interface ISignalsDbContext
{
    SQLiteAsyncConnection Connection { get; }
}