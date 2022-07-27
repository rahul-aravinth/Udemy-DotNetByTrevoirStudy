using ListingDemo.API.Contracts;
using ListingDemo.API.Data;
using Microsoft.EntityFrameworkCore;

namespace ListingDemo.API.Repository
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly ListingDBContext _context;

        public CountriesRepository(ListingDBContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<Country> GetDetails(int id)
        {
            return await _context.Countries.Include(x => x.Hotels).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
