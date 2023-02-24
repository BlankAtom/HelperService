namespace ETWHost.Core.Mission;

public class MissionEntity : IMission
{
    public Task RunAsync()
    {
#if DEBUG
        Console.WriteLine("Doit");
        return Task.CompletedTask;
#endif
    }
}