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

public class BoatRepository : IBoatRepository
{

}

public interface IBoatRepository
{
}

public record BoatData
{
    public int MaxPeople { get; set; }
    public string Name { get; set; }
    public DateTime OpenFrom { get; set; }
    public DateTime OpenUntil { get; set; }
}