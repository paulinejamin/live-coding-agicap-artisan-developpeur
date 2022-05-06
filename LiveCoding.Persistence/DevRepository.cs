using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LiveCoding.Persistence;

public class DevRepository : IDevRepository
{
    public IEnumerable<DevData> Get()
    {
        var json = File.ReadAllText("../LiveCoding.Persistence/devs.json");
        var devs = JsonConvert.DeserializeObject<IEnumerable<DevData>>(json);

        return devs;
    }
}