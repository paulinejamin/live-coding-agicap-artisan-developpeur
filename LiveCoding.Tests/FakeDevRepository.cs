using System.Collections.Generic;
using System.Linq;
using LiveCoding.Persistence;

namespace LiveCoding.Tests;

public class FakeDevRepository : IDevRepository
{
    private readonly IEnumerable<DevData> _devs;

    public FakeDevRepository(DevData[] devDatas)
    {
        _devs = devDatas;
    }

    public IEnumerable<DevData> Get()
    {
        return _devs;
    }
}