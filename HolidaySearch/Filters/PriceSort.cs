namespace HolidaySearchLib;

public class PriceSort : IFilter<HolidaySearchRequest>
{
    const string PriceSortAscending = "asc";
    const string PriceSortDescending = "desc";
    public HolidaySearchRequest Execute(HolidaySearchRequest input)
    {
        if (input.PriceSort == PriceSortAscending)
        {
            input.FlightData = input.FlightData.OrderBy(Flight => Flight.Price).ToList();
            input.HotelData = input.HotelData.OrderBy(hotel => hotel.PricePerNight).ToList();
        }

        if (input.PriceSort == PriceSortDescending)
        {
            input.FlightData = input.FlightData.OrderByDescending(Flight => Flight.Price).ToList();
            input.HotelData = input.HotelData.OrderByDescending(hotel => hotel.PricePerNight).ToList();
        }

        return input;
    }
}