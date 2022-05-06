using LiveCoding.Domain;
using LiveCoding.Domain.Ports;

namespace LiveCoding.Infra.Adapters;

public class DevelopersAvailabilitiesAdapter : IProvideDevelopersAvailabilities
{
    private readonly IDevRepository devRepository;

    public DevelopersAvailabilitiesAdapter(IDevRepository devRepository)
    {
        this.devRepository = devRepository;
    }

    public DevAvailabilities Get()
    {
        var devs = devRepository.Get();
        var availabilities = new List<DevAvailability>();
        foreach (var date in devs.SelectMany(dev => dev.OnSite))
        {
            var devAvailability = availabilities.FirstOrDefault(availability => availability.Date == date);
            if (devAvailability == null)
            {
                availabilities.Add(new DevAvailability(date, 1));
            }
            else
            {
                var numberOfPeople = devAvailability.NumberOfPeople + 1;
                availabilities.Remove(devAvailability);
                availabilities.Add(new DevAvailability(date, numberOfPeople));
            }
        }

        return new DevAvailabilities(availabilities, devs.Count());
    }
}