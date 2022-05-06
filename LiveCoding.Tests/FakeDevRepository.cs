using System.Collections.Generic;
using LiveCoding.Infra;

namespace LiveCoding.Tests;

public class FakeDevRepository : IDevRepository
{
    private readonly IEnumerable<DevData> _devs;

    public FakeDevRepository(IEnumerable<DevData> devData)
    {
        _devs = devDatas;
    }

    public IEnumerable<DevData> Get()
    {
        return _devs;
    }
}