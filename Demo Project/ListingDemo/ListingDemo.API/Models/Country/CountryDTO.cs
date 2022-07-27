﻿using ListingDemo.API.Models.Hotel;

namespace ListingDemo.API.Models.Country
{
    public class CountryDTO : BaseCountryDTO
    {
        public int Id { get; set; }
        public List<HotelDTO> Hotels { get; set; }
    }
}
