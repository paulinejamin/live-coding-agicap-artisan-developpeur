namespace LiveCoding.Persistence;

public interface IBarRepository
{
    IEnumerable<BarData> Get();
}