using System.Collections.Generic;
using LiveCoding.Persistence;

namespace LiveCoding.Tests;

public class FakeBoatRepository : IBoatRepository
{
    private readonly IEnumerable<BoatData> boats;

    public FakeBoatRepository(IEnumerable<BoatData> boatData)
    {
        boats = boatData;
    }

    public IEnumerable<BoatData> Get()
    {
        return boats;
    }
}