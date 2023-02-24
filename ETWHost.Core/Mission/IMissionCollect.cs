namespace ETWHost.Core.Mission;

public interface IMissionCollect
{
    DateTime GetTime();

    void SetNextTime();

    long GetInterval();
    
    void SetInterval(long ms);
    
    Task RunAsync();
}