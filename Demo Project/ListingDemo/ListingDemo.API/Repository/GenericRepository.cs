using ListingDemo.API.Contracts;
using ListingDemo.API.Data;
using Microsoft.EntityFrameworkCore;

namespace ListingDemo.API.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ListingDBContext _context;
        //Injecting context or DB per say
        public GenericRepository(ListingDBContext context)
        {
            this._context = context;
        }
        //Equivalent to GET with id
        public async Task<T> GetAsync(int? id)
        {
            if (id is null)
            {
                return null;
            }

            return await _context.Set<T>().FindAsync(id);
        }
        //Equivalent to GET All
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        //Equivalent to POST
        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        //Equivalent to DELETE
        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        //Equivalent to PUT
        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();

        }
        
        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

    }
}
