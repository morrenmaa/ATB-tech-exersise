using System.Text.Json.Serialization;

namespace HolidaySearchLib;

public class Hotel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime ArrivalDate { get; set; }
    [JsonPropertyName("price_per_night")]
    public int PricePerNight { get; set; }
    [JsonPropertyName("local_airports")]
    public required List<string> LocalAirports { get; set; }
    public int Nights { get; set; }
}