using ETWHost.Core.Mission;

namespace ETWHost.Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var m = new MissionManager();
        Console.WriteLine(m.GetTime());
        Thread.Sleep(10000);
        Console.WriteLine(m.GetTime());
    }
}