using classService.Application.Common.Specification;
using ClassService.Domain.Entities;

namespace ClassService.Application.Classes.Specifications
{
    public class ClassActiveBySchoolYearIdAndSchoolIdSpec : Specification<Class>
    {
        public ClassActiveBySchoolYearIdAndSchoolIdSpec(int schoolYearId, int schoolId)
        :base(s => s.SchoolYearId == schoolYearId && s.SchoolId == schoolId && s.IsActive)
        {
        }
    }
}