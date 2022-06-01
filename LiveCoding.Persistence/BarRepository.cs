using Newtonsoft.Json;

namespace LiveCoding.Persistence;

public class BarRepository : IBarRepository
{
    public IEnumerable<BarData> Get()
    {
        var json = File.ReadAllText("../LiveCoding.Persistence/bars.json");
        var bars = JsonConvert.DeserializeObject<BarsData>(json);

        return bars.Bars;
    }
}