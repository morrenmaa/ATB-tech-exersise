namespace HolidaySearchLib;

public class Hotel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime ArrivalDate { get; set; }
    public int PricePerNight { get; set; }
    public required List<string> LocalAirports { get; set; }
    public int Nights { get; set; }
}