using System.Text;
using System.Text.Json;
using ETWHost.Core.CommandMission;

namespace ETWHost.Core.Helper;

public class TempFileLoader
{
    public void Read(string file)
    {
        var t = File.ReadAllText(file, Encoding.UTF8);
        var c = JsonSerializer.Deserialize(t, typeof(Command));
    }
}