namespace LiveCoding.Infra;

public interface IBoatRepository
{
    IEnumerable<BoatData> Get();
}