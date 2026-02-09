namespace TasksSandbox.Types;

internal class ClassAsyncTask
{
    public async Task DoLogicAsync()
    {
        Console.WriteLine("ClassAsyncTask logic start with Async Task");

        var hostsContent = File.ReadAllTextAsync(@"C:\Windows\System32\drivers\etc\hosts");

        Console.WriteLine("ClassAsyncTask logic start with Async Task done");
    }
}
