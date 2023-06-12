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

        flightData = flightData.Where(flight => flight.DepartureDate == input.DepartureDate).ToList();

        input.FlightData = flightData.ToList();
        input.HotelData = FilterRemainingHotels(input.HotelData, flightData);

        return input;

    }

    private static List<Hotel> FilterRemainingHotels(List<Hotel> hotels, List<Flight> flights)
    {
        var remainingHotels = new List<Hotel>();

        foreach(var hotel in hotels)
        {
            if(flights.Any(flight => hotel.LocalAirports.Contains(flight.To)))
            {
                remainingHotels.Add(hotel);
            }
        }

        return remainingHotels;
    }
}