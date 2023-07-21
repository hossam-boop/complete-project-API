using Core.Entities;
using Core.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    internal class SpecificationsEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery (IQueryable<TEntity> inputQuery,ISpecifications<TEntity> specifications)
        {
            var query = inputQuery;
            if(specifications.Criteria!= null)
            {
                query= query.Where(specifications.Criteria);
            }
            if(specifications.Orderby!= null)
            {
                query= query.OrderBy(specifications.Orderby);
            }

            if (specifications.OrderbyDescending != null)
            {
                query = query.OrderBy(specifications.OrderbyDescending);
            }
            query = specifications.Includes.Aggregate(query,(current,include)=>current.Include(include));
            return query;
        }
    }
}
