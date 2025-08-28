using System;
using Android.Content;
using Android.Runtime;
using AndroidX.Work;

namespace Signals.Android.Scheduling;

public class QuoteWorker : Worker
{
    public QuoteWorker(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
    {
    }

    public QuoteWorker(Context context, WorkerParameters workerParams) : base(context, workerParams)
    {
    }

    public override Result DoWork()
    {
        throw new NotImplementedException();
    }
}