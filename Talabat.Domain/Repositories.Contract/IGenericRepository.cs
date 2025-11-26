using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.ProductSpecs;

namespace Talabat.Core.Repositories.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetAsync(int id);

        Task<IReadOnlyList<T>> GetAllWithSpecsAsync(ISpecifications<T> specs);
        Task<T?> GetWithSpecsAsync(ISpecifications<T> specs);
        Task<int> GetCountAysnc(ISpecifications<T> specs);
    }
}
