using AutoMapper;
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
        private readonly IMapper _mapper;
        public CreateClassHandle (IClassRepository classRepository, ISchoolYearRepository schoolYearRepository, ISchoolRepository schoolRepository, IMapper mapper)
        {
            this.classRepository = classRepository;
            this.schoolYearRepository = schoolYearRepository;
            this.schoolRepository = schoolRepository;
            _mapper = mapper;
        }
        public async Task<CreateClassResponse> Handle(CreateClassCommand request, CancellationToken cancellationToken = default)
        {
            await ClassValidator.GetOrThrowAsync(
                (() => schoolYearRepository.GetSchoolYearReadModelByIdAsync(request.CreateClassRequest.SchoolYearId)),
                (() => new SchoolYearNotFoundException()));

            await ClassValidator.GetOrThrowAsync(
                (() => schoolRepository.GetSchoolReadModelByIdAsync(request.CreateClassRequest.SchoolId)),
                (() => new SchoolNotFoundException()));
            var classCreateModel = _mapper.Map<ClassCreateModel>(request.CreateClassRequest);

            var createdClass = await classRepository.AddAsync(classCreateModel);

            return new CreateClassResponse
            {
                ClassId = createdClass.ClassId,
                SchoolId = createdClass.SchoolId,
                SchoolYearId = createdClass.SchoolYearId,
                Name = createdClass.Name,
                Capacity = createdClass.Capacity,
                CreatedDate = createdClass.CreatedDate ?? System.DateTime.Now
            };
        }
    }
}
