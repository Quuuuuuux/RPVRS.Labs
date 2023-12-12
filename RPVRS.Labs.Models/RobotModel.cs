using System.Text.Json.Serialization;

namespace RPVRS.Labs.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
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

    public Robot(string serialNumber, string name, State state)
    {
        Id = new Random().Next(1,1000);
        SerialNumber = serialNumber;
        Name = name;
        State = state;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = null;
    }
    
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