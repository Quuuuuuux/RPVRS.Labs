using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RPVRS.Labs.CacheDb;
using RPVRS.Labs.Lab2.Inputs;
using RPVRS.Labs.Lab2.ResponseModels;
using RPVRS.Labs.Models;

namespace RPVRS.Labs.Lab2.Controllers;

[ApiController]
[Route("api/robot/")]
public class RobotController: Controller
{
    private readonly ICacheDb _cache;
    private readonly IStringLocalizer<RobotController> _localizer;
    private readonly LinkGenerator _linkGenerator;

    public RobotController(ICacheDb cache, IStringLocalizer<RobotController> localizer, LinkGenerator linkGenerator)
    {
        _cache = cache;
        _localizer = localizer;
        _linkGenerator = linkGenerator;
    }
    
    [HttpPost("")]
    public IActionResult CreateRobot([FromBody] RobotInput input)
    {
        var robot = new Robot(input.SerialNumber, input.Name, input.State);
        _cache.Add(robot.Id, robot);
        return Ok(new ResultObject<Robot>(_localizer["SuccessCreate"].Value, robot));
    }
    
    [HttpPut("{id:long}", Name = "UpdateUser")]
    public IActionResult UpdateRobot([FromRoute]long id, [FromBody] RobotInput input)
    {
        if (! _cache.TryGet(id, out _)) return BadRequest(_localizer["NoSuchRobot"].Value);
        
        var updatedRobot = new Robot(id, input.SerialNumber, input.Name, input.State)
        {
            UpdatedAt = DateTimeOffset.UtcNow
        };

        _cache.Update(id, updatedRobot);
        
        return Ok(new ResultObject<Robot>(_localizer["SuccessUpdate"].Value, updatedRobot));
    }
    
    [HttpDelete("{id:long}", Name = "DeleteUser")]
    public IActionResult DeleteRobot(long id)
    {
        if (!_cache.TryGet(id, out _)) return BadRequest(_localizer["NoSuchRobot"].Value);
        _cache.TryDelete(id, out var robot);
        return Ok(new ResultObject<Robot>(_localizer["SuccessDelete"], (Robot) robot!));
    }
    
    [HttpGet("{id:long}", Name = "GetUser")]
    public IActionResult GetRobot(long id)
    {
        var links = new[]
        {
            new LinkDto(_linkGenerator.GetUriByName(HttpContext, "GetUser", new { id }), _localizer["SelfRoute"], "GET"),
            new LinkDto(_linkGenerator.GetUriByName(HttpContext, "UpdateUser", new { id }), _localizer["UpdateRoute"], "PUT"),
            new LinkDto(_linkGenerator.GetUriByName(HttpContext, "DeleteUser", new { id }), _localizer["DeleteRoute"], "DELETE")
        };
        if (!_cache.TryGet(id, out var robot)) return BadRequest(_localizer["NoSuchRobot"].Value);
        return Ok(new ResultObject<Robot>(_localizer["SuccesGet"], (Robot) robot!, links));
    }
}