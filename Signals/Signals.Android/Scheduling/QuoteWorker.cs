using System;
using Android.Content;
using Android.Runtime;
using AndroidX.Work;
using Microsoft.Extensions.DependencyInjection;
using Signals.ApplicationLayer.Abstract;

namespace Signals.Android.Scheduling;

public class QuoteWorker : Worker
{
    public QuoteWorker(IntPtr javaReference, JniHandleOwnership transfer) 
        : base(javaReference, transfer) { }

    public QuoteWorker(Context context, WorkerParameters workerParams) 
        : base(context, workerParams) { }

    public override Result DoWork()
    {
        var services = new ServiceCollection();
        var provider = App.ConfigureServices(services);
        var service = provider.GetRequiredService<IPriceRefreshService>();

        service.UpdateWatchlistPrices();
        service.UpdateHoldingPrices();
        service.UpdateIndexPrices();
        
        return Result.InvokeSuccess();
    }
}