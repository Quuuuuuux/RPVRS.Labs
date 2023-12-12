using System.Runtime.Caching;
using Microsoft.Extensions.Options;
using RPVRS.Labs.CacheDb.Common;

namespace RPVRS.Labs.CacheDb;

internal class CacheDb: ICacheDb
{
    private readonly CacheDbOptions _options;
    private readonly MemoryCache _memoryCache;
    
    public CacheDb(IOptions<CacheDbOptions> options)
    {
        _options = options.Value;
        _memoryCache = new MemoryCache(_options.StorageName);
    }

    public bool TryGet(long id, out object? value)
    {
        var idStr = id.ToString();
        if (!_memoryCache.Contains(idStr))
        {
            value = null;
            return false;
        }
        value = _memoryCache.Get(idStr)!;
        return true;
    }

    public bool Add(long id, object value)
    {
        return _memoryCache.Add(id.ToString(), value, DateTimeOffset.Now.AddHours(1));
    }

    public void Update(long id, object value)
    {
        _memoryCache.Set(id.ToString(), value, DateTimeOffset.Now.AddHours(1));
    }

    public bool TryDelete(long id, out object? value)
    {
        var idStr = id.ToString();
        if (!_memoryCache.Contains(idStr))
        {
            value = null;
            return false;
        }
        value =  _memoryCache.Remove(idStr);
        return true;
    }
}