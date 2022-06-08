namespace LiveCoding.Infra;

public interface IRooftopRepository
{
    IEnumerable<RooftopData> Get();
}