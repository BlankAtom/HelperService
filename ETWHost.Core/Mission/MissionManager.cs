using System.Timers;
using Timer = System.Timers.Timer;

namespace ETWHost.Core.Mission;

public class MissionManager : IMissionManager
{
    private Timer _centerTimer;

    private DateTime _nowTime;

    public MissionManager()
    {
        initTimer();
    }

    private void initTimer()
    {
        _centerTimer = new Timer(5000);
        _centerTimer.Elapsed += (_, _) => _nowTime = DateTime.Now;
        _centerTimer.Start();
    }
}

