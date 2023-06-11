
using System.Text.Json;

namespace HolidaySearchTests;

public class HolidaySearchLibTests
{
    private readonly IHolidaySearch _sut;

    public HolidaySearchLibTests()
    {
        _sut = new HolidaySearch();
    }

    [Fact]
    public void WhenCallingSearch_ReturnsEmptyListOfResults()
    {
        var request = new HolidaySearchRequest
        {
            FlightData = ConvertFromJsonFile<List<Flight>>("TestData/Flights.json"),
            HotelData = ConvertFromJsonFile<List<Hotel>>("TestData/Hotels.json")
        };

        var results = _sut.Search(request);

        Assert.Empty(results.Results);
    }

    private T ConvertFromJsonFile<T>(string filename)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        string jsonString = File.ReadAllText(filename);
        return JsonSerializer.Deserialize<T>(jsonString, options)!;
    }
}