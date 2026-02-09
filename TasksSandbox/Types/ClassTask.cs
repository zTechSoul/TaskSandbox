namespace TasksSandbox.Types;

internal class ClassTask
{
    public Task DoLogicTask()
    {
        Console.WriteLine("ClassTask logic start with Async Task");

        var hostsContent = File.ReadAllTextAsync(@"C:\Windows\System32\drivers\etc\hosts");

        Console.WriteLine("ClassTask logic start with Async Task done");

        return hostsContent;
    }
}
