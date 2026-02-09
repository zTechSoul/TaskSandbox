using System.Diagnostics;
using System.Runtime;
using TasksSandbox.Bench;

namespace TasksSandbox;

internal class Program
{
    static async Task Main(string[] args)
    {
        var noGCRegionStarted = GC.TryStartNoGCRegion(100_000_000, true);
        if (!noGCRegionStarted && GCSettings.LatencyMode != GCLatencyMode.LowLatency)
            throw new Exception("Can not set NO GC region");

        // warmup
        var workerSync = new WorkerSync();
        var workerSyncTasked = new WorkerSyncTasked();
        var workerAsync = new WorkerAsyncAwait();
        workerSync.DoWorkNoTask();
        await workerSyncTasked.DoWorkByTask();
        await workerAsync.DoWorWithAwait();

        for (var i = 0; i < 6; i++)
            await RunWorkersTest(workerSync, workerSyncTasked, workerAsync);

        if (GCSettings.LatencyMode == GCLatencyMode.LowLatency)
            GC.EndNoGCRegion();

        Console.WriteLine("Hello, World!");
    }

    private static async Task RunWorkersTest(WorkerSync workerSync, WorkerSyncTasked workerSyncTasked, WorkerAsyncAwait workerAsync)
    {
        // do stats
        long statsSyncNoTask = 0l;
        long statsSyncWithTask = 0l;
        long statsAsync = 0l;

        // sync
        Stopwatch sw = Stopwatch.StartNew();
        sw.Restart();
        while (sw.ElapsedMilliseconds < 100)
            statsSyncNoTask += workerSync.DoWorkNoTask();

        sw.Restart();
        while (sw.ElapsedMilliseconds < 100)
            statsSyncWithTask += await workerSyncTasked.DoWorkByTask();

        sw.Restart();
        while (sw.ElapsedMilliseconds < 100)
            statsAsync += await workerAsync.DoWorWithAwait();

        sw.Stop();

        Console.WriteLine($" Sync   no task: {statsSyncNoTask}");
        Console.WriteLine($" Sync with task: {statsSyncWithTask}");
        Console.WriteLine($"Async          : {statsAsync}");

        var minStat = Math.Min(Math.Min(statsAsync, statsSyncWithTask), statsSyncNoTask);
        var maxStat = Math.Max(Math.Max(statsAsync, statsSyncWithTask), statsSyncNoTask);
        var delta = (maxStat - minStat) * 100 / minStat;
        Console.WriteLine($"Delta: {delta}%\n");
    }
}
