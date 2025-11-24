using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        #region Without Specs
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetAsync(int id);
        #endregion


        #region With Specs

        Task<IReadOnlyList<T>> GetAllWithSpecsAsync(ISpecifications<T> specs);
        Task<T?> GetWithSpecsAsync(ISpecifications<T> specs);

        #endregion

        //void Add(T entity);
        //void Update(T entity);
        //void Delete(T entity);
    }
}
