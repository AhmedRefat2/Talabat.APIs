using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository.GenericRepository
{
    internal static class SpecificationsEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> specs)
        {
            var query = inputQuery; // _dbContext.Set<TEntity>

            // 1. Criteria
            if(specs.Criteria is not null)
                query = query.Where(specs.Criteria);

            // 2. Includes 
            if (specs.Includes is not null && specs.Includes.Count() > 0)
                query = specs.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;
        }

    }
}
