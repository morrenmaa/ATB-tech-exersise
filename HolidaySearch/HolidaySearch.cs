
namespace HolidaySearchLib;

public class HolidaySearch : IHolidaySearch
{
    public HolidaySearchResponse Search(HolidaySearchRequest request)
    {
        var flights = request.FlightData;
        var hotels = request.HotelData;
        FilterFlightsByToAndFrom(ref flights, request.To, request.From);
        FilterHotelsByTo(ref hotels, request.To);

        return new HolidaySearchResponse
        {
            Flights = flights,
            Hotels = hotels
        };
    }

    private void FilterFlightsByToAndFrom(ref List<Flight> flights, string? to, string? from)
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

    private void FilterHotelsByTo(ref List<Hotel> hotels, string? to)
    {
        if(!string.IsNullOrEmpty(to))
        {
            hotels = hotels.Where(h => h.LocalAirports.Contains(to)).ToList();
        }
    }
}