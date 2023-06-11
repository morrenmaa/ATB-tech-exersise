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

        var remainingHotels = new List<Hotel>();

        foreach(var hotel in input.HotelData)
        {
            if(flightData.Any(flight => hotel.LocalAirports.Contains(flight.To)))
            {
                remainingHotels.Add(hotel);
            }
        }

        input.FlightData = flightData.ToList();
        input.HotelData = remainingHotels;

        return input;

    }
}