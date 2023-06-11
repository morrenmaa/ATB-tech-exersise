
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
            FlightData = string.Empty,
            HotelData = string.Empty
        };

        var results = _sut.Search(request);

        Assert.Empty(results.Results);
    }
}