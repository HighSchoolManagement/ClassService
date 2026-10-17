using ClassService.Application.Common.Specification;
using ClassService.Application.Interfaces;
using System.Linq.Expressions;

namespace classService.Application.Common.Specification
{
    public class Specification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; }

        protected Specification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        } 
    }
}