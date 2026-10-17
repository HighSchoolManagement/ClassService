using classService.Application.Common.Specification;
using ClassService.Domain.Entities;
using System.Linq.Expressions;

namespace ClassService.Application.Classes.Specifications
{
    public class ClassNameContainsSpec: Specification<Class>
    {
        public ClassNameContainsSpec(string className) 
            : base(c => string.IsNullOrWhiteSpace(className) || c.Name.Contains(className))
        {
        }
    }
}