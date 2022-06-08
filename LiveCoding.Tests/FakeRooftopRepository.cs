using System.Collections.Generic;
using LiveCoding.Infra;

namespace LiveCoding.Tests;

public class FakeRooftopRepository : IRooftopRepository
{
    private readonly IEnumerable<RooftopData> _boats;

    public FakeRooftopRepository(IEnumerable<RooftopData> boatData)
    {
        _boats = boatData;
    }

    public IEnumerable<RooftopData> Get()
    {
        return _boats;
    }
}