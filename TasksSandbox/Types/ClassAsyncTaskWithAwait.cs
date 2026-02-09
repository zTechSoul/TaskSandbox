namespace TasksSandbox.Types;

internal class ClassAsyncTaskWithAwait
{
    public async Task DoLogicAsync()
    {
        Console.WriteLine("ClassAsyncTaskWithAwait logic start with Async Task and await");

        var hostsContent = await File.ReadAllTextAsync(@"C:\Windows\System32\drivers\etc\hosts");

        Console.WriteLine(hostsContent);
        Console.WriteLine("ClassAsyncTaskWithAwait logic start with Async Task and await done");
    }
}
