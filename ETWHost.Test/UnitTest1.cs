using System.Diagnostics;
using System.Reflection;
using ETWHost.Core.Helper;
using ETWHost.Core.Mission;
using ETWHost.Test.NewDirectory2;

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
        var j = new List<string>() { "a", "b" };
        var t = new Data<A>();
        var a = new A();
        t.Value = a;
        t.AddProperty("name");
        Console.WriteLine(t.GetPropertyValue("name"));


    }

    class A
    {
        public string name { get; } = "123";
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