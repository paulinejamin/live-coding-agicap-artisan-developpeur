namespace LiveCoding.Infra;

public interface IDevRepository
{
    IEnumerable<DevData> Get();
}