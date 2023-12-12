namespace RPVRS.Labs.CacheDb;

public interface ICacheDb
{
    public bool TryGet(long id, out object? value);
    public bool Add(long id, object value);
    public void Update(long id, object value);
    public bool TryDelete(long id, out object? value);
}