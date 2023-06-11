namespace HolidaySearchLib;

public class HolidaySearch : IHolidaySearch
{
    public HolidaySearchResponse Search(HolidaySearchRequest request)
    {
        return new HolidaySearchResponse
        {
            Results = new List<HolidaySearchResult>()
        };
    }

}