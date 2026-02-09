namespace TasksSandbox.Bench
{
    static class Settings
    {
        public const int RunLimit = 1000;
    }

    internal class WorkerSync
    {
        public long DoWorkNoTask()
        {
            long result = 0;
            for (int i = 0; i < Settings.RunLimit; i++)
                result += 1;

            return result;
        }
    }

    internal class WorkerSyncTasked
    {
        public ValueTask<long> DoWorkByTask()
        {
            long result = 0;
            for (int i = 0; i < Settings.RunLimit; i++)
                result += 1;

            return ValueTask.FromResult(result);
        }
    }

    internal class WorkerAsync
    {
        public async Task<long> DoWorkNoAwait()
        {
            long result = 0;
            for (int i = 0; i < Settings.RunLimit; i++)
                result += 1;

            return await Task.FromResult(result);
        }
        public async Task<long> DoWorkNoAwait2()
        {
            long result = 0;
            for (int i = 0; i < Settings.RunLimit; i++)
                result += 1;

            return await Task.FromResult(result);
        }
    }

    internal class WorkerAsyncAwait
    {
        public async Task<long> DoWorWithAwait() =>
            await Task.Run<long>(() => {
                long result = 0;
                for (int i = 0; i < Settings.RunLimit; i++)
                    result += 1;

                return result;
            });
    }
}
