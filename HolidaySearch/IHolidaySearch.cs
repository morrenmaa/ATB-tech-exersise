namespace HolidaySearchLib
{
    public interface IHolidaySearch
    {
        HolidaySearchResponse Search(HolidaySearchRequest request);
    }
}