
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
    public void WhenCallingSearchWithoutAnySearchTerms_ReturnsAllResults()
    {
        var request = new HolidaySearchRequest
        {
            FlightData = ConvertFromJsonFile<List<Flight>>("TestData/Flights.json"),
            HotelData = ConvertFromJsonFile<List<Hotel>>("TestData/Hotels.json")
        };

        var results = _sut.Search(request);

        Assert.Equal(13, results.Hotels.Count);
        Assert.Equal(12, results.Flights.Count);
    }

    [Fact]
    public void WhenCallingSearchWithFromAndTo_ReturnsListOfResultsMatchingSearchTerms()
    {
        var request = new HolidaySearchRequest
        {
            From = "MAN",
            To = "AGP",
            FlightData = ConvertFromJsonFile<List<Flight>>("TestData/Flights.json"),
            HotelData = ConvertFromJsonFile<List<Hotel>>("TestData/Hotels.json")
        };

        var results = _sut.Search(request);
        Assert.Equal(3, results.Flights.Count);
        Assert.Equal(4, results.Hotels.Count);
    }

    [Fact]
    public void WhenCallingSearchWithTo_ReturnsListOfResultsMatchingSearchTerms()
    {
        var request = new HolidaySearchRequest
        {
            To = "AGP",
            FlightData = ConvertFromJsonFile<List<Flight>>("TestData/Flights.json"),
            HotelData = ConvertFromJsonFile<List<Hotel>>("TestData/Hotels.json")
        };

        var results = _sut.Search(request);
        Assert.Equal(5, results.Flights.Count);
        Assert.Equal(4, results.Hotels.Count);
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