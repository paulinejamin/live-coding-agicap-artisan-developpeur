using System;
using System.Collections.Generic;
using System.Linq;
using LiveCoding.Persistence;

namespace LiveCoding.Tests;

public class FakeBarRepository : IBarRepository
{
    private readonly IEnumerable<BarData> bars;

    public FakeBarRepository(BarData[] barDatas)
    {
        bars = barDatas.Select(a => new BarData());
    }

    public IEnumerable<BarData> Get()
    {
        return bars;
    }
}