using Microsoft.AspNetCore.Http.HttpResults;

namespace RPVRS.Labs.Lab1.API.Models;

public enum State
{
    TurnOn,
    Pause,
    TurnOff
}

public class Robot
{
    public long Id { get; set; }
    public string SerialNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public State State { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public Robot(long id, string serialNumber, string name, State state)
    {
        Id = id;
        SerialNumber = serialNumber;
        Name = name;
        State = state;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = null;
    }
}