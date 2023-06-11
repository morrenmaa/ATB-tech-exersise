
namespace HolidaySearchLib;

public class HolidaySearch : IHolidaySearch
{
    public HolidaySearchResponse Search(HolidaySearchRequest request)
    {
        var tripFilteringPipeline = new TripFilteringPipeline();
        tripFilteringPipeline.Register(new ToAndFromFilter());

        var result = tripFilteringPipeline.Process(request);

        return new HolidaySearchResponse
        {
            Flights = result.FlightData,
            Hotels = result.HotelData
        };
    }
}