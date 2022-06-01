namespace LiveCoding.Services;

public record BestDate(DateTime Date, int NumberOfDevsAvailable)
{
    private const double MinimumPercentageOfDevs = 0.6;

    public static BestDate Get(List<DevAvailability> devAvailabilities, int totalNumberOfDevelopers)
    {
        var maximumOfDevsOnSite = devAvailabilities.Select(devAvailability => devAvailability.NumberOfPeople.Value)
            .Max();

        if (AMinimumOfDevIsNotAvailable(totalNumberOfDevelopers, maximumOfDevsOnSite))
        {
            return new DateNotFound();
        }

        var favoriteDate = devAvailabilities
            .First(devAvailability => devAvailability.NumberOfPeople == maximumOfDevsOnSite)
            .Date;

        return new BestDate(favoriteDate, maximumOfDevsOnSite);
    }

    private static bool AMinimumOfDevIsNotAvailable(int totalNumberOfDevelopers, int maxNumberOfDevsAvailable)
    {
        return maxNumberOfDevsAvailable <= totalNumberOfDevelopers * MinimumPercentageOfDevs;
    }
}