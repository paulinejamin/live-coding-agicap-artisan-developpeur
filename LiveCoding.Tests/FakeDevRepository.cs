using System.Collections.Generic;
using System.Linq;
using LiveCoding.Persistence;

namespace LiveCoding.Tests;

public class FakeDevRepository : IDevRepository
{
    private readonly IEnumerable<DevData> devs;

    public FakeDevRepository(DevData[] devDatas)
    {
        devs = devDatas;
    }

    public IEnumerable<DevData> Get()
    {
        return devs;
    }
}