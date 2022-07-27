using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ListingDemo.API.Data.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "A2B",
                    Address = "Chennai",
                    Rating = 8,
                    CountryId = 1
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Fairmont Peace Hotel",
                    Address = "Shanghai",
                    Rating = 10,
                    CountryId = 2
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Angleterre Hotel",
                    Address = "Morskaya Ulitsa",
                    Rating = 10,
                    CountryId = 3
                }
            );
        }
    }
}
