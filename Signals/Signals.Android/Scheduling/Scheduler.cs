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
        // We're probably going to need an exablement screen in order to launch the periodic work request, so
        // that we can configure the network type and other settings.  If we want to change the network type, later
        // on, we'll need to cancel the work request and re-create it.'
        
        try
        {
            // var fileService = new FileService();
            // var appConfigService = new SignalsConfigurationService(fileService);
            // var appConfig = appConfigService.GetConfig();
            // var requiredNetworkType = appConfig.UsePhoneData ? NetworkType.Connected! : NetworkType.Unmetered!;

            // May need to ask for user operation to use cellular data.  For testing, assume Wifi is available.
            var requiredNetworkType = NetworkType.Unmetered!;

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
            var repeatInterval = TimeSpan.FromHours(24);  // Cycle interval.
            var flexInterval = TimeSpan.FromMinutes(15);  // Time within the cycle to run (last 12 hours).

             // Time delay before starting the first iteration.
            // var initialDelay = TimeSpan.FromMinutes(5);
            // var minutesTillMidnight = (24 - DateTime.UtcNow.Hour) * 60;

            // Get Eastern Time Zone (handles EST/EDT)
            var easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById(
                OperatingSystem.IsWindows() ? "Eastern Standard Time" : "America/New_York");

            // Calculate initial delay to next 4:30 PM EST/EDT
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, easternTimeZone);
            var targetTime = new DateTime(now.Year, now.Month, now.Day, 16, 30, 0); // 4:30 PM
            if (now >= targetTime)
            {
                targetTime = targetTime.AddDays(1); // Next day
            }
            var initialDelay = (targetTime - now).Minutes;
            
            // Create the work request to periodically run the worker that updates all stock quotes
            // PeriodicWorkRequest quotationSchedule = (PeriodicWorkRequest)PeriodicWorkRequest.Builder
            //     .From<QuoteWorker>(repeatInterval, flexInterval)
            //     .SetBackoffCriteria(BackoffPolicy.Exponential!, backOffDelay.Minutes, TimeUnit.Minutes!)
            //     .SetInitialDelay(initialDelay, TimeUnit.Minutes!)!
            //     .SetConstraints(constraints)
            //     .AddTag(PlatformTag)
            //     .Build();
            PeriodicWorkRequest quotationSchedule = (PeriodicWorkRequest)PeriodicWorkRequest.Builder
                .From<QuoteWorker>(TimeSpan.FromMinutes(60), TimeSpan.FromMinutes(15))
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
            THE FINAL VERSION OF THE WORKER:
            // PeriodicWorkRequest quotationSchedule = (PeriodicWorkRequest)PeriodicWorkRequest.Builder
            //     .From<QuoteWorker>(repeatInterval, flexInterval)
            //     .SetBackoffCriteria(BackoffPolicy.Exponential!, backOffDelay.Minutes, TimeUnit.Minutes!)
            //     .SetInitialDelay(initialDelay, TimeUnit.Minutes!)!
            //     .SetConstraints(constraints)
            //     .AddTag(PlatformTag)
            //     .Build();
            
            TEST VERSION 1:
                PeriodicWorkRequest quotationSchedule = (PeriodicWorkRequest)PeriodicWorkRequest.Builder
                .From<QuoteWorker>(TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(5))
                .SetBackoffCriteria(BackoffPolicy.Exponential!, backOffDelay.Minutes, TimeUnit.Minutes!)
                .SetInitialDelay(5, TimeUnit.Minutes!)!
                .SetConstraints(constraints)
                .AddTag(PlatformTag)
                .Build();

            TEST VERSION 2:
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