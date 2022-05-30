namespace LiveCoding.Persistence;

public interface IBoatRepository
{
    IEnumerable<BoatData> Get();
}