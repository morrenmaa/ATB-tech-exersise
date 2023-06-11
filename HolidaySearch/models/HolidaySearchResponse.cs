namespace HolidaySearchLib;

public class HolidaySearchResponse
{
    public required List<Flight> Flights { get; set; }
    public required List<Hotel> Hotels { get; set; }
}