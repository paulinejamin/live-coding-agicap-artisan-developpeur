namespace LiveCoding.Infra;

public interface IBarRepository
{
    IEnumerable<BarData> Get();
}