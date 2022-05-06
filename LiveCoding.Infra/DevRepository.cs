using Newtonsoft.Json;

namespace LiveCoding.Infra;

public class DevRepository : IDevRepository
{
    public IEnumerable<DevData> Get()
    {
        var json = File.ReadAllText("../LiveCoding.Persistence/Data/devs.json");
        var devs = JsonConvert.DeserializeObject<IEnumerable<DevData>>(json);

        return devs;
    }
}