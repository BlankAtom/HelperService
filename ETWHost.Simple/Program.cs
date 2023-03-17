using ETWHost.Core.Helper;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

var timer = new System.Timers.Timer(1 * /*60 * */1000);
// timer.Elapsed += (sender, eventArgs) =>
// {
//     CommandHelper.RunCommand("C:\\Dev\\GitTfs-0.32.0\\git-tfs.exe", $"quick-clone http://172.21.0.11/DefaultCollection/ $/FUNDAMENTAL_UTILITY {AppDomain.CurrentDomain.BaseDirectory}/repo");
//     CommandHelper.RunCommand("C:\\Dev\\GitTfs-0.32.0\\git-tfs.exe", $"quick-clone http://172.21.0.11/DefaultCollection/ $/FUNDAMENTAL_UTILITY {AppDomain.CurrentDomain.BaseDirectory}/repo");
//     timer.Stop();
// };
var ck_script = """
cd /d %~dp0
"C:\\Program Files\\Git\\bin\\git.exe" checkout -b tfs_default tfs/default
"C:\\Program Files\\Git\\bin\\git.exe" branch
""";
File.WriteAllText($"{AppDomain.CurrentDomain.BaseDirectory}/repo/ck_script.bat", ck_script);
CommandHelper.RunCommand($"{AppDomain.CurrentDomain.BaseDirectory}/repo/ck_script.bat", "");
var fetch = """
git tfs fetch
git
""";

timer.Start();

app.Run();