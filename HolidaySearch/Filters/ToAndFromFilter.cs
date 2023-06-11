namespace HolidaySearchLib;

public class ToAndFromFilter : IFilter<HolidaySearchRequest>
{
    public HolidaySearchRequest Execute(HolidaySearchRequest input)
    {
        var flightData = input.FlightData;
        var hotelData = input.HotelData;
        FilterFlightsByToAndFrom(ref flightData, input.To, input.From);
        FilterHotelsByTo(ref hotelData, input.To);

        input.FlightData = flightData;
        input.HotelData = hotelData;

        return input;
    }

        private static void FilterFlightsByToAndFrom(ref List<Flight> flights, string? to, string? from)
    {
        if (!string.IsNullOrEmpty(to))
        {
            flights = flights.Where(flight => flight.To == to).ToList();
        }

        if (!string.IsNullOrEmpty(from))
        {
            flights = flights.Where(flight => flight.From == from).ToList();
        }
        
    }

    private static void FilterHotelsByTo(ref List<Hotel> hotels, string? to)
    {
        if(!string.IsNullOrEmpty(to))
        {
            hotels = hotels.Where(h => h.LocalAirports.Contains(to)).ToList();
        }
    }
}