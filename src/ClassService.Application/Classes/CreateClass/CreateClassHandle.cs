using ClassService.Application.Common.Mediator;
using ClassService.Application.Interfaces;
using ClassService.Application.Models;
using ClassService.Application.Schools.GetSchoolById;
using ClassService.Application.SchoolYears.GetSchoolYearById;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Classes.CreateClass
{
    public class CreateClassHandle : IRequestHandler<CreateClassCommand, CreateClassResponse>
    {
        private readonly IClassRepository classRepository;
        private readonly ISchoolYearRepository schoolYearRepository;
        private readonly ISchoolRepository schoolRepository;
        public CreateClassHandle (IClassRepository classRepository, ISchoolYearRepository schoolYearRepository, ISchoolRepository schoolRepository)
        {
            this.classRepository = classRepository;
            this.schoolYearRepository = schoolYearRepository;
            this.schoolRepository = schoolRepository;
        }
        public async Task<CreateClassResponse> Handle(CreateClassCommand request, CancellationToken cancellationToken = default)
        {
            if (request.CreateClassRequest == null)
            {
                throw new ArgumentException("CreateClassRequest is null");
            }

            var req = request.CreateClassRequest;
            var name = ClassValidator.NormalizeAndValidateName(req.Name);
            if (req.Capacity <= 0)
            {
                throw new ArgumentException("Capacity must be greater than 0");
            }

            await ClassValidator.GetOrThrowAsync(
                (() => schoolYearRepository.GetSchoolYearReadModelByIdAsync(request.SchoolYearId, cancellationToken)),
                (() => new SchoolYearNotFoundException()));

            await ClassValidator.GetOrThrowAsync(
                (() => schoolRepository.GetSchoolReadModelByIdAsync(request.SchoolId)),
                (() => new SchoolNotFoundException()));

            var classCreateModel = new ClassCreateModel
            {
                SchoolId = request.SchoolId,
                SchoolYearId = request.SchoolYearId,
                Name = name,
                Capacity = req.Capacity,
                CreatedDate = DateTime.UtcNow
            };

            var createdClass = await classRepository.AddAsync(classCreateModel, cancellationToken);

            return new CreateClassResponse
            {
                ClassId = createdClass.ClassId,
                SchoolId = createdClass.SchoolId,
                SchoolYearId = createdClass.SchoolYearId,
                Name = createdClass.Name,
                Capacity = createdClass.Capacity,
                CreatedDate = createdClass.CreatedDate ?? DateTime.UtcNow,
                RowVersion = createdClass.RowVersion
            };
        }
    }
}
