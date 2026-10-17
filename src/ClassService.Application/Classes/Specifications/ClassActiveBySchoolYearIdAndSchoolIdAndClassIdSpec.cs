using classService.Application.Common.Specification;
using ClassService.Domain.Entities;
using System.Linq.Expressions;

namespace ClassService.Application.Classes.Specifications
{
    public class ClassActiveBySchoolYearIdAndSchoolIdAndClassIdSpec:Specification<Class>
    {
        public ClassActiveBySchoolYearIdAndSchoolIdAndClassIdSpec(int schoolYearId, int schoolId, int classId) 
            : base(c => c.SchoolYearId == schoolYearId && c.SchoolId == schoolId && c.Id == classId && c.IsActive)
        {
        }
    }
}