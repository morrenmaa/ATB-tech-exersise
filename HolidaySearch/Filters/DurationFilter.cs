namespace HolidaySearchLib;

public class DurationFilter : IFilter<HolidaySearchRequest>
{
    public HolidaySearchRequest Execute(HolidaySearchRequest input)
    {
        if(input.Duration is null)
        {
            return input;
        }

        var remainingHotels = input.HotelData.Where(h => h.Nights >= input.Duration).OrderBy(h => h.Nights).ToList();
        var remainingAirportNames = new List<string>();

        foreach(var hotel in remainingHotels)
        {
            foreach (var airport in hotel.LocalAirports)
            {
                if (!remainingAirportNames.Contains(airport))
                {
                    remainingAirportNames.Add(airport);
                }
            }
        }

        var remainingFlights = input.FlightData.Where(flight => remainingAirportNames.Contains(flight.To)).ToList();

        input.HotelData = remainingHotels;
        input.FlightData = remainingFlights;

        return input;
    }
}