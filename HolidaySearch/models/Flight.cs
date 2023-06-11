using System.Text.Json.Serialization;

namespace HolidaySearchLib;

public class Flight
{
    public int Id { get; set; }
    public required string Airline { get; set; }
    public required string From { get; set; }
    public required string To { get; set; }
    public int Price { get; set; }
    [JsonPropertyName("departure_date")]
    public DateTime DepartureDate { get; set; }
}