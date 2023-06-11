namespace HolidaySearchLib;

public class TripFilteringPipeline : Pipeline<HolidaySearchRequest>
{
    public override HolidaySearchRequest Process(HolidaySearchRequest input)
    {
        foreach(var filter in filters)
        {
            input = filter.Execute(input);
        }

        return input;
    }
}