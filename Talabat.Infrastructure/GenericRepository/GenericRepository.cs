using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Product;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Data;

namespace Talabat.Infrastructure.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if(typeof(T) == typeof(Product))
                return (IReadOnlyList<T>) await _dbContext.Products.Include(P => P.Brand).Include(P => P.Category).ToListAsync();  
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
            => await _dbContext.Set<T>().FindAsync(id); // FindAsync is optimized for primary key lookups 


        //public void Add(T entity)
        //{
        //    _dbContext.Add(entity);
        //    _dbContext.SaveChangesAsync();
        //}

        //public void Update(T entity)
        //    => _dbContext.Update(entity);

        //public void Delete(T entity)
        //    => _dbContext.Remove(entity);


    }
}
