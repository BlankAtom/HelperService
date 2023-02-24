namespace ETWHost.Core.Mission;

public class MissionCollect : IMissionCollect
{
    /// Mission list
    public List<IMission> Missions{ get; set; }

    private int _index = 0;

    public DateTime NextTime{ get; set; }
    
    public long Interval { get; set; }

    public MissionCollect(IMission mission, long interval)
    {
        Missions = new List<IMission>();
        NextTime = DateTime.Now.AddMilliseconds(interval);
        Interval = interval;
        

        Missions.Add(mission);
    }

    public IMission? Next()
    {
        if (_index > Missions.Count)
            return null;
        return Missions[_index];
    }

    public async Task RunAsync()
    {
        foreach (var mission in Missions)
        {
            await mission.RunAsync();
        }

        await Task.CompletedTask;
    }

    public long GetInterval() => Interval;
    public void SetInterval(long ms)
    {
        Interval = ms;
    }

    public DateTime GetTime() => NextTime;
    public void SetNextTime()
    {
        NextTime = DateTime.Now.AddMilliseconds(Interval);
    }

}