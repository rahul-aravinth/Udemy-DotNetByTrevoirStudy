using ListingDemo.API.Contracts;
using ListingDemo.API.Data;
using Microsoft.EntityFrameworkCore;

namespace ListingDemo.API.Repository
{
    public class HotelsRepository : GenericRepository<Hotel>, IHotelsRepository
    {
        public HotelsRepository(ListingDBContext context) : base(context)
        {
        }
    }
}
