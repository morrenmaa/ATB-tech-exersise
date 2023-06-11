namespace HolidaySearchLib;
public class HolidaySearchRequest
{
    public string? From { get; set; }
    public string? To { get; set; }
    public DateTime? DepartureDate { get; set; }
    public required List<Flight> FlightData { get; set; }
    public required List<Hotel> HotelData { get; set; }
}