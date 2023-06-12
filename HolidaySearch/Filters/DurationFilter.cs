namespace HolidaySearchLib;

public class DurationFilter : IFilter<HolidaySearchRequest>
{
    public HolidaySearchRequest Execute(HolidaySearchRequest input)
    {
        if(input.Duration is null)
        {
            return input;
        }

        var remainingHotels = input.HotelData.Where(h => h.Nights == input.Duration).ToList();

        var remainingAirportNames = FilterRemainingAirportNames(remainingHotels);
        var remainingFlights = input.FlightData.Where(flight => remainingAirportNames.Contains(flight.To)).ToList();

        input.HotelData = remainingHotels;
        input.FlightData = remainingFlights;

        return input;
    }

    private static List<string> FilterRemainingAirportNames(List<Hotel> hotels)
    {
        var remainingAirportNames = new List<string>();

        foreach(var hotel in hotels)
        {
            foreach (var airport in hotel.LocalAirports)
            {
                if (!remainingAirportNames.Contains(airport))
                {
                    remainingAirportNames.Add(airport);
                }
            }
        }

        return remainingAirportNames;
    }
}