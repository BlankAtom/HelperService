using System.Diagnostics;

namespace ETWHost.Core.Helper;

public class CommandHelper
{
    public static void RunCommand(string execute, string others)
    {
        var process = new Process();
        var info = new ProcessStartInfo()
        {
            FileName = execute,
            Arguments = others,
            UseShellExecute = false,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };
        process.StartInfo = info;

        process.Start();
        var output = process.StandardOutput.ReadToEnd();
        Console.WriteLine(output);
        process.WaitForExit();
        process.Close();
    }
}