namespace HolidaySearchLib;

public interface IFilter<T>
{
    T Execute(T input);
}