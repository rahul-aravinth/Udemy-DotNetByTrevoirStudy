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
    }
}
