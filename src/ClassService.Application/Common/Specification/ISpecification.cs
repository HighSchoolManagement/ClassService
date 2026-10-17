using System.Linq.Expressions;

namespace ClassService.Application.Common.Specification
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
    }
}