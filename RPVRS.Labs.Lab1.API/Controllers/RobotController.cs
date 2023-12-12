using Microsoft.AspNetCore.Mvc;
using RPVRS.Labs.Lab1.API.Models;

namespace RPVRS.Labs.Lab1.API.Controllers;
[Route("api/robot")]
public class RobotController
{
    private readonly Cache _cache;

    public RobotController(Cache cache)
    {
        _cache = cache;
    }
    
    [HttpPost]
    public Robot CreateRobot(long id, string serialNumber, string name, State state)
    {
        var robot = new Robot(id, serialNumber, name, state);
        _cache.MemoryCache.Add(id.ToString(), robot, DateTimeOffset.Now.AddHours(1));
        return robot;
    }
    
    [HttpPut]
    public Robot UpdateRobot(long id, string serialNumber, string name, State state)
    {
        var idStr = id.ToString();

        if (! _cache.MemoryCache.Contains(idStr)) throw new Exception("no such robot");
        
        var robot = (Robot)( _cache.MemoryCache.Get(idStr));
        var updatedRobot = new Robot(id, serialNumber, name, state)
        {
            UpdatedAt = DateTimeOffset.UtcNow
        };

        _cache.MemoryCache.Set(idStr, updatedRobot, DateTimeOffset.Now.AddHours(1));
        
        return updatedRobot;
    }
    
    [HttpDelete]
    public string DeleteRobot(long id)
    {
        var idStr = id.ToString();
        if (! _cache.MemoryCache.Contains(idStr)) throw new Exception("no such robot");
        var robot = (Robot) _cache.MemoryCache.Remove(idStr);
        return $"robot with id = {robot.Id} was deleted";
    }
    
    [HttpGet]
    public Robot CreateRobot(long id)
    {
        var idStr = id.ToString();
        if (! _cache.MemoryCache.Contains(idStr)) throw new Exception("no such robot");
        return (Robot) _cache.MemoryCache.Get(idStr);
    }
}