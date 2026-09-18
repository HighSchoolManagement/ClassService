using ClassService.Application.Classes.GetClassById;
using ClassService.Application.Common.Mediator;
using ClassService.Application.Interfaces;
using ClassService.Application.Schools.GetSchoolById;
using ClassService.Application.SchoolYears.GetSchoolYearById;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Classes.UpdateClass
{
    public class UpdateClassHandle : IRequestHandler<UpdateClassCommand, Unit>
    {
        private readonly IClassRepository classRepository;
        private readonly ISchoolYearRepository schoolYearRepository;
        private readonly ISchoolRepository schoolRepository;
        public UpdateClassHandle(IClassRepository classRepository, ISchoolYearRepository schoolYearRepository, ISchoolRepository schoolRepository)
        {
            this.classRepository = classRepository;
            this.schoolYearRepository = schoolYearRepository;
            this.schoolRepository = schoolRepository;
        }
        public async Task<Unit> Handle(UpdateClassCommand request, CancellationToken cancellationToken = default)
        {
            if (request?.UpdateClassRequest == null)
                throw new ArgumentException("Request is null");
            if (request.ClassId <= 0|| request.SchoolYearId <= 0)
            {
                throw new ArgumentException("ClassId or SchoolYearId must be greater than 0");
            }
            var existingClass = await classRepository.GetByIdTrackedAsync(request.SchoolYearId, request.ClassId);
            if (existingClass == null)
                throw new ClassNotFoundException();

            var req = request.UpdateClassRequest;
            bool changed = false;

            if (req.Name != null)
            {
                var name = req.Name.Trim();
                existingClass.Name = name;
                changed = true;
            }

            if (req.SchoolYearId != existingClass.SchoolYearId)
            {
                var schoolYear = await schoolYearRepository.GetSchoolYearReadModelByIdAsync(req.SchoolYearId);
                if (schoolYear == null)
                    throw new SchoolYearNotFoundException();
                existingClass.SchoolYearId = req.SchoolYearId;
                changed = true;
            }
            if (req.SchoolId != existingClass.SchoolId)
            {
                var school = await schoolRepository.GetSchoolReadModelByIdAsync(req.SchoolId);
                if (school == null)
                    throw new SchoolNotFoundException();
                existingClass.SchoolId = req.SchoolId;
                changed = true;
            }
            if (!changed)
                throw new ArgumentException("At least one field must be provided.");

            await classRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
