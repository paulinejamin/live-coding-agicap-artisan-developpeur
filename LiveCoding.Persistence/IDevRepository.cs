namespace LiveCoding.Persistence;

public interface IDevRepository
{
    IEnumerable<DevData> Get();
}