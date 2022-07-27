using ListingDemo.API.Data;

namespace ListingDemo.API.Contracts
{
    public interface ICountriesRepository : IGenericRepository<Country>
    {
        Task<Country> GetDetails(int id);
    }
}
