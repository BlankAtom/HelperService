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
        return Ok("");
    }
}