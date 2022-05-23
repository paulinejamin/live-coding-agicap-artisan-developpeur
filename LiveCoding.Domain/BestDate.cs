namespace LiveCoding.Domain;

public record BestDate
{
    public static BestDate NotFound = new(DateTime.MinValue, 0);
    private const double MinimumPercentageOfDevs = 0.6;
    public DateTime Date { get; }
    public int NumberOfDevsAvailable { get; }

    private BestDate(DateTime date, int numberOfDevsAvailable)
    {
        Date = date;
        NumberOfDevsAvailable = numberOfDevsAvailable;
    }

    public static BestDate GetBestDate(int numberOfDevs, IEnumerable<DateTime> allDates)
    {
        var availabilities = new Dictionary<DateTime, int>();
        foreach (var date in allDates)
        {
            if (availabilities.ContainsKey(date))
            {
                availabilities[date]++;
            }
            else
            {
                availabilities.Add(date, 1);
            }
        }

        var maxNumberOfDevsAvailable = availabilities.Values.Max();

        if (maxNumberOfDevsAvailable <= numberOfDevs * MinimumPercentageOfDevs)
        {
            return BestDate.NotFound;
        }

        var dateTime = availabilities.First(kv => kv.Value == maxNumberOfDevsAvailable).Key;
        return new BestDate(dateTime, maxNumberOfDevsAvailable);
    }
}