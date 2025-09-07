using System;
using Java.Util.Concurrent;
using Signals.InfrastructureLayer.FileService;

namespace Signals.Android.Scheduling;

using AndroidX.Work;
using InfrastructureLayer.Abstract;

public static class Scheduler
{
    private const string PlatformTag = "com.skmetrics.signals.android";

    internal static void ConfigureWorkManager()
    {
        try
        {
            // var appConfigService = new SignalsConfigurationService(fileService);
            // var appConfig = appConfigService.GetConfig();

            //var requiredNetworkType = appConfig.UsePhoneData ? NetworkType.Connected! : NetworkType.Unmetered!;
            var requiredNetworkType = NetworkType.NotRequired!;

            var constraints = new Constraints.Builder()
                .SetRequiredNetworkType(requiredNetworkType)
                .SetRequiresBatteryNotLow(false) // Set false only for testing.
                .Build();

            // Time to wait before retrying if the task fails.
            var backOffDelay = new TimeSpan(0, 5, 0);

            // Start the first repeat interval iteration to 10:00 UTC (5:00 AM EST/6:00 AM EDT).

            // var intervalStart = DateTime.UtcNow.Date.AddHours(10);
            // if (intervalStart < DateTime.UtcNow) intervalStart = intervalStart.AddDays(1);

            // Configure the repeat interval for 24 hours.  Set the flex interval to the last 12 hours.
            // This will ensure that the worker has time to run during the evening, after the stock
            // markets have closed.
            // var repeatInterval = TimeSpan.FromHours(24);  // Cycle interval.
            // var flexInterval = TimeSpan.FromHours(12);    // Time within the cycle to run (last 12 hours).

            // Time delay before starting the first iteration.
            // var initialDelay = TimeSpan.Zero;
            // var minutesTillMidnight = (24 - DateTime.UtcNow.Hour) * 60;

            // Create the work request to periodically run the worker that updates all stock quotes
            PeriodicWorkRequest quotationSchedule = (PeriodicWorkRequest)PeriodicWorkRequest.Builder
                .From<QuoteWorker>(TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(5))
                .SetBackoffCriteria(BackoffPolicy.Exponential!, backOffDelay.Minutes, TimeUnit.Minutes!)
                .SetInitialDelay(5, TimeUnit.Minutes!)!
                .SetConstraints(constraints)
                .AddTag(PlatformTag)
                .Build();

            // var op = WorkManager.GetInstance(App.Context).Enqueue(sigalsQuotationScheduler);
            var mgr = WorkManager.GetInstance(Xamarin.Essentials.Platform.AppContext);
            var op = mgr.EnqueueUniquePeriodicWork(PlatformTag, ExistingPeriodicWorkPolicy.Keep!,
                quotationSchedule);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
//#if RELEASE
// Create the work request to periodically run the worker that updates all stock quotes
/*
PeriodicWorkRequest quotationSchedule = (PeriodicWorkRequest)PeriodicWorkRequest.Builder
    .From<QuoteWorker>(TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(5))
    .SetBackoffCriteria(BackoffPolicy.Exponential!, backOffDelay.Minutes, TimeUnit.Minutes!)
    .SetInitialDelay(5, TimeUnit.Minutes!)!
    .SetConstraints(constraints)
    .AddTag(PlatformTag)
    .Build();

// var op = WorkManager.GetInstance(App.Context).Enqueue(sigalsQuotationScheduler);
var mgr = WorkManager.GetInstance(Xamarin.Essentials.Platform.AppContext);
var op = mgr.EnqueueUniquePeriodicWork(PlatformTag, ExistingPeriodicWorkPolicy.Keep!,
    quotationSchedule);
*/
//        mgr.CancelAllWork();
//#else
// Create the work request to periodically run the worker that updates all stock quotes
/*
PeriodicWorkRequest quotationSchedule = (PeriodicWorkRequest)PeriodicWorkRequest.Builder
    .From<QuoteWorker>(repeatInterval, flexInterval)
    .SetBackoffCriteria(BackoffPolicy.Exponential!, backOffDelay.Minutes, TimeUnit.Minutes!)
    .SetInitialDelay(minutesTillMidnight, TimeUnit.Minutes!)!
    .SetConstraints(constraints)
    .AddTag(PlatformTag)
    .Build();

// var op = WorkManager.GetInstance(App.Context).Enqueue(sigalsQuotationScheduler);
var mgr = WorkManager.GetInstance(Xamarin.Essentials.Platform.AppContext);
var op = mgr.EnqueueUniquePeriodicWork(PlatformTag, ExistingPeriodicWorkPolicy.Keep!,
    quotationSchedule);
*/
// mgr.CancelAllWork();
//#endif