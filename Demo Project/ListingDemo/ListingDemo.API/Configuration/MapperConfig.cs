using AutoMapper;
using ListingDemo.API.Data;
using ListingDemo.API.Models.Country;
using ListingDemo.API.Models.Hotel;

namespace ListingDemo.API.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Country, CreateCountryDTO>().ReverseMap();
            CreateMap<Country, GetCountryDTO>().ReverseMap();
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, UpdateCountryDTO>().ReverseMap();
            CreateMap<Hotel, HotelDTO>().ReverseMap();
        }
    }
}
