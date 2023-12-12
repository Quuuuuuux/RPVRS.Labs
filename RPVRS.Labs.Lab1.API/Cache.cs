using System.Runtime.Caching;

namespace RPVRS.Labs.Lab1.API;

public class Cache
{
    public MemoryCache MemoryCache = new("lab1");
}