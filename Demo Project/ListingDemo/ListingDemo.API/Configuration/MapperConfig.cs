using AutoMapper;
using ListingDemo.API.Data;
using ListingDemo.API.Models.Country;
using ListingDemo.API.Models.Hotel;
using ListingDemo.API.Models.Users;

namespace ListingDemo.API.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            //Country
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, GetCountryDTO>().ReverseMap();
            CreateMap<Country, CreateCountryDTO>().ReverseMap();
            CreateMap<Country, UpdateCountryDTO>().ReverseMap();
            //Hotel
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Hotel, GetHotelDTO>().ReverseMap();
            CreateMap<Hotel, CreateHotelDTO>().ReverseMap();
            CreateMap<Hotel, UpdateHotelDTO>().ReverseMap();
            //User
            CreateMap<APIUserDTO, CustomAPIIdentiyUser>().ReverseMap();
        }
    }
}
