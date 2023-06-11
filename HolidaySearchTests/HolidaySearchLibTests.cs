
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
        Assert.All(results.Hotels, h => h.LocalAirports.Contains("AGP"));
        Assert.All(results.Flights, f => f.To.Equals("AGP"));
        Assert.All(results.Flights, f => f.From.Equals("MAN"));
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
        Assert.All(results.Hotels, h => h.LocalAirports.Contains("AGP"));
        Assert.All(results.Flights, f => f.To.Equals("AGP"));
    }

    [Fact]
    public void WhenCallingSearchWithDepartureDate_ReturnsListOfResultsSortedByDate()
    {
        var request = new HolidaySearchRequest
        {
            To = "AGP",
            DepartureDate = new DateTime(2023, 07, 01),
            FlightData = ConvertFromJsonFile<List<Flight>>("TestData/Flights.json"),
            HotelData = ConvertFromJsonFile<List<Hotel>>("TestData/Hotels.json")
        };

        var results = _sut.Search(request);
        Assert.Collection(results.Flights, 
            f => Assert.Equal(new DateTime(2023, 07, 01), f.DepartureDate),
            f => Assert.Equal(new DateTime(2023, 07, 01), f.DepartureDate),
            f => Assert.Equal(new DateTime(2023, 07, 01), f.DepartureDate),
            f => Assert.Equal(new DateTime(2023, 10, 25), f.DepartureDate)
            );
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