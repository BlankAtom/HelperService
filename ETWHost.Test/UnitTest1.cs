using System.Diagnostics;
using System.Reflection;
using ETWHost.Core.Helper;
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
        // Thread.Sleep(10000);
        // Console.WriteLine(m.GetTime());

        var ms = new MissionManager();
        ms.CreateManagerCollection(new MissionEntity(), 1000);

        int cnt = 0;
        while (cnt < 20)
        {
            Thread.Sleep(500);
        }
    }

    [Test]
    public void Test2()
    {
        var process = new Process();
        var info = new ProcessStartInfo()
        {
            FileName = "git-tfs",
            Arguments = "quick-clone http://172.21.0.11/DefaultCollection $/FUNDAMENTAL_UTILITY ./repo",
            UseShellExecute = false,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };
        process.StartInfo = info;

        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        Console.WriteLine(output);
        process.WaitForExit();
        process.Close();
    }

    [Test]
    public void test3()
    {
        
        var assemblies = new List<Assembly>();
        assemblies.Add(typeof(IMission).Assembly);

        foreach (var assembly in assemblies.GetControllerAssembly())
        {
            Console.WriteLine(assembly);
        }
    }
}