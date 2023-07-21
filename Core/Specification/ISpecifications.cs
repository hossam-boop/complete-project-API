using System.Linq.Expressions;

namespace Core.Specification
{
    public interface ISpecifications<T>
    {
        Expression<Func<T,bool>> Criteria { get; }
        List<Expression<Func<T,object>>> Includes { get; }
        Expression<Func<T, object>> Orderby { get; }
        Expression<Func<T, object>> OrderbyDescending { get; }



    }
}
