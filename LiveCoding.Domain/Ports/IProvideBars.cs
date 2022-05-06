namespace LiveCoding.Domain.Ports;

public interface IProvideBars
{
    public IEnumerable<Bar> GetAll();
}
