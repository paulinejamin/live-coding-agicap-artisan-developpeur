using LiveCoding.Persistence;

namespace LiveCoding.Services;

public class BarAdapter : IProvideBars
{
    private readonly IBarRepository _barRepo;
    private readonly IBoatRepository _boatRepo;

    public BarAdapter(IBarRepository barRepo, IBoatRepository boatRepo)
    {
        _barRepo = barRepo;
        _boatRepo = boatRepo;
    }

    public IEnumerable<Bar> GetAllBars()
    {
        var bars = _barRepo.Get();
        var boats = _boatRepo.Get();
        IEnumerable<Bar> allBars = bars.Select(b => new Bar(b.Name, b.Capacity, b.Open, false))
            .Concat(boats.Select(b => new Bar(b.Name, b.MaxPeople, Enum.GetValues<DayOfWeek>(), true)));
        return allBars;
    }
}