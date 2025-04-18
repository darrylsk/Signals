﻿using System;
using System.IO;
using Signals.CoreLayer.Entities;
using Signals.InfrastructureLayer.Abstract;
using SQLite;

namespace Signals.InfrastructureLayer.Repository;

public class SignalsContext : ISignalsDbContext
{
    public SignalsContext(IFileService fileService)
    {
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
        
        var result = Connection.CreateTableAsync<Settings>().Result;
        var def = Connection.GetTableInfoAsync(result.ToString());
        // Guard: Insert the initial settings record only on creation.
        if (result != CreateTableResult.Created) return;
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