using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RPVRS.Labs.CacheDb;
using RPVRS.Labs.Lab2.ResponseModels;
using RPVRS.Labs.Models;

namespace RPVRS.Labs.Lab2.Controllers;

[Route("api/robot")]
public class RobotController: Controller
{
    private readonly ICacheDb _cache;
    private readonly IStringLocalizer<RobotController> _localizer;

    public RobotController(ICacheDb cache, IStringLocalizer<RobotController> localizer)
    {
        _cache = cache;
        _localizer = localizer;
    }
    
    [HttpPost]
    public IActionResult CreateRobot(long id, string serialNumber, string name, State state)
    {
        var robot = new Robot(id, serialNumber, name, state);
        _cache.Add(id, robot);
        return Ok(new ResultObject<Robot>(_localizer["SuccessCreate"].Value, robot));
    }
    
    [HttpPut]
    public IActionResult UpdateRobot(long id, string serialNumber, string name, State state)
    {
        if (! _cache.TryGet(id, out _)) return BadRequest(_localizer["NoSuchRobot"].Value);
        
        var updatedRobot = new Robot(id, serialNumber, name, state)
        {
            UpdatedAt = DateTimeOffset.UtcNow
        };

        _cache.Update(id, updatedRobot);
        
        return Ok(new ResultObject<Robot>(_localizer["SuccessUpdate"].Value, updatedRobot));
    }
    
    [HttpDelete]
    public IActionResult DeleteRobot(long id)
    {
        if (!_cache.TryGet(id, out _)) return BadRequest(_localizer["NoSuchRobot"].Value);
        _cache.TryDelete(id, out _);
        return Ok(_localizer["SuccessDelete"].Value);
    }
    
    [HttpGet]
    public IActionResult GetRobot(long id)
    {
        if (!_cache.TryGet(id, out var robot)) return BadRequest(_localizer["NoSuchRobot"].Value);
        return Ok((Robot) robot!);
    }
}