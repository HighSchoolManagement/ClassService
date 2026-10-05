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
            
            var classExisting = await ClassValidator.GetOrThrowAsync(
                () => classRepository.GetByIdTrackedAsync(request.SchoolYearId, request.SchoolId, request.ClassId),
                () => new ClassNotFoundException());

            var req = request.UpdateClassRequest;
            bool changed = false;

            if (req.Name != null)
            {
                var name = req.Name.Trim();
                classExisting.Name = name;
                changed = true;
            }

            if (req.SchoolYearId != classExisting.SchoolYearId)
            {
                var schoolYear = await ClassValidator.GetOrThrowAsync(
                    (() => schoolYearRepository.GetSchoolYearReadModelByIdAsync(req.SchoolYearId)),
                    (() => new SchoolYearNotFoundException()));
                classExisting.SchoolYearId = schoolYear.Id;
                changed = true;
            }
            if (req.SchoolId != classExisting.SchoolId)
            {
                var school = await ClassValidator.GetOrThrowAsync(
                    (() => schoolRepository.GetSchoolReadModelByIdAsync(req.SchoolId)),
                    (() => new SchoolNotFoundException()));
                
                classExisting.SchoolId = school.Id;
                changed = true;
            }
            if (!changed)
                throw new ArgumentException("At least one field must be provided.");

            await classRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
