using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Product;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;
using Talabat.Repository.GenericRepository;

namespace Talabat.Infrastructure.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Without Specs 
        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await _dbContext.Set<T>().ToListAsync();
        public async Task<T?> GetAsync(int id)
            => await _dbContext.Set<T>().FindAsync(id); // FindAsync is optimized for primary key lookups  

        #endregion

        #region With Specs

        public async Task<IReadOnlyList<T>> GetAllWithSpecsAsync(ISpecifications<T> specs)
            => await ApplySpecifications(specs).AsNoTracking().ToListAsync();

        public async Task<T?> GetWithSpecsAsync(ISpecifications<T> specs)
            => await ApplySpecifications(specs).AsNoTracking().FirstOrDefaultAsync();

        private IQueryable<T> ApplySpecifications(ISpecifications<T> specs)
            => SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), specs);


        #endregion
        public async Task<int> GetCountAysnc(ISpecifications<T> specs)
        {
           return await ApplySpecifications(specs).CountAsync();
        }

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
