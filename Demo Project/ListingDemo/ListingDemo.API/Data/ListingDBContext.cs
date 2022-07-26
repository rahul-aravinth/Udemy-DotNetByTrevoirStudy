using Microsoft.EntityFrameworkCore;

namespace ListingDemo.API.Data
{
    public class ListingDBContext : DbContext
    {
        public ListingDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "India",
                    ShortName = "IN"
                },
                new Country
                {
                    Id = 2,
                    Name = "China",
                    ShortName = "CN"
                },
                new Country
                {
                    Id = 3,
                    Name = "Russia",
                    ShortName = "RS"
                }
                );
            modelBuilder.Entity<Hotel>().HasData(
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
