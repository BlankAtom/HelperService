using System.Diagnostics;
using ETWHost.Core.Mission;

namespace ETWHost.Core.CommandMission;

public class Command : IMission
{
    private string execute;
    public string Execute
    {
        get => execute;
        set => execute = value;
    }

    private string parameters;

    public string Parameters
    {
        get => parameters;
        set => parameters = value;
    }

    public Command()
    {
        execute = "";
        parameters = "";
    }

    public Command(string e, string p)
    {
        execute = e;
        parameters = p;
    }
    
    public Task RunAsync()
    {
        Run();
        return Task.CompletedTask;
    }

    public void Run()
    {
        var process = new Process();
        var info = new ProcessStartInfo()
        {
            FileName = execute,
            Arguments = execute + " " + parameters,
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
}