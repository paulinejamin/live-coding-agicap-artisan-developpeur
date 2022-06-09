namespace LiveCoding.Services;

public interface IProvideBars
{
    IEnumerable<Bar> GetAllBars();
}