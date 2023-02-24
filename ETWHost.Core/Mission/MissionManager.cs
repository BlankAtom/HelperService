using System.Timers;
using Timer = System.Timers.Timer;

namespace ETWHost.Core.Mission;

public class MissionManager : IMissionManager
{
    private Timer _centerTimer;

    private DateTime _nowTime;

    private List<IMissionCollect> _collects;

    public MissionManager()
    {
        _centerTimer = new Timer(5000);
        _nowTime = DateTime.Now;
        initTimer();
    }

    private void initTimer()
    {
        _centerTimer.Elapsed += (_, _) => _nowTime = DateTime.Now;
        _centerTimer.Elapsed += (_, _) => Update();
        _centerTimer.Start();
    }

    public string GetTime() => _nowTime.ToLongTimeString();

    private void Update()
    {
        foreach (var collect in _collects)
        {
            if (collect.GetTime() < DateTime.Now)
            {
                if (DateTime.Now.Subtract(collect.GetTime()).Microseconds < 5 * collect.GetInterval())
                {
                    collect.RunAsync();
                }
                
                collect.SetNextTime();
            }
        }
    }

    public void CreateManagerCollection(IMission mission, long ms)
    {
        
    }
}

