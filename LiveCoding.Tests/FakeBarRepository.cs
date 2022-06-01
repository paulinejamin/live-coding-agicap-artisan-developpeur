using System.Collections.Generic;
using System.Linq;
using LiveCoding.Persistence;

namespace LiveCoding.Tests;

public class FakeBarRepository : IBarRepository
{
    private readonly IEnumerable<BarData> _bars;

    public FakeBarRepository(BarData[] barDatas)
    {
        _bars = barDatas;
    }

    public IEnumerable<BarData> Get()
    {
        return _bars;
    }
}