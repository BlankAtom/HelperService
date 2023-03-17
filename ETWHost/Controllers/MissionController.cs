using System.Reflection;
using ETWHost.Core.Helper;
using ETWHost.Core.Mission;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ETWHost.Controllers;

[ApiController]
[Route("mission")]
public class MissionController : ControllerBase
{
    private readonly IMissionManager _manager;

    [HttpPost("add")]
    public IActionResult AddMission(dynamic args)
    {
        var assemblies = new List<Assembly>();
        assemblies.Add(typeof(IMission).Assembly);

        var list = assemblies.GetControllerAssembly().ToList();
        return Ok("");
    }
}