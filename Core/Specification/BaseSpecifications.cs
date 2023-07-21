using System.Linq.Expressions;

namespace Core.Specification
{
    public class BaseSpecifications<T> : ISpecifications<T>
    {
        public BaseSpecifications(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
            
        }

        public Expression<Func<T, bool>> Criteria { get; } 

        public List<Expression<Func<T, object>>> Includes { get; }= new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> Orderby { get; private set; }

        public Expression<Func<T, object>> OrderbyDescending { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
            => Includes.Add(includeExpression);


        protected void AddOrderby(Expression<Func<T, object>> orderbyExpression)
            => Orderby = orderbyExpression;
        protected void AddOrderbyDescending(Expression<Func<T, object>> orderbyDescendingExpression)
            => OrderbyDescending = orderbyDescendingExpression;
    }
}
