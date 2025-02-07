using System;
using System.IO;
using Signals.Data;
using SQLite;

namespace Signals.InfrastructureLayer.Repository.Base;

public class SignalsContext : ISignalsDbContext
{
    public SignalsContext()
    {
        var dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Signals.db");
        Connection = new SQLiteAsyncConnection(dbPath);

        Connection.CreateTableAsync<WatchlistItem>().Wait();
    }

    public SQLiteAsyncConnection Connection { get; }
}

public interface ISignalsDbContext
{
    SQLiteAsyncConnection Connection { get; }
}
