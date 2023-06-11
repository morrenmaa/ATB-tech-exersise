
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
            DepartureDate = new DateTime(2023, 07, 01),
            FlightData = ConvertFromJsonFile<List<Flight>>("TestData/Flights.json"),
            HotelData = ConvertFromJsonFile<List<Hotel>>("TestData/Hotels.json")
        };

        var results = _sut.Search(request);
        Assert.Collection(results.Flights, 
            f => Assert.Equal(new DateTime(2023, 07, 01), f.DepartureDate),
            f => Assert.Equal(new DateTime(2023, 07, 01), f.DepartureDate),
            f => Assert.Equal(new DateTime(2023, 07, 01), f.DepartureDate),
            f => Assert.Equal(new DateTime(2023, 07, 01), f.DepartureDate)
            );
    }

    [Fact]
    public void WhenCallingSearchWithHolidayDuration_ReturnsListOfResultsEqualOfDuration()
    {
        var request = new HolidaySearchRequest
        {
            Duration = 10,
            FlightData = ConvertFromJsonFile<List<Flight>>("TestData/Flights.json"),
            HotelData = ConvertFromJsonFile<List<Hotel>>("TestData/Hotels.json")
        };

        var results = _sut.Search(request);
        Assert.Equal(3, results.Hotels.Count);
        Assert.Equal(9, results.Flights.Count);
    }

// *** EXAMPLE TEST CASES ***

    [Fact]
    public void ExampleTestCase1()
    {
        var request = new HolidaySearchRequest
        {
            From = "MAN",
            To = "AGP",
            DepartureDate = new DateTime(2023, 07, 01),
            Duration = 7,
            FlightData = ConvertFromJsonFile<List<Flight>>("TestData/Flights.json"),
            HotelData = ConvertFromJsonFile<List<Hotel>>("TestData/Hotels.json")
        };

        var result = _sut.Search(request);

        Assert.Equal(2, result.Flights.First().Id);
        Assert.Equal(9, result.Hotels.First().Id);
    }

    [Fact]
    public void ExampleTestCase2()
    {
        var request = new HolidaySearchRequest
        {
            To = "PMI",
            DepartureDate = new DateTime(2023, 06, 15),
            Duration = 10,
            FlightData = ConvertFromJsonFile<List<Flight>>("TestData/Flights.json"),
            HotelData = ConvertFromJsonFile<List<Hotel>>("TestData/Hotels.json")
        };

        var result = _sut.Search(request);

        Assert.Equal(6, result.Flights.First().Id);
        Assert.Equal(5, result.Hotels.First().Id);
    }
    
    [Fact]
    public void ExampleTestCase3()
    {
        var request = new HolidaySearchRequest
        {
            To = "LPA",
            DepartureDate = new DateTime(2022, 11, 10),
            Duration = 14,
            FlightData = ConvertFromJsonFile<List<Flight>>("TestData/Flights.json"),
            HotelData = ConvertFromJsonFile<List<Hotel>>("TestData/Hotels.json")
        };

        var result = _sut.Search(request);

        Assert.Equal(7, result.Flights.First().Id);
        Assert.Equal(6, result.Hotels.First().Id);
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