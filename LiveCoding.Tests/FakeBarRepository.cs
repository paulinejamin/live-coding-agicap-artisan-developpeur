using System.Collections.Generic;
using LiveCoding.Infra;

namespace LiveCoding.Tests;

public class FakeBarRepository : IBarRepository
{
    private readonly IEnumerable<BarData> _bars;

    public FakeBarRepository(IEnumerable<BarData> barData)
    {
        _bars = barDatas;
    }

    public IEnumerable<BarData> Get()
    {
        return _bars;
    }
}