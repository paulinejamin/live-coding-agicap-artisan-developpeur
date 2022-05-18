using LiveCoding.Persistence;

namespace LiveCoding.Services;

public class FakeBoatRepository
{
    private readonly IEnumerable<BoatData> boats ;

    public FakeBoatRepository(BoatData[]? boatDatas = null)
    {
        boats = boatDatas ?? Array.Empty<BoatData>();
    }

    public IEnumerable<BoatData> Get()
    {
        return boats;
    }
}