namespace HolidaySearchLib;

public abstract class Pipeline<T>
{
    protected readonly List<IFilter<T>> filters = new();

    public Pipeline<T> Register (IFilter<T> filter)
    {
        filters.Add(filter);
        return this;
    }

    public abstract T Process(T input);
}