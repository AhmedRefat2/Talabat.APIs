using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public interface ISpecifications<T> where T : BaseEntity
    {
        // 1. Where
        public Expression<Func<T, bool>> Criteria { get; set; }
        // 2. Includes 
        public List<Expression<Func<T, object>>> Includes { get; set; }

    }
}
