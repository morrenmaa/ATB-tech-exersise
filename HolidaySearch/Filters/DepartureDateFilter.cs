namespace HolidaySearchLib;

public class DepartureDateFilter : IFilter<HolidaySearchRequest>
{
    public HolidaySearchRequest Execute(HolidaySearchRequest input)
    {
        if (input.DepartureDate is null)
        {
            return input;
        }
        var flightData = input.FlightData;

        flightData = flightData.Where(flight => flight.DepartureDate >= input.DepartureDate).ToList();
        
        var orderedFlights = flightData.OrderBy(item => Math.Abs((item.DepartureDate - input.DepartureDate).Value.Days));
        input.FlightData = orderedFlights.ToList();

        return input;

    }
}