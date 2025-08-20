using System.Collections.Generic;
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
        var dbPath = Path.Combine(databaseFolder, "Signals.db");
        Connection = new SQLiteAsyncConnection(dbPath);

        // Create the tables if they don't already exist.
        Connection.CreateTableAsync<CompanyProfile>().Wait();
        Connection.CreateTableAsync<WatchlistItem>().Wait();
        Connection.CreateTableAsync<Holding>().Wait();
        Connection.CreateTableAsync<IndexItem>().Wait();
        Connection.CreateTableAsync<TradingJournal>().Wait();
        var result = Connection.CreateTableAsync<Settings>().Result;

        // SQLite useful functions
        // sqlite3.exe is a command line interface for sqlite and allows you to examine the database and run queries
        // C:\Program Files\SQLite contains command line executable utilities for SQLite, including sqlite3.exe
        // C:\Users\[user]\Downloads\Sqlite contains a few useful utilities (could be anywhere)
        // C:\Users\[user]\AppData\Local\Signals contains the database file
        // .open C:\Users\[user]\AppData\Local\Signals\Signals.db opens the database connection 
        // .schema [table name] displays the table definition (all tables if table name parameter is omitted)
        // .mode sets the output mode (list, column, box, table or markdown)
        // .columns n n n ... sets column widths
        // .help displays hints
        // .exit closes SQLite.exe

        // var settingsDef = Connection.GetTableInfoAsync("Settings").Result;
        // var watchlistItemDef = Connection.GetTableInfoAsync("WatchListItem").Result;
        // var holdingDef = Connection.GetTableInfoAsync("Holding").Result;
        // var companyProfileDef = Connection.GetTableInfoAsync("CompanyProfile").Result;

        // Guard: Continue beyond here only on database creation.

        if (result != CreateTableResult.Created) return;

        // Everything below this point is only executed on database creation.

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

        // Create the fixed list of index ETF items.
        var profiles = new CompanyProfile[]
        {
            new CompanyProfile() { Name = "SPDR S&P 500 ETF", Symbol = "SPY", Currency = "USD" },
            new CompanyProfile() { Name = "Invesco QQQ ETF Trust (NASDAQ)", Symbol = "QQQ", Currency = "USD" },
            new CompanyProfile()
                { Name = "SPDR Dow Jones Industrial Average ETF Trust", Symbol = "DIA", Currency = "USD" },
            new CompanyProfile() { Name = "iShares Russell 2000 ETF", Symbol = "IWM", Currency = "USD" },
            new CompanyProfile() { Name = "Vanguard Total Stock Market ETF", Symbol = "VTI", Currency = "USD" },
        };
        
        Connection.InsertAllAsync((IEnumerable<CompanyProfile>)profiles).Wait();
        var indexItems = new IndexItem[]
        {
            new IndexItem("SPY", "New York", "SPDR S&P 500 ETF", "USD"),
            new IndexItem("QQQ", "New York", "Invesco QQQ ETF Trust (NASDAQ)", "USD"),
            new IndexItem("DIA", "New York", "SPDR Dow Jones Industrial Average ETF Trust", "USD"),
            new IndexItem("IWM", "New York", "iShares Russell 2000 ETF", "USD"),
            new IndexItem("VTI", "New York", "Vanguard Total Stock Market ETF", "USD"),
        };
        Connection.InsertAllAsync((IEnumerable<IndexItem>)indexItems).Wait();
    }

    public SQLiteAsyncConnection Connection { get; }
}

public interface ISignalsDbContext
{
    SQLiteAsyncConnection Connection { get; }
}